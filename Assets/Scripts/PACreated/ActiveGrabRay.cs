using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActiveGrabRay : MonoBehaviour
{
    [SerializeField] GameObject leftGrabRay;
    [SerializeField] GameObject rightGrabRay;
    [SerializeField] XRDirectInteractor leftDirectGrab;
    [SerializeField] XRDirectInteractor rightDirectGrab;
  

    // Update is called once per frame
    void Update()
    {
        leftGrabRay.SetActive(leftDirectGrab.interactablesSelected.Count == 0);
        rightGrabRay.SetActive(rightDirectGrab.interactablesSelected.Count == 0);
    }
}
