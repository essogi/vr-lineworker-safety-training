using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Collect : MonoBehaviour
{
    

    [SerializeField] int Score = 0;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject ScreenUI;

    public void CollectItem(GameObject grabbed)
    {
        
        if(grabbed.gameObject.CompareTag("collectable"))
        { 
            Score++;
            Destroy(grabbed);
            UI.GetComponent<EndScreenManager>().updateCount(Score);
            ScreenUI.GetComponent<ScreenUIManager>().updateCount(Score);
        }
        if (Score >=5)
        {
            UI.GetComponent<EndScreenManager>().finish();
        }
    }



}
