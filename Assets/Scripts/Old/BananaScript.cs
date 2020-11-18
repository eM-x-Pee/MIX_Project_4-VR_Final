using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
public class BananaScript : MonoBehaviour
{
    //this script is attached to objects so they know what to do when a button was pressed

    //this event sends out the transform of the object that was touched
    public delegate void DescriptionRequest(Transform objectPosition); //the event will send out the name of which button was pressed
    public static event DescriptionRequest onDescriptionRequest;

    private Color newColor = new Color(1.0f, 1.0f, 1.0f, 1.0f); // change this to desired color
    private Renderer rend = null;

    public GameObject[] replacementObjects;

    [SerializeField]
    private GameObject scalerObject;

    private void Awake()
    {
        rend = GetComponent<Renderer>(); // gives functionality for the renderer
        rend.enabled = true; //makes the rendered 3d object visible if enabled
    }

    private void OnEnable()
    {
        ////start minimized
        //scalerObject.transform.localScale = new Vector3(0, 0, 0);
    }

    public void Update()
    {
      

        // returns a float of the Hand Trigger’s current state on the Left Oculus Touch controller.
        var LeftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch);

        // returns a float of the Hand Trigger’s current state on the Right Oculus Touch controller.
        var RightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch);


        if (LeftTrigger != 0f)
        {
            onDescriptionRequest?.Invoke(this.gameObject.transform);
        }
        else
        if (RightTrigger != 0f)
        {
            onDescriptionRequest?.Invoke(this.gameObject.transform);
        }
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "Finger")
    //    {
    //        Debug.Log("Object touched! Description pop up... how about rumble?");
    //        VibrationManager.singleton.TriggerVibration(30, 2, 255, OVRInput.Controller.Touch);


    //        //tell observers where the object is, the DescriptionController will make a pop up
    //        onDescriptionRequest?.Invoke(this.gameObject.transform);
    //    }
    //}
    public void resetScene()
    {

        // returns a float of the Hand Trigger’s current state on the Left Oculus Touch controller.
        var LeftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch);

        // returns a float of the Hand Trigger’s current state on the Right Oculus Touch controller.
        var RightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch);


        if (LeftTrigger != 0f)
        {
            SceneManager.LoadScene("Main");
        }
        else
        if (RightTrigger != 0f)
        {
            SceneManager.LoadScene("Main");
        }
    }
    public void ColorChanger()
    {
        if ((rend != null))
        {
            newColor = new Color(Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f));
            Debug.Log("New color: " + newColor);

            rend.material.color = newColor;
        }
    }

    public void openScaler(Transform spawnPos)
    {
        Debug.Log("Controller: Bringing up scaler... did rumble work?");
        VibrationManager.singleton.TriggerVibration(30, 2, 255, OVRInput.Controller.Touch);

        //pop out of the spawn point (like the description)
        LeanTween.scale(spawnPos.gameObject, new Vector3(0.001f, 0.001f, 1.23f), 0.5f);
    }

    public void replaceObject()
    {
        //int randomID = Random.Range(0, replacementObjects.Length);
        //Resources.Load<GameObject>(replacementObjects[randomID].name);

        Resources.Load<Mesh>("SM_Icon_Lock_Closed_01");

    }

    public void resizeObject(float value)
    {
        this.transform.localScale = transform.localScale * value;
    }

    public void toggleMe(bool hide)
    {
        if(hide == true)
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
        }

        if (hide == false)
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }

    }

    public void destroyMe()
    {
        Destroy(gameObject);
    }



}
