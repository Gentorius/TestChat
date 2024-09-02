using System;
using Models;
using UnityEngine;

namespace Interface
{
    public interface IMessageWidget
    {
        public event Action<int, float> OnDeleteMessage;
        
        public bool IsDestroyed { get; }
        
        public virtual float InitializeMessage (Message message, Sprite profileImage, int index, bool isEditMode = false)
        {
            return default;
        }
        public void EnableEditMode();
        public void DisableEditMode();
        public void SetMessageIndex(int index);
        public void Destroy();
    }
}