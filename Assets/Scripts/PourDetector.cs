using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private WaterStreamScript currentStream = null;

    private void Update()
    {
        bool pourCheck = CalculatePourAngle() < pourThreshold;

        if(isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }

        }
    }

    private void StartPour()
    {
        print("Starting pour...");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        print("End pour.");

        currentStream.End();
        currentStream = null;

    }

    private float CalculatePourAngle()
    {
        //return the degrees it is being tilted forward
        return transform.forward.y * Mathf.Rad2Deg; //transform.up if forward not OK
    }

    private WaterStreamScript CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);

        return streamObject.GetComponent<WaterStreamScript>();
    }

}