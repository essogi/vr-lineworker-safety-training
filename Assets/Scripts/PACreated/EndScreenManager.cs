using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{

    [SerializeField] GameObject menu;
    [SerializeField] Transform head;
    [SerializeField] float spawnDistance = 3;
    [SerializeField] InputActionProperty showMenuL;
    [SerializeField] InputActionProperty showMenuR;
    [SerializeField] TMP_Text Timer;
    [SerializeField] TMP_Text Message;
    [SerializeField] GameObject player;

    public bool TimeStart = false;

    bool timerstop = false;

    float currentTime = 0;
   
    //[SerializeField] Input

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        menu.transform.LookAt(head);
        menu.transform.forward *= -1;
        if (showMenuL.action.WasPerformedThisFrame() || showMenuR.action.WasPerformedThisFrame())
        {
            activate();
        }
        if (!timerstop && TimeStart)
        {
            currentTime += Time.deltaTime;
           
            Timer.text = "Time Spent: "+ Mathf.Round(currentTime).ToString() + " sec";
        }

    }
    public void updateCount(int count)
    {
        Message.text = "You have: \r\n" + count.ToString() + "/5 Stars collected so far";
    }


    public void finish()
    {
        timerstop = true;
        Message.text = "Done! \r\ncongratulations!!\r\nYou have collected all 5 Stars!";

        activate();
    }

    public void activate()
    {
        //Debug.Log(!menu.activeSelf);
        menu.SetActive(!menu.activeSelf);
        //Debug.Log(menu.activeSelf);
        this.transform.parent = player.transform.parent.transform;
        menu.transform.position = head.position + new Vector3(head.forward.x,0,head.forward.z).normalized *spawnDistance;
        if (!menu.activeSelf)
        {
            this.transform.parent = null;
        }
        
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
