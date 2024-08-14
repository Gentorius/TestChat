using System.Collections.Generic;
using Models;

namespace Interface
{
    public interface IChatHistory : IService
    {
        List<Message> Messages { get; }
        void AddMessage(Message message);
        void DeleteMessage(int index);
        void EditMessage(int index, Message newContent);
    }
}