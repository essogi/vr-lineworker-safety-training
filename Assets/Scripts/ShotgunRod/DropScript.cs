using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    public Transform returnal;
    private Vector3 originalScale;
    public Tracker simTracker;

    public bool attatched = false;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    public void toggleAttatch()
    {
        attatched = !attatched;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attatched)
        {
            //Debug.Log(transform.position);
            if (transform.position.y < returnal.position.y - 0.2f)
            {
                //Debug.Log(timeOnGround);
                transform.GetComponent<Rigidbody>().velocity = new Vector3();
                transform.GetComponent<Rigidbody>().angularVelocity = new Vector3();
                transform.localScale = originalScale;
                transform.position = returnal.position;
                transform.rotation = returnal.rotation;
                simTracker.droppedPole();
                //transform.GetComponent<Rigidbody>().rotation = originalRodRotation;

            }
        }
    }
}
