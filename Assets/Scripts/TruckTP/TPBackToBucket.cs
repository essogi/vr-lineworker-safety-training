using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPBackToBucket : MonoBehaviour
{

    [SerializeField] private Transform Player;
    [SerializeField] private Transform PlayerOffset;

    [SerializeField] private Transform TpPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == Player)
        {
            var Dist = new Vector3(TpPoint.position.x - PlayerOffset.position.x, TpPoint.position.y - Player.position.y, TpPoint.position.z - PlayerOffset.position.z);

            Player.position = Player.position + Dist;

            Player.parent = TpPoint.parent;

        }

    }
}
