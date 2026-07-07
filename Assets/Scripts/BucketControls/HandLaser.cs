using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLaser : MonoBehaviour
{
    private LineRenderer LR;
    public bool IsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();
        LR.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit[] hits;
        RaycastHit[] hits2;
        bool isHit = false;

        LR.SetPosition(0, transform.position);

        if (IsActive)
        {
            hits = Physics.RaycastAll(transform.position, Vector3.down, 25.0f);
            hits2 = Physics.RaycastAll(transform.position, -Vector3.down, 25.0f);
            
            for(int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Grid"))
                {
                    isHit = true;
                    LR.enabled = true;
                    LR.SetPosition(1, hits[i].point);
                }
            }
            for (int i = 0; i < hits2.Length; i++)
            {
                if (hits2[i].collider.CompareTag("Grid"))
                {
                    isHit = true;
                    LR.enabled = true;
                    LR.SetPosition(1, hits2[i].point);
                }
            }
            if (!isHit)
            {
                LR.enabled = false;
            }
                


        }
        else { LR.enabled = false; }
        
        
        
    }
}
