using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))] //without a collider this won't work.
public class ButtonTrigger : MonoBehaviour
{
    //this script is for detecting when the button reaches the trigger zone
    //when it does it should trigger an event

    //the event will send out the name of which button was pressed
    public delegate void MenuButtonPress(string whichButton); 
    public static event MenuButtonPress onMenuButtonPress; //event to subscribe to, static so it can be used in other classes

    private bool pressedInProgress = false;

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
