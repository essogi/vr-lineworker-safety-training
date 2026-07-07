using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportRay : MonoBehaviour
{


    [SerializeField] GameObject leftTeleportation;
    [SerializeField] GameObject rightTeleportation;
    [SerializeField] InputActionProperty leftActivate;
    [SerializeField] InputActionProperty rightActivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftTeleportation.SetActive(leftActivate.action.ReadValue<float>() > 0.1f);
        rightTeleportation.SetActive(rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
