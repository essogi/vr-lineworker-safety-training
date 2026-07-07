using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HighlightControl : MonoBehaviour
{
    public bool allwaysOn = false;

    //is the pole picked up (only used for pole)
    public bool pickedup = false;
    //are the highlights active
    public bool InScenario = false;


    // Start is called before the first frame update
    void Start()
    {
        if (!allwaysOn)
        {
        GetComponent<MeshRenderer>().enabled = false;   
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleActive()
    {
        if (InScenario)
        {
            GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled;
        }
       
    }



    /// <summary>
    /// Specific for the Rod
    /// </summary>
    public void OnActive()
    {
        //Debug.Log("test");
        if (!pickedup && InScenario)
        {
            //Debug.Log("test2");
            GetComponent<MeshRenderer>().enabled = true;
        }
        
    }

    public void OffActive()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    public void PickUpSetter()
    {
        pickedup = !pickedup;
    }

    public void TurnOffGuide()
    {
        InScenario = false;
        GetComponent<MeshRenderer>().enabled = false;
    }
    public void TurnOnGuide()
    {
        InScenario = true;
        if (allwaysOn)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
