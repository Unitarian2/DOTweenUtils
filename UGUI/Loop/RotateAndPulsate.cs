using DG.Tweening;

using UnityEngine;

namespace DOTweenUtils
{
    [RequireComponent(typeof(CanvasGroup))]
    public class RotateAndPulsate : DOTweenUtilsItem<RotateAndPulsate>
    {
        [SerializeField] private float animTime = 1f;
        [SerializeField] private Ease rotationEaseType = Ease.Linear;
        [SerializeField] private Ease pulsateEaseType = Ease.InCubic;

        Sequence mySequence;
        CanvasGroup cg;

        public override RotateAndPulsate Perform()
        {

            cg = GetComponent<CanvasGroup>();
            gameObject.transform.localEulerAngles = Vector3.zero;
            cg.alpha = 1f;

            Stop(false);
            transform.rotation = Quaternion.identity;

            mySequence = DOTween.Sequence();

            mySequence.Append(transform.DORotate(new Vector3(0f, 0f, 360f), animTime, RotateMode.FastBeyond360).SetEase(rotationEaseType))
                .Insert(0, cg.DOFade(0f, animTime).SetEase(pulsateEaseType))
                .Append(transform.DORotate(new Vector3(0f, 0f, 360f), animTime, RotateMode.FastBeyond360).SetEase(rotationEaseType))
                .Insert(animTime, cg.DOFade(1f, animTime).SetEase(pulsateEaseType))
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

