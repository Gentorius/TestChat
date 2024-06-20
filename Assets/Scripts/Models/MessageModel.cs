using System;

namespace Models
{
    [Serializable]
    public class MessageModel
    {
        public string message;
        public int userOwnerId;
        public int messageIndex;
    }
}