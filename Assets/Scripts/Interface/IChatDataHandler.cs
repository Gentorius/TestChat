using Models;

namespace Interface
{
    public interface IChatDataHandler : IService
    {
        public void SaveChatHistory();
        public ChatHistory LoadHistory();
    }
}