using Controllers;
using Models;
using Presenter.View.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.View.Scroll
{
    public class ChatScrollRect : ScrollRect
    {
        private int _activeUserID;
        
        public void Initialize(int activeUserID)
        {
            _activeUserID = activeUserID;
        }
        
        private void LoadChat(ChatHistoryModel chatHistoryModel, UserStorageModel userStorageModel)
        {
            foreach (var message in chatHistoryModel.Messages)
            {
                var messageWidget = Instantiate(Resources.Load<MessageWidget>("Prefabs/MessageWidget"), content);
                messageWidget.SpawnMessage(message, userStorageModel.Users[message.SenderId], message.SenderId == _activeUserID);
            }
        }
        
        private void UpdateChatView(MessageModel message)
        {
            
        }
    }
}