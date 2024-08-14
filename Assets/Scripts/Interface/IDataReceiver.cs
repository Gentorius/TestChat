namespace Interface
{
    public interface IDataReceiver : IService
    {
        public void ReceiveMessage(string messageJson);
        public void ReceiveEditedMessage(string newMessageJson, int messageIndex);
        public void ReceiveDeletedMessage(int messageIndex);
        public void ReceiveReaction(string reactionJson, int messageIndex);
    }
}