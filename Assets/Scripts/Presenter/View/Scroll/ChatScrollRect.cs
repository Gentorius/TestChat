using Controllers;
using Models;
using Presenter.View.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.View.Scroll
{
    public class ChatScrollRect : ScrollRect
    {
        public void ScrollToBottom()
        {
            verticalNormalizedPosition = 0;
        }
    }
}