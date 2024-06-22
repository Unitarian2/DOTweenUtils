using DG.Tweening;
using UnityEngine;
namespace DOTweenUtils
{
    [RequireComponent(typeof(CanvasGroup))]
    public class OffScreenToAppear : DOTweenUtilsItem<OffScreenToAppear>
    {
        public enum AppearMoveType
        {
            Top, Bottom, Left, Right
        }

        [SerializeField] private float animTime = 0.5f;
        [SerializeField] private Ease moveEaseType = Ease.OutCubic;
        [SerializeField] private AppearMoveType appearFrom;

        Sequence mySequence;
        CanvasGroup cg;

        public override OffScreenToAppear Perform()
        {
            Stop(false);

            cg.alpha = 0f;
            gameObject.SetActive(true);

            #region Start Pos Setup
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

            Vector2 targetPos = rectTransform.anchoredPosition;

            switch (appearFrom)
            {
                case AppearMoveType.Left:
                    MoveToLeft(rectTransform, canvasRectTransform);
                    break;
                case AppearMoveType.Right:
                    MoveToRight(rectTransform, canvasRectTransform);
                    break;
                case AppearMoveType.Top:
                    MoveToTop(rectTransform, canvasRectTransform);
                    break;
                case AppearMoveType.Bottom:
                    MoveToBottom(rectTransform, canvasRectTransform);
                    break;
            };
            #endregion

            #region Anim
            mySequence = DOTween.Sequence();
            mySequence.Append(rectTransform.DOAnchorPos(targetPos, animTime).SetEase(moveEaseType))
                .Insert(0f, cg.DOFade(1f, animTime))
                .OnComplete(() =>
                {
                    OnComplete?.Invoke();
                    OnComplete = null;
                });
            #endregion

            return this;
        }

        private void MoveToLeft(RectTransform rectTransform, RectTransform canvasRectTransform)
        {
            Vector2 targetPos = rectTransform.anchoredPosition;
            targetPos.x = -canvasRectTransform.rect.width / 2 + rectTransform.rect.width * rectTransform.pivot.x;
            rectTransform.anchoredPosition = targetPos;
        }

        private void MoveToRight(RectTransform rectTransform, RectTransform canvasRectTransform)
        {
            Vector2 targetPos = rectTransform.anchoredPosition;
            targetPos.x = canvasRectTransform.rect.width / 2 - rectTransform.rect.width * (1 - rectTransform.pivot.x);
            rectTransform.anchoredPosition = targetPos;
        }

        private void MoveToTop(RectTransform rectTransform, RectTransform canvasRectTransform)
        {
            Vector2 targetPos = rectTransform.anchoredPosition;
            targetPos.y = canvasRectTransform.rect.height / 2 - rectTransform.rect.height * (1 - rectTransform.pivot.y);
            rectTransform.anchoredPosition = targetPos;
        }

        private void MoveToBottom(RectTransform rectTransform, RectTransform canvasRectTransform)
        {
            Vector2 targetPos = rectTransform.anchoredPosition;
            targetPos.y = -canvasRectTransform.rect.height / 2 + rectTransform.rect.height * rectTransform.pivot.y;
            rectTransform.anchoredPosition = targetPos;
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

