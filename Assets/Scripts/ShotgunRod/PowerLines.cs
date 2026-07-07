using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PowerLines : XRSocketInteractor
{
    public Tracker simTracker;
    public bool isHighlighted;
    public int lineNumber;

    private bool aboutToHook = false;
    private Vector3 hook;

    private void Update()
    {
        if(aboutToHook)
        {
            transform.GetChild(0).position = new Vector3(hook.x, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Hook 1" || other.name == "Hook 2")
        {
            //Debug.LogError("triggered");
            socketActive = true;
        }
        base.OnTriggerEnter(other);
    }

    protected override void OnHoverEntered(XRBaseInteractable interactable)
    {
        if (interactable.name == "PowerMeter")
        {
            socketActive = false;
            showInteractableHoverMeshes = false;
            simTracker.trackEvent("Line " + lineNumber + " | Power Check");
        }
        else if (interactable.name == "Hook 1" || interactable.name == "Hook 2")
        {
            hook = interactable.transform.position;
            aboutToHook = true;
            showInteractableHoverMeshes = true;
        }
        base.OnHoverEntered(interactable);
    }

    protected override void OnHoverExited(XRBaseInteractable interactable)
    {
        if (interactable.name == "Hook 1" || interactable.name == "Hook 2")
        {
            //hook.position = interactable.transform.position;
            aboutToHook = false;
        }
        base.OnHoverExited(interactable);
    }

    protected override void OnSelectEntering(XRBaseInteractable interactable)
    {
        if (interactable.name != "Hook 1" && interactable.name != "Hook 2")
        {
            return;
        }
        interactable.transform.parent.parent.parent = null;
        interactable.GetComponent<XRGrabInteractable>().enabled = false;
        interactable.transform.position = transform.GetChild(0).position;
        interactable.GetComponent<Rigidbody>().isKinematic = true;
        interactable.GetComponent<Rigidbody>().freezeRotation = true;
        simTracker.trackEvent("Line " + lineNumber + " | " + interactable.transform.parent.parent.name + " | " + interactable.name);
        Invoke("setInactive", 1f);
        base.OnSelectEntering(interactable);
    }

    private void setInactive()
    {
        socketActive = false;
    }
}
