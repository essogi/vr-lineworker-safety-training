using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReParient : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] Rigidbody playermovement;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hi");
        if (other.gameObject == player)
        {
            other.gameObject.transform.SetParent(transform.parent.transform, true);
        }
    }

    //Vector3 positionLast = new Vector3(0,0,0);

    // Update is called once per frame



    private void OnTriggerStay(Collider other)
    {
        //Vector3 newPosition = other.transform.position;
        //if(positionLast.x + positionLast.y + positionLast.z == 0)
        //{
        //    positionLast = newPosition;
        //}
        if (other.gameObject == player)
        {
            //Vector3 velocity = (newPosition + positionLast) / Time.deltaTime;
            //if (velocity.x > 0.00000001 && velocity.y > 0.00000001 && velocity.z > 0.00000001)
            //{
            //    playermovement.velocity += (newPosition + positionLast) / Time.deltaTime;
            //}


            //Vector3 velocity = this.GetComponent<WaypointFollower>;

            //if (velocity.x > 0.00000001 && velocity.y > 0.00000001 && velocity.z > 0.00000001)
            //{
            //    playermovement.velocity
            //}

        }
        //positionLast = newPosition;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playermovement.velocity = new Vector3(0,0,0);
        }

    }
}