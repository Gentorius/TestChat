using System;
using Controllers;

namespace Utility
{
    public class DataStream
    {
        public event Action<string> ReceiveData;
        
        private ChatConfigController _chatConfigController;
        
        public void Initialize(ChatConfigController chatConfigController)
        {
            ReceiveData += OnReceiveDataHandler;
            _chatConfigController = chatConfigController;
        }
        
        public void SendData(string messageJson)
        {
            ReceiveData?.Invoke(messageJson);
        }
        
        public void OnReceiveDataHandler(string messageJson)
        {
            _chatConfigController.AddMessageFromJson(messageJson);
        }
        
        public void Dispose()
        {
            ReceiveData -= OnReceiveDataHandler;
        }
    }
}