using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    public float currentWaterLevel = 0; //will be used to measure for how long you can pour water
    private float maxWaterLevel = 3;

    private bool corIsPaused = false;

    private bool isPouring = false;
    private WaterStreamScript currentStream = null;


    private void OnTriggerEnter(Collider other)
    {
        //if you hit water, fill the bucket up with 5 sec worth of water
        if (other.gameObject.name == "Lake" || other.gameObject.tag == "Lake")
        {
            print("Bucket filled with water");
            currentWaterLevel = maxWaterLevel;
        }
    }

    private IEnumerator PouringCountDown(float waterLeft)
    {
        while (!corIsPaused)
        {
            if (waterLeft == 0)
            {
                Debug.Log("You have run out of water!");
                currentWaterLevel = 0;
                EndPour();

                //stop the coroutine since we are out of water, has to be restarted
                yield break;
            }

            else
            {
                Debug.Log("Water left in bucket: " + waterLeft);
                currentWaterLevel = waterLeft;
                yield return new WaitForSeconds(1.0f);
                waterLeft--;
            }
        }
    }

    private void Update()
    {
        //for checking if we reached the angle for pouring
        bool pourCheck = CalculatePourAngle() < pourThreshold;

        //can't have more water in the bucket than the max
        if (currentWaterLevel > maxWaterLevel)
        {
            currentWaterLevel = maxWaterLevel;
        }

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring && currentWaterLevel > 0)
            {
                //there is water in the bucket and it is tipped, so pour it out and the coroutine tracks how much water is left.
                StartPour();
                corIsPaused = false;

                StartCoroutine(PouringCountDown(currentWaterLevel)); //start counting down the seconds you can pour
            }
            else
            {
                if (currentStream != null)
                {
                    EndPour();
                    print("pausing coroutine");

                    corIsPaused = true; //stop counting down
                }
            }
        }
    }

    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
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