using DG.Tweening;

using UnityEngine;

namespace DOTweenUtils
{
    [RequireComponent(typeof(CanvasGroup))]
    public class StaticAppear : DOTweenUtilsItem<StaticAppear>
    {
        [SerializeField] private float animTime = 0.25f;
        [SerializeField] private Ease scaleEaseType = Ease.OutBack;

        Sequence mySequence;
        CanvasGroup cg;

        public override StaticAppear Perform()
        {
            Stop(false);

            transform.localScale = Vector3.zero;
            cg.alpha = 0f;
            gameObject.SetActive(true);

            mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOScale(Vector3.one, animTime).SetEase(scaleEaseType))
                .Insert(0, cg.DOFade(1f, animTime).SetEase(Ease.Linear))
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

