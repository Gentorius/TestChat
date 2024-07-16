using System;
using Controllers;
using Models;

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
        
        public void SendData(string messageJSON)
        {
            ReceiveData?.Invoke(messageJSON);
        }
        
        public void OnReceiveDataHandler(string messageJSON)
        {
            _chatConfigController.AddMessageFromJson(messageJSON);
        }
        
        public void Dispose()
        {
            ReceiveData -= OnReceiveDataHandler;
        }
    }
}