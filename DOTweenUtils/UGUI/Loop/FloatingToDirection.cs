using DG.Tweening;

using UnityEngine;

namespace DOTweenUtils
{
    public class FloatingToDirection : DOTweenUtilsItem<FloatingToDirection>
    {
        [Header("Negative Numbers => Left/Down / Positive Numbers => Right/Up")]
        [SerializeField] private float localMoveAmountX;
        [SerializeField] private float localMoveAmountY;
        [Header("Anim Time")]
        [SerializeField] private float animTime = 1f;
        [Header("Easing")]
        [SerializeField] private Ease floatingEaseType = Ease.Linear;

        Sequence mySequence;

        public override FloatingToDirection Perform()
        {

            Stop(false);
            transform.rotation = Quaternion.identity;
            RectTransform rectTransform = GetComponent<RectTransform>();

            Vector2 currentPos = rectTransform.anchoredPosition;
            Vector2 targetPos = currentPos + new Vector2(localMoveAmountX, localMoveAmountY);

            mySequence = DOTween.Sequence();

            mySequence.Append(rectTransform.DOAnchorPos(targetPos, animTime).SetLoops(2, LoopType.Yoyo).SetEase(floatingEaseType))
                .SetLoops(-1, LoopType.Restart);

            return this;
        }



        public override void Stop(bool complete)
        {
            mySequence?.Kill(complete);
        }


        private void Start()
        {
            if (performOnStart) Perform();
        }

        private void OnDestroy()
        {
            Stop(false);
        }
    }

}
