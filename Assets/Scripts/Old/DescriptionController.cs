using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionController : MonoBehaviour
{
    private Transform sourceObject;
    private string parentGameObjectName;

    [SerializeField]
    private TextMeshProUGUI headerText = null;

    [SerializeField]
    private TextMeshProUGUI descriptionText = null;

    public delegate void showMeTheMenu(Transform theObjectsMenu);
    public static event showMeTheMenu openMenu;

    private void OnEnable()
    {
        //sub event, listen for if an object is pressed
        BananaScript.onDescriptionRequest += openDescription;

        //store where object was originally
        sourceObject = transform.parent.transform;

        //start description minimized
        transform.localScale = new Vector3(0, 0, 0);

        //load right description based on object name
        switch (sourceObject.name)
        {
            case "Banana":
                Debug.Log("Loading Banana description...");
                descriptionText.text = "A banana is an elongated, edible fruit – botanically a berry – produced by several kinds of large herbaceous flowering plants in the genus Musa.";
                headerText.text = "Banana";

                break;
            case "X":
                Debug.Log("Loading X description...");
                descriptionText.text = "X";
                headerText.text = "X";

                break;
            case "Y":
                Debug.Log("Loading Y description...");
                descriptionText.text = "Y";
                headerText.text = "Y";

                break;
            case "Z":
                Debug.Log("Loading Z description...");
                descriptionText.text = "Z";
                headerText.text = "Z";

                break;
            default:
                Debug.Log("No gameobject found with that name");
                descriptionText.text = "Object name not found in switch case, check DescriptionController and make sure the name appears as a case.";
                headerText.text = "Huh?";
                break;
        }

    }

    void OnDestroy()
    {
        //unsub event
        BananaScript.onDescriptionRequest -= openDescription;
    }

    void openDescription(Transform sourceObject)
    {
        this.gameObject.SetActive(true);

        Debug.Log("Controller: Bringing up description... did rumble work?");
        VibrationManager.singleton.TriggerVibration(30, 2, 255, OVRInput.Controller.Touch);

        //make the window pop out of the source object
        LeanTween.scale(gameObject, new Vector3(0.002f, 0.002f, 0.002f), 0.5f);
    }

    public void openMenuButton()
    {
        Debug.Log("Show me the menu! Did rumble work?");
        VibrationManager.singleton.TriggerVibration(30, 2, 255, OVRInput.Controller.Touch);

        //tell menu controller to open menu for the parent object of this script (Banana for testing)
        openMenu?.Invoke(this.gameObject.transform.parent);

    }

    public void closeDescription()
    {
        //make the window minimize into the object it spawned from
        Debug.Log("Dismiss clicked. Did rumble work?");
        VibrationManager.singleton.TriggerVibration(30, 2, 255, OVRInput.Controller.Touch);


        if (sourceObject != null)
        {
            Debug.Log("Dismiss success");
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f);
        }

    }

    void DestroyMe()
    {
        Debug.Log("Destroy Description called");
        //Destroy(gameObject);

        this.gameObject.SetActive(false);
    }


}
