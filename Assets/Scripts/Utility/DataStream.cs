using System;
using Attributes;
using Interface;

namespace Utility
{
    public class DataStream : IDataSender, IDataReceiver
    {
        public event Action<string> OnSendMessage;
        public event Action<string, int> OnEditMessage;
        public event Action<int> OnDeleteMessage;
        public event Action<string, int> OnAddReaction;
        
        [Inject]
        private IChatManager _chatManager;
        
        private bool _hasSubscribed = false;

        public DataStream()
        {
            if (_hasSubscribed) return;
            
            OnSendMessage += ReceiveMessage;
            OnEditMessage += ReceiveEditedMessage;
            OnDeleteMessage += ReceiveDeletedMessage;
            OnAddReaction += ReceiveReaction;
            _hasSubscribed = true;
        }
        
        public void SendMessage(string messageJson)
        {
            OnSendMessage?.Invoke(messageJson);
        }

        public void EditMessage(string newMessageJson, int messageIndex)
        {
            OnEditMessage?.Invoke(newMessageJson, messageIndex);
        }

        public void DeleteMessage(int messageIndex)
        {
            OnDeleteMessage?.Invoke(messageIndex);
        }

        public void AddReaction(string reactionJson, int messageIndex)
        {
            OnAddReaction?.Invoke(reactionJson, messageIndex);
        }

        public void ReceiveMessage(string messageJson)
        {
            _chatManager.AddMessageFromJson(messageJson);
        }

        public void ReceiveEditedMessage(string newMessageJson, int messageIndex)
        {
            _chatManager.EditMessage(newMessageJson, messageIndex);
        }

        public void ReceiveDeletedMessage(int messageIndex)
        {
            _chatManager.DeleteMessage(messageIndex);
        }

        public void ReceiveReaction(string reactionJson, int messageIndex)
        {
            _chatManager.AddReaction(messageIndex, reactionJson);
        }
    }
}