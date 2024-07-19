using Controllers;
using Models;
using Presenter.View.Widget;
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
        
        private void LoadChat(ChatHistoryModel chatHistoryModel)
        {
            foreach (var message in chatHistoryModel.Messages)
            {
                Instantiate(new MessageWidget());
            }
        }
        
        private void UpdateChatView(MessageModel message)
        {
            
        }
    }
}