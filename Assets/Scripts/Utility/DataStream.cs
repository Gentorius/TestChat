using System;
using Controllers;
using Interface;

namespace Utility
{
    public class DataStream : IService
    {
        public event Action<string> ReceiveData;
        
        private ChatDataHandler _chatDataHandler;
        
        public void Initialize(ChatDataHandler chatDataHandler)
        {
            ReceiveData += OnReceiveDataHandler;
            _chatDataHandler = chatDataHandler;
        }
        
        public void SendData(string messageJson)
        {
            ReceiveData?.Invoke(messageJson);
        }
        
        public void OnReceiveDataHandler(string messageJson)
        {
            _chatDataHandler.AddMessageFromJson(messageJson);
        }
        
        public void Dispose()
        {
            ReceiveData -= OnReceiveDataHandler;
        }
    }
}