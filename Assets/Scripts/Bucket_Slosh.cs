using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket_Slosh : MonoBehaviour
{
    public GameObject mLiquid;
    public GameObject mLiquidMesh;

    private int mSloshSpeed = 60;
    private int mRotateSpeed = 15;

    private int difference = 25;

    void Update()
    {
        //Motion
        Slosh();

        //Spin liquid while idle, remove if looks awkward
        //mLiquidMesh.transform.Rotate(Vector3.up * mRotateSpeed * Time.deltaTime, Space.Self);
    }

    private void Slosh()
    {
        //Inverse cup rotation
        Quaternion inverseRotation = Quaternion.Inverse(transform.localRotation);

        //rotate to
        Vector3 finalRotation = Quaternion.RotateTowards(mLiquid.transform.localRotation, inverseRotation, mSloshSpeed * Time.deltaTime).eulerAngles;

        //clamp in place
        finalRotation.x = ClampRotationValue(finalRotation.x, difference);
        finalRotation.z = ClampRotationValue(finalRotation.z, difference);

        //set 
        mLiquid.transform.localEulerAngles = finalRotation;

    }

    private float ClampRotationValue(float value, float difference)
    {
        float returnValue = 0f;

        if (value > 180)
        {
            //clamp in place
            returnValue = Mathf.Clamp(value, 360 - difference, 360);

        }
        else
        {
            //clamp in place
            returnValue = Mathf.Clamp(value, 0, difference);

        }

        return returnValue;
    }
}
