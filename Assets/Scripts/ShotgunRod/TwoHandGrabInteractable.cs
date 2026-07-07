using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrabInteractable : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();
    private XRBaseInteractor secondInteractor;
    private Quaternion attachInitialRotation;
    public Transform returnal;
    private Vector3 originalScale;

    private float timeOnGround = 0f;
    public enum TwoHandRotationType { None, First, Second};
    public TwoHandRotationType twoHandRotationType;
    public Tracker simTracker;
    public GameObject rod;
    public Transform firstHandPoint;
    private Transform secondHandPoint;
    private Transform socket;

    [SerializeField] private GameObject highlight;
    [SerializeField] private UnityEvent PowerLineHighlight;

    private bool grabbed = false;
    private float rodInitialScale;

    public bool returnToStart = false;
    

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in secondHandGrabPoints)
        {
            item.onSelectEntered.AddListener(OnSecondHandGrab);
            item.onSelectExited.AddListener(OnSecondHandRelease);
        }
        originalScale = rod.transform.localScale;
        rodInitialScale = rod.transform.localScale.y;
        secondHandPoint = rod.transform.Find("SecondAttatchPoint");
        socket = rod.transform.Find("Socket");
    }

    // Update is called once per frame
    void Update()
    {
        if (!selectingInteractor)
        {
            //Debug.Log(transform.position);
            if (transform.position.y < returnal.position.y-0.2f && returnToStart)
            {
                //Debug.Log(timeOnGround);
                timeOnGround -= Time.deltaTime;
                if (timeOnGround < 0f)
                {
                    timeOnGround = 0f;
                    transform.GetComponent<Rigidbody>().velocity = new Vector3();
                    transform.GetComponent<Rigidbody>().angularVelocity = new Vector3();
                    transform.position = returnal.position;
                    transform.rotation = returnal.rotation;
                    transform.localScale = rod.transform.localScale;
                    //transform.GetComponent<Rigidbody>().rotation = originalRodRotation;
                    simTracker.droppedPole();
                }
            }
        }
    }
    
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {   
        if(secondInteractor && selectingInteractor)
        {
            //rod.transform.transform.localScale = new Vector3(rod.transform.localScale.x, rod.transform.localScale.y + extendFactor * (secondInteractor.transform.position - selectingInteractor.transform.position).sqrMagnitude, rod.transform.localScale.z);
            //Debug.Log(rod.transform.localScale);
            //Compute the rotation
            selectingInteractor.attachTransform.rotation = GetTwoHandRotation();
        }
        else if (selectingInteractor)
        {
            selectingInteractor.attachTransform.rotation = Quaternion.LookRotation(selectingInteractor.transform.up);
        }
        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation;
        if (twoHandRotationType == TwoHandRotationType.None)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
        }
        else if (twoHandRotationType == TwoHandRotationType.First)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.transform.up);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.transform.up);
        }
        return targetRotation;
    }

    public void OnSecondHandGrab(XRBaseInteractor interactor)
    {
        //Debug.Log("SECOND HAND GRAB");
        firstHandPoint.localPosition = firstHandPoint.localPosition - new Vector3(0f, 1.25f, 0f);
        secondInteractor = interactor;
    }

    public void OnSecondHandRelease(XRBaseInteractor interactor)
    {
        //Debug.Log("SECOND HAND RELEASE");
        firstHandPoint.localPosition = firstHandPoint.localPosition + new Vector3(0f, 1.25f, 0f);
        secondInteractor = null;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        
        //if (grabbed)
        
        
        attachInitialRotation = interactor.attachTransform.localRotation;
        grabbed = true;
        secondHandPoint.GetComponent<BoxCollider>().enabled = true;
        if (!socket.gameObject.GetComponent<SocketBehavior>().socketAttatched)
        {
            highlight.GetComponent<HighlightControl>().OnActive();
        }
        else
        {
            highlight.GetComponent<HighlightControl>().OffActive();
        }
        highlight.GetComponent<HighlightControl>().pickedup = true;
        PowerLineHighlight.Invoke();

        base.OnSelectEntered(interactor);
        //else
        //{
            //base.OnSelectExited(interactor);

            //secondInteractor = null;
            //interactor.attachTransform.localRotation = attachInitialRotation;
            //grabbed = false;
            //highlight.GetComponent<HighlightControl>().pickedup = false;
            //highlight.GetComponent<HighlightControl>().OffActive();
            //PowerLineHighlight.Invoke();
        //}

        


        //Debug.Log("First Grab Enter");


    }

    //protected override void OnSelect




    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        //Debug.Log("First Grab Exit");
        
        secondInteractor = null;
        secondHandPoint.GetComponent<BoxCollider>().enabled = false;
        interactor.attachTransform.localRotation = attachInitialRotation;
        grabbed = false;
        highlight.GetComponent<HighlightControl>().OffActive();
        highlight.GetComponent<HighlightControl>().pickedup = false;
        PowerLineHighlight.Invoke();
        base.OnSelectExited(interactor);
        //rod.transform.transform.localScale = new Vector3(rod.transform.localScale.x, rodInitialScale, rod.transform.localScale.z);
    }
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}
