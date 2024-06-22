using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DOTweenUtils
{
    [RequireComponent(typeof(Button))]
    public class ButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Ease scaleDownEaseType = Ease.Linear;
        [SerializeField] private float animTime = 0.05f;
        [SerializeField] private float scaleDownAmount = 0.8f;
        Tween downTween;
        Tween upTween;
        Button button;
        Vector3 startScale;

        private void Awake()
        {
            button = GetComponent<Button>();
            startScale = transform.localScale;
        }


        private void OnDestroy()
        {
            downTween?.Kill();
            upTween?.Kill();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            downTween?.Kill(false);

            downTween = transform.DOScale(startScale * scaleDownAmount, animTime).SetEase(scaleDownEaseType);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            downTween?.Kill(false);
            upTween = transform.DOScale(startScale, animTime).SetEase(scaleDownEaseType);
        }
    }

}
