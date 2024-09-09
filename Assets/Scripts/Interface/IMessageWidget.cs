using System;
using Models;
using UnityEngine;

namespace Interface
{
    public interface IMessageWidget
    {
        public event Action<int, float> OnDeleteMessage;

        public float InitializeMessage(Message message, Sprite profileImage, int index, bool isEditMode = false, bool isLastMessageOfUser = false);
        public void EnableEditMode();
        public void DisableEditMode();
        public void SetMessageIndex(int index);
        public void Destroy();
        public void SetLastMessageStatus(bool isLastMessage);
    }
}