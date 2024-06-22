# DOTweenUtils

 # IMPORTANT NOTE: This package has a dependency on the DOTween package. Please make sure to install the DOTween package in your project first. <br>
## _https://assetstore.unity.com/publishers/1341_

## Usage
- Add the script to the UI GameObject you want to use it with as a component.
- If you set the performOnStart option to TRUE, the animation will automatically start in the GameObject's Start method so, you don't have to start it manually.
- You can also start or stop the animation programmatically using the Perform/Stop methods.
```sh
public class ExampleScript : MonoBehaviour
{  
    public GameObject slider;
    public GameObject panel;
    public GameObject icon;


    void Start()
    {
        //Value 1 will fully fill the slider.
        //For example, if you send 1.4f, slider will animate to 1f first, and then will zero out itself and lastly animation to 0.4f
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


