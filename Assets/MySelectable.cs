using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySelectable : MonoBehaviour
{
    private bool _selected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect()
    {
        _selected = true;
        this.GetComponent<Renderer>().material.color = Color.green;
    }

    public void OnDeselect()
    {
        _selected = false;
        this.GetComponent<Renderer>().material.color = Color.white;
    }
}
