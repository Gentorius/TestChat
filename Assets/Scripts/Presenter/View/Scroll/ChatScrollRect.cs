using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Presenter.View.Scroll
{
    public class ChatScrollRect : ScrollRect
    {
        public void ScrollToBottom()
        {
            this.DONormalizedPos(Vector2.zero, 1f);
        }

        private const float _smoothScrollTime = 0.2f;

        public override void OnScroll(PointerEventData data)
        {
            if (!IsActive())
                return;
            
            Vector2 positionBefore = normalizedPosition;
            this.DOKill(complete: true);
            base.OnScroll(data);
            Vector2 positionAfter = normalizedPosition;

            normalizedPosition = positionBefore;
            this.DONormalizedPos(positionAfter, _smoothScrollTime);
        }
    }
}