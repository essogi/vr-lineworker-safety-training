using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BucketCheck : MonoBehaviour
{
    private bool alreadyActive = false;
    [SerializeField] private UnityEvent InRange;
    [SerializeField] private UnityEvent OutOfRange;

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
        
        if (other.CompareTag("BucketLocation") && !alreadyActive)
        {
            Debug.Log("bucket in");
            InRange.Invoke();
            alreadyActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("BucketLocation") && !alreadyActive)
        {
            Debug.Log("bucket out");
            OutOfRange.Invoke();
            alreadyActive = false;
        }
    }

}
