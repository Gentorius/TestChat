using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models
{
    [Serializable]
    public class MessageModel
    {
        
        public int UserOwnerId;
        public string Message;
        public string[] SmallEmojis;
        public string BigEmoji;
        public string[] ReactionEmojis;
        public string TimeSent;
        [HideInInspector]
        public int MessageIndex;
    }
}