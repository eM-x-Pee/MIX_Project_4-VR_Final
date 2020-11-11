using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    //this should be the only vibration manager
    public static VibrationManager singleton;


    void Start()
    {
        if(singleton && singleton != this)
        {
            Destroy(this);
        }
        else{
            singleton = this;
        }
    }

    public void TriggerVibration(int iteration, int frequency, int strength, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip();

        for(int i=0; i<iteration; i++)
        {
            clip.WriteSample(i % frequency == 0 ? (byte)strength : (byte)0);
        }

        if(controller == OVRInput.Controller.LTouch)
        {
            //trigger on left controller
            OVRHaptics.LeftChannel.Preempt(clip);

        }
        else if (controller == OVRInput.Controller.RTouch)
        {
            //trigger on right controller
            OVRHaptics.RightChannel.Preempt(clip);

        }
    }
}
