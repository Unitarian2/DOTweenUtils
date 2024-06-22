using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace DOTweenUtils
{
    /// <summary>
    /// Sets the value of the slider to between 0-1f.
    /// If you set the value to 1.5f for example, it will reach to 1f first, and then set the slider to 0, then runs it to 0.5f(1.5f - 1f)
    /// Example Usage sliderRunToValue.Perform().SetValue(0.5f).OnCompleted(() =>{});
    /// Do not forget that this is not an value addition, you set the final value of the slider using this component.
    /// If you want to add certain value => sliderRunToValue.Perform().SetValue(slider.value + valueToAdd).OnCompleted(() =>{});
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class SliderRunToValue : DOTweenUtilsItemValueFeed<SliderRunToValue>
    {
        [SerializeField] private float animTime = 1f;

        Sequence mySequence;
        Slider slider;
        public float value = 1f;
        float remainingValue = 0f;
        bool isRunning;


        public override SliderRunToValue Perform()
        {
            slider = GetComponent<Slider>();

            remainingValue = value;
            isRunning = true;
            DoSequence();

            return this;
        }

        private void DoSequence()
        {
            mySequence?.Kill(false);

            mySequence = DOTween.Sequence();


            mySequence.Append(slider.DOValue(remainingValue, animTime)).OnComplete(() =>
            {

                if (remainingValue > 1f)
                {
                    remainingValue = remainingValue - 1f;
                    slider.value = 0f;
                    if (isRunning) DoSequence();
                }
                else
                {
                    OnComplete?.Invoke();
                    OnComplete = null;
                }

            });
        }

        public override void Stop(bool complete)
        {
            isRunning = false;
            mySequence?.Kill(complete);
        }

        /// <summary>
        /// Chain this method to feed the slider value to Run to.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override SliderRunToValue SetValue(float value)
        {
            this.value = value;
            return this;
        }


        private void OnDestroy()
        {
            Stop(false);
        }
    }
}

