using DG.Tweening;

using UnityEngine;

namespace DOTweenUtils
{
    [RequireComponent(typeof(CanvasGroup))]
    public class StaticDisappear : DOTweenUtilsItem<StaticDisappear>
    {
        [SerializeField] private float animTime = 0.25f;
        [SerializeField] private Ease scaleEaseType = Ease.InBack;

        Sequence mySequence;
        CanvasGroup cg;

        public override StaticDisappear Perform()
        {
            Stop(false);

            mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOScale(Vector3.zero, animTime).SetEase(scaleEaseType))
                .Insert(0, cg.DOFade(0f, animTime).SetEase(Ease.Linear))
                .OnComplete(() =>
                {
                    OnComplete?.Invoke();
                    OnComplete = null;
                });

            return this;
        }



        public override void Stop(bool complete)
        {
            mySequence?.Kill(complete);
        }

        private void Awake()
        {
            cg = GetComponent<CanvasGroup>();
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
