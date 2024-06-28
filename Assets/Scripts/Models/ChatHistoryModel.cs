using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Models
{
    public class ChatHistoryModel
    {
        [HideInInspector]
        public List<MessageModel> Messages = new List<MessageModel>();

        public MessageModel[] SerializedMessages;
        
        [HideInInspector]
        public int IndexOfNewMessage = 0;
    }
}