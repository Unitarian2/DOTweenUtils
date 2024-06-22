# DOTweenUtils

 # <b>ÖNEMLİ NOT :</b> Bu paketin DOTween pakedine dependency'si vardır. Lütfen önce DOTween pakedini projenize yükleyin. <br>
## _https://assetstore.unity.com/publishers/1341_

## Usage
- Script'i kullanmak istediğiniz UI Gameobject'e ekleyin.
- performOnStart seçeneğini TRUE yaparsanız, Gameobject'in Start metodunda animasyon otomatik olarak başlatılır.
- Perform/Stop metodlarını kullanarak kod üzerinden de animasyonu başlatabilir veya durdurabilirsiniz.
```sh
public class ExampleScript : MonoBehaviour
{  
    public GameObject slider;
    public GameObject panel;
    public GameObject icon;


    void Start()
    {
        //Value 1 will fully fill the slider.
        //For example, if you send 1.4f, slider will animate to 1f first, and then will zero out itself and lastly animato to 0.4f
        var sliderAnimator  = slider.GetComponent<SliderRunToValue>();
        sliderAnimator.Perform()
            .SetValue(1.5f)
            .OnCompleted(() => 
            { 
                //Do Something here as a callback
            });


        //Triggers OffScreenToAppear Animation.
        var panelOffToAppear = panel.GetComponent<OffScreenToAppear>();
        panelOffToAppear.Perform()
            .OnCompleted(() =>
            {
                //Do Something here as a callback
            });

        //Triggers DisappearToOffScreen Animation.
        var panelDisappearToOff = panel.GetComponent<DisappearToOffScreen>();
        panelDisappearToOff.Perform()
            .OnCompleted(() =>
            {
                //Do Something here as a callback
            });

        //Triggers StaticAppear Animation.
        var panelstaticAppear = panel.GetComponent<StaticAppear>();
        panelstaticAppear.Perform()
            .OnCompleted(() =>
            {
                //Do Something here as a callback
            });

        //Triggers StaticDisappear Animation.
        var panelStaticDisappear = panel.GetComponent<StaticDisappear>();
        panelStaticDisappear.Perform()
            .OnCompleted(() =>
            {
                //Do Something here as a callback
            });


        //Rotates this element forever
        var iconRotate = icon.GetComponent<RotateForever>();
        iconRotate.Perform();

        //Rotates and pulsates this element forever
        var iconRotatePulsate = icon.GetComponent<RotateAndPulsate>();
        iconRotatePulsate.Perform();

        //Floates this element to a designated direction back and forth(like and arrow icon)
        var iconFloatToDirection = icon.GetComponent<FloatingToDirection>();
        iconFloatToDirection.Perform();


        //You can stop the animations whenever you want.
        //Stop method will Stop the animation immediately, does not complete the animation loop so, don't forget to reset the values after calling it.
        iconRotate.Stop();
        panelStaticDisappear.Stop();
    }

    
}
```


