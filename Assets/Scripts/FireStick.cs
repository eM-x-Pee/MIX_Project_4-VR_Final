using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStick : MonoBehaviour
{

    Collider stickCollider;
    //fireprefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");


    // Start is called before the first frame update
    Renderer rend;
    void Start() 
    {
        stickCollider = GetComponent<Collider>();
        stickCollider.isTrigger = true;
    
    }


    // Update is called once per frame
    void Update() { }
   
 //   void OnTriggerStay(Collider other)
  //  {
  //      if (other.attachedRigidbody)
   //     {
            // destroy stick
    //        Destroy(gameObject);

   //     }
  //  }

}
