using DG.Tweening;

using UnityEngine;

namespace DOTweenUtils
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DisappearToOffScreen : DOTweenUtilsItem<DisappearToOffScreen>
    {
        public enum AppearMoveType
        {
            Top, Bottom, Left, Right
        }

        [SerializeField] private float animTime = 0.5f;
        [SerializeField] private Ease moveEaseType = Ease.InCubic;
        [SerializeField] private AppearMoveType appearFrom;

        Sequence mySequence;
        CanvasGroup cg;

        public override DisappearToOffScreen Perform()
        {
            Stop(false);

            gameObject.SetActive(true);

            #region Target Pos Setup
            RectTransform rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                Debug.LogError("The GameObject does not have a RectTransform component.");
                return this;
            }

            RectTransform canvasRectTransform = GetCanvasRectTransform(rectTransform);
            if (canvasRectTransform == null)
            {
                Debug.LogError("The UI element is not a child of a Canvas.");
                return this;
            }

            Vector2 currentPos = rectTransform.anchoredPosition;
            Vector2 targetPos = Vector2.zero;

            switch (appearFrom)
            {
                case AppearMoveType.Left:
                    targetPos = FindLeftPos(rectTransform, canvasRectTransform);
                    break;
                case AppearMoveType.Right:
                    targetPos = FindRightPos(rectTransform, canvasRectTransform);
                    break;
                case AppearMoveType.Top:
                    targetPos = FindTopPos(rectTransform, canvasRectTransform);
                    break;
                case AppearMoveType.Bottom:
                    targetPos = FindBottomPos(rectTransform, canvasRectTransform);
                    break;
            };
            #endregion

            #region Anim
            mySequence = DOTween.Sequence();
            mySequence.Append(rectTransform.DOAnchorPos(targetPos, animTime).SetEase(moveEaseType))
                .Insert(0f, cg.DOFade(0f, animTime))
                .OnComplete(() =>
                {
                    OnComplete?.Invoke();
                    OnComplete = null;
                });
            #endregion

            return this;
        }

        private Vector2 FindLeftPos(RectTransform rectTransform, RectTransform canvasRectTransform)
        {
            Vector2 targetPos = rectTransform.anchoredPosition;
            targetPos.x = -canvasRectTransform.rect.width / 2 + rectTransform.rect.width * rectTransform.pivot.x;
            return targetPos;
        }

        private Vector2 FindRightPos(RectTransform rectTransform, RectTransform canvasRectTransform)
        {
            Vector2 targetPos = rectTransform.anchoredPosition;
            targetPos.x = canvasRectTransform.rect.width / 2 - rectTransform.rect.width * (1 - rectTransform.pivot.x);
            return targetPos;
        }

        private Vector2 FindTopPos(RectTransform rectTransform, RectTransform canvasRectTransform)
        {
            Vector2 targetPos = rectTransform.anchoredPosition;
            targetPos.y = canvasRectTransform.rect.height / 2 - rectTransform.rect.height * (1 - rectTransform.pivot.y);
            return targetPos;
        }

        private Vector2 FindBottomPos(RectTransform rectTransform, RectTransform canvasRectTransform)
        {
            Vector2 targetPos = rectTransform.anchoredPosition;
            targetPos.y = -canvasRectTransform.rect.height / 2 + rectTransform.rect.height * rectTransform.pivot.y;
            return targetPos;
        }


        private RectTransform GetCanvasRectTransform(RectTransform rectTransform)
        {
            Transform currentTransform = rectTransform;
            while (currentTransform != null)
            {
                if (currentTransform.GetComponent<Canvas>() != null)
                {
                    return currentTransform as RectTransform;
                }
                currentTransform = currentTransform.parent;
            }
            return null; // Canvas bulunamazsa
        }



        private void Awake()
        {
            cg = GetComponent<CanvasGroup>();
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

