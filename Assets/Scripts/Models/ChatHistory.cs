using System;
using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class ChatHistory : IChatHistory
    {
        [SerializeField]
        private List<Message> _messages = new List<Message>();
        
        public List<Message> Messages => _messages;
        
        public void AddMessage(Message message)
        {
            _messages.Add(message);
        }

        public void DeleteMessage(int index)
        {
            _messages.RemoveAt(index);
        }
        
        public void EditMessage(int index, Message newContent)
        {
            _messages[index] = newContent;
        }
    }
}