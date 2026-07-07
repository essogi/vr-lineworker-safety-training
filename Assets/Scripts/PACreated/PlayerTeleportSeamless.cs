using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportSeamless : MonoBehaviour
{

    [SerializeField] GameObject Distort;

    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;

    bool acting = false;

    private void Update()
    {
        if (acting)
        {
            if (Vector3.Distance(Distort.transform.position, point2.transform.position) < 0.1f)
            {
                stop();
            }
            else
            {
                Distort.transform.position = Vector3.MoveTowards(Distort.transform.position, point2.transform.position, 5 * Time.deltaTime);
            }
            
        }
    }


    // Start is called before the first frame update
    public void triggure()
    {
        acting = true;
        Distort.SetActive(true);
    }

    void stop()
    {
        acting = false;
        Distort.SetActive(false);
        Distort.transform.position = point1.transform.position;
        
    }
}
