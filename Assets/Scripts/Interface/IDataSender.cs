namespace Interface
{
    public interface IDataSender : IService
    {
        public void SendMessage(string messageJson);
        public void EditMessage(string newMessageJson, int messageIndex);
        public void DeleteMessage(int messageIndex);
        public void AddReaction(string reactionJson, int messageIndex);
    }
}