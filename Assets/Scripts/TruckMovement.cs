using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    [SerializeField] private TruckSoundPlay truckSound;

    [SerializeField] private float MovSpeed = 0.06f;
    [SerializeField] private float RotSpeed = .3f;

    [SerializeField] private GameObject soundbox;


    [SerializeField] private GameObject handle;

    private HandleMovement truckmove;

    private bool isMoving = false;
    private bool shaking = false;

    [SerializeField] private Transform ArmBase;
    [SerializeField] private Transform LowerPivit; //this is the part that is meant to extend and lower/rise
    [SerializeField] private Transform UpperPivit;
    [SerializeField] private Transform ExtensionPoint;


    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        truckmove = handle.GetComponent<HandleMovement>();
        truckSound = handle.GetComponent<TruckSoundPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(shaking)
        {
            float shakefunc = (Mathf.Sin(time) - Mathf.Cos(time) + Mathf.Pow(Mathf.Cos(time), 2) - Mathf.Pow(Mathf.Sin(time), 2))/100;
            ArmBase.Rotate(new Vector3(0, -shakefunc, 0));

            shakefunc = (Mathf.Sin(2* (time - 2)) - Mathf.Cos((time-2)) + Mathf.Pow(Mathf.Cos(2*(time - 2)), 2) - Mathf.Pow(Mathf.Sin(2*(time - 2)), 2)) /100;
            LowerPivit.Rotate(0, 0, -shakefunc);
            UpperPivit.Rotate(0, 0, shakefunc);
        }


        if(isMoving)
        {
           // Debug.Log(truckmove.percentX);
           // Debug.Log(truckmove.percentY);

            if (Mathf.Abs(truckmove.percentX) >= 0.3) // base rotation
            {
                ArmBase.Rotate(new Vector3(0, truckmove.percentX * RotSpeed, 0));
                truckSound.soundType = TruckSoundPlay.Sound.Rotation;
            }
            if (Mathf.Abs(truckmove.percentY) >= 0.3) // Up piviting: - is up + is down
            {
                float rotation = RotSpeed * truckmove.percentY;
                
                if (LowerPivit.localRotation.z * Mathf.Rad2Deg *2 > -22f && truckmove.percentY > 0)
                {
                    LowerPivit.Rotate(0,0,-rotation);
                    UpperPivit.Rotate(0,0,rotation);
                    truckSound.soundType = TruckSoundPlay.Sound.Rotation;
                }
                else if (LowerPivit.localRotation.z * Mathf.Rad2Deg *2 < 25.0f && truckmove.percentY < 0)
                {
                    LowerPivit.Rotate(0, 0, -rotation);
                    UpperPivit.Rotate(0, 0, rotation);
                    truckSound.soundType = TruckSoundPlay.Sound.Rotation;
                }
                else
                {
                    truckSound.soundType = TruckSoundPlay.Sound.None;
                }
                //Debug.Log(new Vector3(0, truckmove.percentY * MovSpeed*100, 0));
                //UpperArm.Translate(new Vector3(0, truckmove.percentY * MovSpeed,0));
            }
            if (Mathf.Abs(truckmove.percentZ) >= 0.3)
            {
                if (ExtensionPoint.localPosition.x > -2.9f && truckmove.percentZ > 0)
                {
                    ExtensionPoint.Translate( -MovSpeed* truckmove.percentZ, 0,0);
                    truckSound.soundType = TruckSoundPlay.Sound.Extension;
                }
                else if (ExtensionPoint.localPosition.x < 0.01f && truckmove.percentZ < 0)
                {
                    ExtensionPoint.Translate( -MovSpeed * truckmove.percentZ, 0, 0);
                    truckSound.soundType = TruckSoundPlay.Sound.Extension;
                }
                else
                {
                    truckSound.soundType = TruckSoundPlay.Sound.None;
                }
            }
        }
        else
        {
            truckSound.soundType = TruckSoundPlay.Sound.None;
        }
    }


    public void ToogleMove()
    {
        isMoving = !isMoving;

    }

    public void shakingOn()
    {
        shaking = true;

    }

    public void shakingOff()
    {
        shaking = false;
    }

}
