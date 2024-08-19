using System;

namespace Models
{
    [Serializable]
    public class Message
    {
        public int SenderId;
        public string MessageText;
        public string Sticker;
        public string[] Reactions;
        public string TimeSent;
    }
}