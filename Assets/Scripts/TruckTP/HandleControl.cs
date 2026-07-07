using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class HandleControl : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private UnityEvent StartTime;
    public bool handle1 = false;

    public bool handle2 = false;

    [SerializeField] private Transform Player;
    [SerializeField] private Transform PlayerOffset;

    [SerializeField] private Transform TpPoint;

    public bool blur = false;

    [SerializeField] private Transform blurplane;


    // Update is called once per frame

    public void updateHandle1()
    {
        handle1 = !handle1;
        checkState();
    }
    public void updateHandle2()
    {
        handle2 = !handle2;
        checkState();
    }

    public void checkState()
    {
        if (handle1 && handle2) {

            if (blur)
            {
                Debug.Log("blur");
                blurplane.GetComponent<MeshRenderer>().enabled = true;
            }
            //Vector3 playerOffse = Player.GetComponent<CharacterController>().center;
            //Player.GetComponent<TeleportationProvider>().

            //Player.position = new Vector3(TpPoint.position.x - PlayerOffset.localPosition.x, TpPoint.position.y, TpPoint.position.z - PlayerOffset.localPosition.y);

            var Dist = new Vector3(TpPoint.position.x - PlayerOffset.position.x, TpPoint.position.y - Player.position.y, TpPoint.position.z - PlayerOffset.position.z);

            Player.position = Player.position + Dist;

            Player.parent = TpPoint.parent;

            StartTime.Invoke();
        }
    }

    public void bluron()
    {
        blur = true;
    }
    public void bluroff()
    {
        blur = false;
    }


}
