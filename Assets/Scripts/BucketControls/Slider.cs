using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    [SerializeField] Handle handle;
    //[SerializeField] Mechanic control; //not needed

    [SerializeField] Vector2 handleStartAndEndLocatalZ;
    [SerializeField] Vector2 handleStartAndEndLocationX;

    [SerializeField] Vector2 outputRange;

    [SerializeField] float startValue;

    public event Action<float> OnNewSliderValue;

    bool isGrabbed;

    float offsetOnGrab;

    Vector3 handleLocalStartPos;

    //[field: SerializeReference] public SliderRestriction SliderRestriction { get; private set; }

    Transform activeGrabber;

    float handMovesSinceGrab;

    Vector3 handInLocalSpace;



    private void OnEnable()
    {
        handle.OnStartGrabbing += StartGrabbing;
        handle.OnStopGrabbing += StopGrabbing;
    }

    void SetStartPosition()
    {
        float transformRange = Math.Abs(handleStartAndEndLocatalZ.x - handleStartAndEndLocatalZ.y);
        var startPositionZ = handleStartAndEndLocatalZ.x + (transformRange * startValue);
        handleLocalStartPos = handle.transform.localPosition;
        handle.transform.localPosition = new Vector3(handleLocalStartPos.x, handleLocalStartPos.y, startPositionZ);

        OnNewSliderValue?.Invoke(startValue);

    }
    public void StartGrabbing(Transform grabber)
    {
        activeGrabber = grabber;
        isGrabbed = true;
        handleLocalStartPos = handle.transform.localPosition;
        handInLocalSpace = handle.transform.parent.InverseTransformPoint(grabber.position);
        offsetOnGrab = handInLocalSpace.z;
    }
    public void StopGrabbing()
    {

    }

    private void Update()
    {
        if (isGrabbed)
        {
            MoveToHand();
        }
    }

    public void MoveToHand()
    {
        //if (SliderRestriction == SliderRestriction.locked)
           // return;
        float newhandPosition = handle.transform.parent.InverseTransformDirection(activeGrabber.position).z;
        handMovesSinceGrab = newhandPosition - offsetOnGrab;

        float newZ = Mathf.Clamp(handleLocalStartPos.z + handMovesSinceGrab, handleStartAndEndLocatalZ.x, handleStartAndEndLocatalZ.y);

        float oldZ = handle.transform.localEulerAngles.z;

        //if(SliderRestriction == SliderRestriction.up && newZ < oldZ)
        //{
        //    return;
        //}
        //else if(SliderRestriction == SliderRestriction.up && newZ < oldZ)
        //{
        //    return;
        //}

        handle.transform.localPosition = new Vector3(handleLocalStartPos.x, handleLocalStartPos.y, newZ);

        float percentofRange = Mathf.InverseLerp(handleStartAndEndLocatalZ.x, handleStartAndEndLocatalZ.y, handle.transform.localPosition.z);

        float sliderValue = Mathf.Lerp(outputRange.x, outputRange.y, percentofRange);

        OnNewSliderValue?.Invoke(sliderValue);

    }


}
