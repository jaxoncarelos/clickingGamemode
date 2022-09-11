using Sandbox;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sandbox
{
	/// <summary>
	/// WebSocketExample shows example s&box WebSocket implementation
	/// </summary>
	public class WebSocketClient
	{
		private WebSocket _webSocket;
		private readonly string _connectString = "";
		public WebSocketClient()
		{
			_webSocket = new WebSocket();
			_webSocket.OnMessageReceived += OnMessage;

		}

		private void OnMessage( string message )
		{
            Log.Info(message);
			ConsoleSystem.Run("setValues", message);
		}

		public async void SendMessage( string message )
		{
			await _webSocket.Send( message );
		}

		/// <summary>
		/// Connect to the remote WebSocket Server.
		/// </summary>
		/// <param name="path">The path appended to the connection string.</param>
		/// <returns>Whether the WebSocket is connected.</returns>
		public async Task<bool> Connect()
		{
			await _webSocket.Connect( _connectString );
			return _webSocket.IsConnected;
		}

		/// <summary>
		/// Method for disposing of the WebSocket.
		/// </summary>
		public void Shutdown()
		{
			_webSocket?.Dispose();
			_webSocket = null;
		}

		/// <summary>
		/// Asynchronous method for Sending a message to the WebSocket Server.
		/// </summary>
		/// <param name="data">The message to send to the WebSocket Server.</param>
		public async void Send( string data )
		{
			await _webSocket.Send( data );
		}
	}
}
