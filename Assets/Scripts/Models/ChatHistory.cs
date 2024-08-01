using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class ChatHistory
    {
        [HideInInspector]
        public List<MessageModel> Messages = new List<MessageModel>();

        public MessageModel[] SerializedMessages;
        
        [HideInInspector]
        public int IndexOfNewMessage = 0;
        
        public bool LastChangeWasByUser = false;
    }
}