using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Animat_Hand : MonoBehaviour
{
    public InputActionProperty pinchAnimation;
    public InputActionProperty gripAnimationAction;

    public Animator handAnimatior;

    // Start is called before the first frame update
    void Start()
    {
        
    } 

    // Update is called once per frame
    void Update()
    {
        float trig = pinchAnimation.action.ReadValue<float>();
        handAnimatior.SetFloat("Trigger", trig);

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimatior.SetFloat("Grip", gripValue);
    }
}
