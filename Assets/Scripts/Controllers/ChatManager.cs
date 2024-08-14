using System;
using Attributes;
using Interface;
using Models;
using UnityEngine;

namespace Controllers
{
    public class ChatManager : IChatManager
    {
        [Inject]
        private ChatHistory _chatHistory;
        
        public event Action OnChatHistoryChanged;
        
        public void AddMessageFromJson(string message)
        {
            var messageModel = JsonUtility.FromJson<Message>(message);
            
            _chatHistory.AddMessage(messageModel);
            OnChatHistoryChanged?.Invoke();
        }

        public void DeleteMessage(int messageIndex)
        {
            _chatHistory.DeleteMessage(messageIndex);
            OnChatHistoryChanged?.Invoke();
        }

        public void EditMessage(string messageJson, int messageIndex)
        {
            var messageModel = JsonUtility.FromJson<Message>(messageJson);
            
            _chatHistory.EditMessage(messageIndex, messageModel);
            OnChatHistoryChanged?.Invoke();
        }

        public void AddReaction(int messageIndex, string reaction)
        {
            var messageModel = _chatHistory.Messages[messageIndex];
            var currentReactions = _chatHistory.Messages[messageIndex].Reactions;
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
            _chatHistory.EditMessage(index, message);
            OnChatHistoryChanged?.Invoke();
        }
    }
}