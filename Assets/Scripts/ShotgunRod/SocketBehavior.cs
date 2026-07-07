using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketBehavior : XRSocketInteractor
{
    
    [System.NonSerialized] public bool socketAttatched = false;
        
    /*public void changeSocketSize()
    {
        Debug.Log("Here");
        XRGrabInteractable grabInteractable = GetComponentInChildren<XRGrabInteractable>();
        gameObject.transform.position = grabInteractable.transform.position;
        Bounds bounds = grabInteractable.GetComponent<Collider>().bounds;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = bounds.size;
    }*/
    [SerializeField] private GameObject highlight;
    protected override void OnHoverEntering(XRBaseInteractable interactable)
    {
        if (interactable.name == "PowerMeter")
        {
            interactableHoverScale = 1.0f;
        }
        highlight.GetComponent<HighlightControl>().OffActive();
        base.OnHoverEntering(interactable);
    }

    protected override void OnHoverExited(XRBaseInteractable interactable)
    {
        if (interactable.name == "PowerMeter")
        {
            interactableHoverScale = 2.0f;
        }
        highlight.GetComponent<HighlightControl>().OnActive();
        base.OnHoverExited(interactable);
    }

    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        if (interactable is XRGrabInteractable grabInteractable)
        {
            //Debug.Log("Here");
            Bounds bounds = grabInteractable.GetComponent<Collider>().bounds;
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            //gameObject.transform.position = grabInteractable.transform.position;
            float y = (bounds.size.y - boxCollider.size.y);
            //Debug.Log(y);
            gameObject.transform.localPosition += new Vector3(0f, y/8f, 0f);
            boxCollider.size = bounds.size;
            socketAttatched = true;
        }
        base.OnSelectEntered(interactable);
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        socketAttatched = false;
        highlight.GetComponent<HighlightControl>().OnActive();
        base.OnSelectExited(interactable);
    }
}
