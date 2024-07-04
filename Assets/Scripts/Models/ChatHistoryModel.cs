using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class ChatHistoryModel
    {
        [HideInInspector]
        public List<MessageModel> Messages = new List<MessageModel>();

        public MessageModel[] SerializedMessages;
        
        [HideInInspector]
        public int IndexOfNewMessage = 0;
    }
}