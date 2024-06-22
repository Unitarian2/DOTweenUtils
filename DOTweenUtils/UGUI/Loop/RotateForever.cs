using DG.Tweening;

using UnityEngine;

namespace DOTweenUtils
{
    public class RotateForever : DOTweenUtilsItem<RotateForever>
    {
        [SerializeField] private float timeToSingleRotation = 1f;
        [SerializeField] private Ease rotationEaseType = Ease.Linear;

        Sequence mySequence;

        public override RotateForever Perform()
        {
            Stop(false);
            transform.rotation = Quaternion.identity;

            mySequence = DOTween.Sequence();

            mySequence.Append(transform.DORotate(new Vector3(0f, 0f, 360f), timeToSingleRotation, RotateMode.FastBeyond360).SetEase(rotationEaseType))
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

