using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour, IGrabbable
{
    public Transform SnapPointRight => handleSnapPointRight;
    public Transform SnapPointLeft => handleSnapPointLeft;


    [SerializeField] Transform handleSnapPointRight;
    [SerializeField] Transform handleSnapPointLeft;


    [SerializeField] MeshRenderer handleRender;
    [SerializeField] Material inRangeMat, closestOneMat, grabbedMat;

    Material defaultMat;

    public event Action OnEnteredRange = delegate { };
    public event Action OnExitRange = delegate { };
    public event Action OnSetClosestOne = delegate { };
    public Action<Transform> OnStartGrabbing = delegate { };
    public event Action OnStopGrabbing = delegate { };


    private void Awake()
    {
        defaultMat = handleRender.material;
    }
    public void EnterRange()
    {
        OnEnteredRange();
        handleRender.material = inRangeMat;
    }


    public void ExitRange()
    {
        handleRender.material = defaultMat;
        OnExitRange();
    }

    public void setClosestOne()
    {

    }
    public void startGrabbing(Transform handAnchor)
    {
        handleRender.material = grabbedMat;
        OnStartGrabbing(handAnchor);
    }

    public void StopGrabbing()
    {
        handleRender.material = inRangeMat;
        OnStopGrabbing();
    } 

}
