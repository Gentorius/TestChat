using System;

namespace Interface
{
    public interface IChatManager : IService
    {
        public void AddMessageFromJson(string message);
        public void DeleteMessage(int messageIndex);
        public void EditMessage(string messageJson, int messageIndex);
        public void AddReaction(int messageIndex, string reaction);
    }
}