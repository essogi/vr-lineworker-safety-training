using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class HandleMovement : MonoBehaviour
{


    private Vector3 StartLocation;

    [SerializeField] Vector2 ZForwordBackMaxMin; //front is the direction the handle is pointing
    [SerializeField] Vector2 XSidewaysMaxMin; //Note that towards the user is positive
    [SerializeField] Vector2 YUpDownewaysMaxMin;

    [SerializeField] Transform handleSnapPointRight;
    [SerializeField] Transform handleSnapPointLeft;

    [SerializeField] GameObject GridObject;

    private Transform HandModel;
    private Transform NormalHandLocation;

    private Transform Holder; //what is currently holding the object

    private Transform DotLocation; //the dot item on the hand
    private Vector3 HandStartLoc;
    //get value of amount of distance that the handle has moved to max
    public float percentX;
    public float percentY;
    public float percentZ;

    [SerializeField] private UnityEvent IsHandleGrabed;



    public bool held = false;

    public Vector3 PositionFromParent;

    // Start is called before the first frame update
    void Start()
    {
        GridObject.SetActive(false);
        XRSimpleInteractable grabbable = GetComponent<XRSimpleInteractable>();
        grabbable.selectEntered.AddListener(click);
        grabbable.selectExited.AddListener(ReleaseItem);

        //current location
        StartLocation = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Handle position update
        if (held)
        {

            float temp;
            //relitive location in local space
            Vector3 positionInLocal = transform.parent.InverseTransformPoint(Holder.position);
            float HandReletiveX = positionInLocal.x;
            float HandReletiveY = positionInLocal.y;
            float HandReletiveZ = positionInLocal.z;


            float XDistanceFromStart = HandReletiveX - HandStartLoc.x;
            float YDistanceFromStart = HandReletiveY - HandStartLoc.y;
            float ZDistanceFromStart = HandReletiveZ - HandStartLoc.z;
            //check which value is the highest and move the handle in that direction.
            if (Mathf.Abs(XDistanceFromStart) > Mathf.Abs(YDistanceFromStart) && Mathf.Abs(XDistanceFromStart) > Mathf.Abs(ZDistanceFromStart))
            {
                float HandleX = Mathf.Clamp(StartLocation.x + XDistanceFromStart, XSidewaysMaxMin.x, XSidewaysMaxMin.y);
                transform.localPosition = new Vector3(HandleX, 0, 0);
                temp = Mathf.InverseLerp(XSidewaysMaxMin.x, XSidewaysMaxMin.y, HandleX);
                percentX = Mathf.Lerp(-1, 1, temp);
                percentY = 0;
                percentZ = 0;
            }
            else if (Mathf.Abs(YDistanceFromStart) > Mathf.Abs(ZDistanceFromStart))
            {
                float HandleY = Mathf.Clamp(StartLocation.y + YDistanceFromStart, YUpDownewaysMaxMin.x, YUpDownewaysMaxMin.y);
                transform.localPosition = new Vector3(0, HandleY, 0);
                temp = Mathf.InverseLerp(YUpDownewaysMaxMin.x, YUpDownewaysMaxMin.y, HandleY);
                percentY = Mathf.Lerp(-1, 1, temp);
                percentZ = 0;
                percentX = 0;
            }
            else
            {
                float HandleZ = Mathf.Clamp(StartLocation.z + ZDistanceFromStart, ZForwordBackMaxMin.x, ZForwordBackMaxMin.y);
                transform.localPosition = new Vector3(0, 0, HandleZ);
                temp = Mathf.InverseLerp(ZForwordBackMaxMin.x, ZForwordBackMaxMin.y, HandleZ);
                percentZ = Mathf.Lerp(-1, 1, temp);
                percentX = 0;
                percentY = 0;
            }
        }
        else
        {
            percentX = 0;
            percentY = 0;
            percentZ = 0;
        }
    }

    public void click(SelectEnterEventArgs args)
    {
        Transform Hand = args.interactorObject.transform;

        if (held) //only one hand
        {
            return;
        }

        IsHandleGrabed.Invoke();

        held = true;
        Holder = Hand;
        HandModel = Hand.GetChild(0);
        NormalHandLocation = Hand.GetChild(1);
        DotLocation = Hand.GetChild(2);
        DotLocation.GetComponent<HandLaser>().IsActive = true;
        //Hand.GetChild(0).parent = transform;

        HandStartLoc = transform.parent.InverseTransformPoint(Hand.position);

        if (Hand.CompareTag("RightHand"))
        {
            HandModel.parent = handleSnapPointRight;
            HandModel.position = handleSnapPointRight.position;
            HandModel.rotation = handleSnapPointRight.rotation;
        }
        else if (Hand.CompareTag("LeftHand"))
        {
            HandModel.parent = handleSnapPointLeft;
            HandModel.position = handleSnapPointLeft.position;
            HandModel.rotation = handleSnapPointLeft.rotation;
        }

        GridObject.transform.localPosition = transform.parent.InverseTransformPoint(DotLocation.position); //move location of the grid to start location of the hand point
        GridObject.SetActive(held);


    }
    public void ReleaseItem(SelectExitEventArgs args)
    {
        transform.localPosition = StartLocation;
        IsHandleGrabed.Invoke();
        //change models
        HandModel.parent = Holder;
        HandModel.SetSiblingIndex(0); // make sure its first in the list again so the program works
        DotLocation.GetComponent<HandLaser>().IsActive = false;
        HandModel.position = NormalHandLocation.position;
        HandModel.rotation = NormalHandLocation.rotation;
        Holder = null;
        HandModel = null;
        NormalHandLocation = null;
        held = false;

        GridObject.SetActive(held);
    }



}
