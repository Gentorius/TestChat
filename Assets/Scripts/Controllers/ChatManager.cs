using Attributes;
using Interface;
using Models;
using Presenter;
using UnityEngine;

namespace Controllers
{
    public class ChatManager : ChatDataHandler, IChatManager
    {
        [Inject]
        private ChatPresenter _chatPresenter;
        
        public void AddMessageFromJson(string message)
        {
            var messageModel = JsonUtility.FromJson<Message>(message);
            
            ChatHistory.AddMessage(messageModel);
            FinalizeChanges();
        }

        public void DeleteMessage(int messageIndex)
        {
            ChatHistory.DeleteMessage(messageIndex);
            FinalizeChanges();
        }

        public void EditMessage(string messageJson, int messageIndex)
        {
            var messageModel = JsonUtility.FromJson<Message>(messageJson);
            
            ChatHistory.EditMessage(messageIndex, messageModel);
            FinalizeChanges();
        }

        public void AddReaction(int messageIndex, string reaction)
        {
            var messageModel = ChatHistory.Messages[messageIndex];
            var currentReactions = ChatHistory.Messages[messageIndex].Reactions;
            string[] newReactions;
            if (currentReactions == null)
            {
                newReactions = new[]{reaction};
                FinalizeReaction(messageModel, newReactions, messageIndex);
                return;
            }
            
            newReactions = new string[currentReactions.Length + 1];
            for (var i = 0; i < currentReactions.Length; i++)
            {
                newReactions[i] = currentReactions[i];
            }
            newReactions[^1] = reaction;
            FinalizeReaction(messageModel, newReactions, messageIndex);
        }

        private void FinalizeReaction(Message message, string[] newReactions, int index)
        {
            message.Reactions = newReactions;
            ChatHistory.EditMessage(index, message);
            FinalizeChanges();
        }
        
        private void FinalizeChanges()
        {
            SaveChatHistory();
            var chatHistory = LoadHistory();
            _chatPresenter.UpdateChatView(chatHistory);
        }
    }
}