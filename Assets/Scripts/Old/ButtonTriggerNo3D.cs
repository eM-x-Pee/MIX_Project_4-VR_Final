using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerNo3D : MonoBehaviour
{

    public delegate void MenuButtonPress(string whichButton);
    public static event MenuButtonPress onMenuButtonPress; //event to subscribe to, static so it can be used in other classes

    private bool pressedInProgress = false;


    void Update()
    {
        OVRInput.Update();
        OVRInput.FixedUpdate();

        // returns a float of the Hand Trigger’s current state on the Left Oculus Touch controller.
        var LeftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch);

        // returns a float of the Hand Trigger’s current state on the Right Oculus Touch controller.
        var RightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch);


        if (LeftTrigger != 0f)
        {
            onMenuButtonPress?.Invoke(this.gameObject.name);
        }
        else
        if (RightTrigger != 0f)
        {
            onMenuButtonPress?.Invoke(this.gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ButtonActivator" && !pressedInProgress)
        {
            pressedInProgress = true;
            //tell observers about which button was pressed
            onMenuButtonPress?.Invoke(this.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ButtonActivator")
        {
            pressedInProgress = false;
        }
    }
}
