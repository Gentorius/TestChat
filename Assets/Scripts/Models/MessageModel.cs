using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models
{
    [Serializable]
    public class MessageModel
    {
        
        public int SenderId;
        public string Message;
        public string Sticker;
        public string[] ReactionEmojis;
        public DateTime TimeSent;
        [HideInInspector]
        public int MessageIndex;
    }
}