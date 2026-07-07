using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenUIManager : MonoBehaviour
{

    [SerializeField] TMP_Text Message;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updateCount(int count)
    {
        Message.text = count.ToString() + "/5 Stars Collected";
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
