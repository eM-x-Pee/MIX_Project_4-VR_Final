using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MyVRRaycaster : MonoBehaviour
{

    [System.Serializable]
    public class Callback : UnityEvent<Ray, RaycastHit> { }

    public Transform leftHandAnchor = null;
    public Transform rightHandAnchor = null;
    public Transform centerEyeAnchor = null;
    public LineRenderer lineRenderer = null;
    public float maxRayDistance = 500.0f;
    public LayerMask excludeLayers;

    public MySelectable selected = null;

    void Awake()
    {
        if (leftHandAnchor == null)
        {
            Debug.LogWarning("Assign LeftHandAnchor in the inspector!");
            GameObject left = GameObject.Find("LeftHandAnchor");
            if (left != null)
            {
                leftHandAnchor = left.transform;
            }
        }
        if (rightHandAnchor == null)
        {
            Debug.LogWarning("Assign RightHandAnchor in the inspector!");
            GameObject right = GameObject.Find("RightHandAnchor");
            if (right != null)
            {
                rightHandAnchor = right.transform;
            }
        }
        if (centerEyeAnchor == null)
        {
            Debug.LogWarning("Assign CenterEyeAnchor in the inspector!");
            GameObject center = GameObject.Find("CenterEyeAnchor");
            if (center != null)
            {
                centerEyeAnchor = center.transform;
            }
        }
        if (lineRenderer == null)
        {
            Debug.LogWarning("Assign a line renderer in the inspector!");
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineRenderer.receiveShadows = false;
            lineRenderer.widthMultiplier = 0.02f;
        }
    }

    void Update()
    {
        Transform pointer = rightHandAnchor;
        if (pointer == null)
        {
            return;
        }

        Ray laserPointer = new Ray(pointer.position, pointer.forward);

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, laserPointer.origin);
            lineRenderer.SetPosition(1, laserPointer.origin + laserPointer.direction * maxRayDistance);
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            RaycastHit hit;
            if (Physics.Raycast(laserPointer, out hit, maxRayDistance, ~excludeLayers))
            {
                var obj = hit.transform.GetComponent<MySelectable>();

                if (lineRenderer != null)
                {
                    lineRenderer.SetPosition(1, hit.point);
                }

                if (selected != null)
                {
                    selected.OnDeselect();
                    selected = null;
                }

                if (obj != null)
                {
                    selected = obj;
                    selected.OnSelect();
                }
            }
        }
    }

    public void ScaleSelectedX(Slider slider)
    {
        var newScale = selected.transform.localScale;
        newScale.x = slider.value / 100;
        selected.transform.localScale = newScale;
    }
    public void ScaleSelectedY(Slider slider)
    {
        var newScale = selected.transform.localScale;
        newScale.y = slider.value / 100;
        selected.transform.localScale = newScale;
    }
    public void ScaleSelectedZ(Slider slider)
    {
        var newScale = selected.transform.localScale;
        newScale.z = slider.value / 100;
        selected.transform.localScale = newScale;
    }
}