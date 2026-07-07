using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    Transform SnapPointRight { get; }
    Transform SnapPointLeft { get; }



    void startGrabbing(Transform handAnchor);
    void EnterRange();
    void ExitRange();
    void setClosestOne();

    void StopGrabbing();
}
