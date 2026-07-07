using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypoint = 0;
    [SerializeField] float speed = 1;

    Vector3 lastLoc;

    Vector3 Velocity;

    // Start is called before the first frame update
    void Start()
    {
        lastLoc = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 0.1f)
        {

            currentWaypoint++;
            currentWaypoint %= waypoints.Length;

        }
        

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);

        Vector3 NextLoc = transform.position;

        Velocity = (NextLoc - lastLoc)/Time.deltaTime;

        lastLoc = NextLoc;


    }
    public Vector3 GetVelocity()
    {

       return Velocity;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (!this.CompareTag("Enemy") && other.gameObject.name == "Player1 Origin")
    //    {
    //        Debug.Log("hi");
    //        Debug.Log("here");
    //        other.transform.position += Velocity;
    //    }
    //}
}
