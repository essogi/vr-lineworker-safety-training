using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLine : MonoBehaviour
{
    private Transform hook1;
    private Transform hook2;
    private LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        hook1 = gameObject.transform.GetChild(0).GetChild(0);
        hook2 = gameObject.transform.GetChild(1).GetChild(0);
        lr = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, hook1.transform.position);
        lr.SetPosition(1, hook2.transform.position);
    }
}
