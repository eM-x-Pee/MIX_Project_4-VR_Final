using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLimiter : MonoBehaviour
{
    //this script is for making sure the button "activator" stays between limits

    [SerializeField] private GameObject buttonTrigger;
    private Vector3 originalPosition;
    private float minDistance;
    private float maxDistance;

    void Awake()
    {

        if(buttonTrigger == null)
        {
            Debug.Log("ERROR: No trigger assigned on button " + gameObject.name);
            buttonTrigger = null;
        }

        //calculate min distance between activator and trigger (highest it can come out of the base)
        minDistance = Vector3.Distance(buttonTrigger.transform.position, transform.position);

        //the deepest the button can be pressed
        maxDistance = buttonTrigger.transform.position.y;

        //we will want to know where to restore the position
        originalPosition = transform.position;

    }

    void Update()
    {
        //restore original position if it goes past the min distance
        if(Vector3.Distance(buttonTrigger.transform.position, transform.position) >= minDistance)
        {
            transform.position = originalPosition;
        }

        //restore position Y of the activator if <= max
        if(transform.position.y < maxDistance)
        {
            transform.position = new Vector3(transform.position.x, maxDistance, transform.position.z);
        }

    }

}
