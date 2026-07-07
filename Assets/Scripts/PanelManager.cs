using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    private bool finalFinish = false;
    enum Experement
    {
        None, Text, Higthlight, both
    };


    public List<string> Panels;


    public List<string> Panels0;
    public List<string> Panels1;
    public List<string> Panels2;
    public List<string> Panels3;

    private int index;

    Experement experement = Experement.None;

    [SerializeField] private GameObject MainPanelRef; 

    [SerializeField] private TMP_Text PanelText;

    [SerializeField] private GameObject ButtonNext;
    [SerializeField] private GameObject ButtonBack;

    [SerializeField] private GameObject ButtonFinish;
    [SerializeField] private TextMeshProUGUI buttonFinishText;
    [SerializeField] private GameObject ButtonReset;

    [SerializeField] private GameObject Tracker;

    public UnityEvent HActive;
    public UnityEvent HDeactive;
    public UnityEvent EnvironmentOn;
    public UnityEvent EnvironmentOff;
    // Start is called before the first frame update
    void Start()
    {
        //setup the different panels in this area depending on the scenario 
        

        ///Experement with NOTHING active
        string panel = "<color=orange>Point</color> at this panel with your <color=orange>conroller</color> and press the <color=orange>trigger button</color> on the <color=orange>buttons</color> below to go through instructions";

        Panels0.Add(panel);
        Panels1.Add(panel);
        Panels2.Add(panel);
        Panels3.Add(panel);

        panel = "Please take a look over the edge of the bucket and let us know if you feel high up?";


        Panels0.Add(panel);

        panel = "<color=yellow>Press and <b>hold</b></color> the <color=yellow>grip button</color> while hovering over the <color=yellow>yellow pole</color> with your hand to grab it.\n\n" +
             "<color=yellow>Pickup</color> the <color=yellow>tools</color> from the table with your <color=yellow>other hand</color> and place them on <color=yellow>top of the pole</color> to get ready for your powerline tasks.\n\n" +
            "Use your second hand to <color=yellow>grab and <b>hold</b></color> higher up from your first hand to extend the rod to reach the powerlines.\n\n" +
            "Press the <color=orange>trigger button</color> of the hand lower on the pole to <color=orange>release the object</color> attached.";
            

        Panels0.Add(panel);
        Panels1.Add(panel);
        Panels2.Add(panel);
        Panels3.Add(panel);

        panel = "<color=yellow>Grab and hold</color> the black bucket controller handle in the box facing the power lines to move the bucket.\n\n" +
            "It moves in the direction based on the distance your hand is away from the center of the grid that appears.\n\n" +
            "<color=yellow>Pull up/Push down</color> to move <color=yellow>up and down</color>.  <color=yellow>Pull it towards you/push away</color> to <color=yellow>rotate</color> the bucket.\n\n" +
            "<color=yellow>Move it sideways</color> (away/towards the bucket's arm) to <color=yellow>extend or retract</color> the arm.";
            


        Panels0.Add(panel);
        Panels1.Add(panel);
        Panels2.Add(panel);
        Panels3.Add(panel);

        panel = "Placing items at the end of the pole:\n\n" +
            "<color=yellow>Pick up the pole</color> in one hand and a <color=yellow>tool</color> in the other. <color=yellow>Move the tool to the end of the pole</color> until you see the outline of the tool apear on the end. <color=yellow>Let go of the tool</color>.\n\n" +
            "<color=yellow>Pick up the pole</color> and <color=yellow>move the top end over the tool</color> you want to pick up.";
            

        Panels0.Add(panel);
        Panels1.Add(panel);
        Panels2.Add(panel);
        Panels3.Add(panel);

        panel = "Two Hands Must Be Used to Reach the Powerlines:\n\n" +
            "<color=yellow>Pick up the pole</color> in one hand and <color=yellow>grab further up the pole with the other</color>. " +
            "The pole is now extened\n\n" +
            "<color=orange>Release tools</color> by pressing the <color=orange>trigger button</color> on your lower hand.\n\n" +
            "Use this technique to remove the Line Tester box from the pole and set Line Clamps on the power lines.";

        Panels0.Add(panel);
        Panels1.Add(panel);
        Panels2.Add(panel);
        Panels3.Add(panel);

        panel = "Complete the tasks using your memory of the instructions given before entering the experi-ence.";

        Panels0.Add(panel);
        //Panels1.Add(panel);
        //Panels2.Add(panel);
        //Panels3.Add(panel);

        //past panel
        panel = "Click the finish button when you believe you have completed all tasks.";

        Panels0.Add(panel);
        //Panels1.Add(panel);
        //Panels2.Add(panel);
        //Panels3.Add(panel);


        /////////////////////////////////////////////////////////

        /// Experement with TEXT only

        panel = "Task 1:\n\n" +
            "Use the controller on the side of the bucket facing the power lines to move the bucket below the power lines close enough to reach the lines with the pole extended.";

        Panels1.Add(panel);

        panel = "Task 2:\n\n" + 
            "Pick up the pole at the back of the bucket and place the Line Tester tool on the top of the pole.\n\n" +
            "Hold it up to each line to test for voltage.\n\n" +
            "Continue after you believe you have tested all lines.";

        Panels1.Add(panel);

        panel = "Task 3:\n" +
            "Attach one of the clamps to the end of the pole. Place the <b>paired</b> clamps on the power lines in the order below:\n" +
            " -First, clamp on the lowest line (neutral), second clamp on the left line (closest to the bucket start).\n" +
            " -Then, first clamp on the left most line, second clamp on the top most line.\n" +
            " -Finaly, first clamp on the lowest line, second clamp on the right line (Furthest from the bucket start)\n" +
            "Continue to the next panel once you are done.";

        Panels1.Add(panel);

        //past panel
        panel = "Click the finish button when you believe you have completed all tasks.";

        Panels1.Add(panel);


        /////////////////////////////////////////////////////////

        /// Experement with HIGHLIGHTS only

        

        panel = "Tasks: Use your memory of the provided instructions along with the highlights to navigate and place the lines in the specified order.";

        Panels2.Add(panel);

        //past panel
        panel = "Click the finish button when you believe you have completed all tasks.";

        Panels2.Add(panel);

        /////////////////////////////////////////////////////////

        //experement with BOTH active

        panel = "Task 1:\n\n"+
            "Use the controller on the side of the bucket facing the power lines to move the bucket below the power lines where the <color=yellow>yellow highlight</color> is and close enough to reach the lines with the pole extended.\n\n" +
            "The highlight will appear once you grab the handle.\n\n";

        Panels3.Add(panel);

        panel = "Task 2:\n\n" +
            "Pick up the pole at the back of the bucket and place the Line Tester tool where the <color=yellow>yellow highlight</color> is.\n\n" +
            "Hold it up to each line to test for voltage.\n\n" +
            "Continue after you believe you have tested all lines.";

        Panels3.Add(panel);

        panel = "Task 3:\n" +
            "Attach one of the neutral clamps to the end of the pole. Place the <b>paired</b> clamps on the power lines in the order below:\n" +
            " -First, clamp on the <color=purple>purple line</color>, second clamp on <color=red>red line</color>.\n" +
            " -Then, first clamp the <color=red>red line</color>, second clamp on the <color=green>green line</color>.\n" +
            " -Finaly, first clamp on the <color=purple>purple line</color>, second clamp on the <color=blue>blue line</color>\n" +
            "Continue to the next panel once you are done.";

        Panels3.Add(panel);

        //past panel
        panel = "Click the finish button when you believe you have completed all tasks.";

        Panels3.Add(panel);




        /////////////////////////////////////////////////////////
        ///Finish init
        Panels = Panels0;
        
        ButtonFinish.SetActive(false);
        ButtonReset.SetActive(false);
        Expere1();
        Envy1();

    }

    public void BForward()
    {
        //make sure the index does not overflow
        if(index < Panels.Count)
        {
            index++;
        }
        //set back button active
        if (index != 0)
        {
            ButtonBack.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        //set finish button visible on final panel and the next button uniteractable.
        if (index == Panels.Count - 1)
        {
            ButtonFinish.SetActive(true); 
            ButtonNext.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        //set panel text to next text
        PanelText.text  = Panels[index];
    }

    public void BBackWard()
    {
        //avoid underflow
        if(index > 0)
        {
            index--;
        }
        if(index == 0)
        {
            ButtonBack.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        //set next button active
        if (index != Panels.Count - 1)
        {
            ButtonNext.GetComponent<UnityEngine.UI.Button>().interactable = true;
            ButtonFinish.SetActive(false);
        }
        
        //set panel text to previous text
        PanelText.text = Panels[index];
    }

    public void BFinish()
    {
        if (!finalFinish)
        {
            PanelText.text = "\n\n\n<b>Thank you for participating!</b>\n";
            PanelText.text += "<b>Please hand the headset to the experimenter.</b>\n";
            ButtonFinish.GetComponent<RectTransform>().anchoredPosition = new UnityEngine.Vector2(ButtonFinish.GetComponent<RectTransform>().anchoredPosition.x+475, ButtonFinish.GetComponent<RectTransform>().anchoredPosition.y - 125);
            ButtonFinish.GetComponent<UnityEngine.UI.Image>().color = new Color(0.1f, 0.1f, 0.1f, 0.0f);
            buttonFinishText.text = "...";
            buttonFinishText.color = new Color(1f, 1f, 1f, 1f);
            ButtonFinish.SetActive(true);
            ButtonNext.SetActive(false);
            ButtonBack.SetActive(false);
            finalFinish = true;
        }
        else
        {
            PanelText.text = Tracker.GetComponent<Tracker>().FinishButton();
            ButtonFinish.SetActive(false);
            ButtonReset.SetActive(true);
        }
    }

    public void BReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("key 4");
            Panels = Panels3;
            PanelReset();
            HActive.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("key 2");
            Panels = Panels1;
            PanelReset();
            HDeactive.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("key 3");
            Panels = Panels2;
            PanelReset();
            HActive.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            
            Debug.Log("key 1");
            Panels = Panels0;
            PanelReset();
            HDeactive.Invoke();
        }
        //environment on
        if (Input.GetKeyDown(KeyCode.E))
        {
            EnvironmentOn.Invoke();
        }
        //environment off
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EnvironmentOff.Invoke();
        }
    }

    public void Expere1()
    {
        Debug.Log("key 4");
        Panels = Panels3;
        PanelReset();
        HActive.Invoke();
    }
    public void Expere2()
    {
        Debug.Log("key 2");
        Panels = Panels1;
        PanelReset();
        HDeactive.Invoke();
    }
    public void Expere3()
    {
        Debug.Log("key 3");
        Panels = Panels2;
        PanelReset();
        HActive.Invoke();
    }
    public void Expere4()
    {
        Debug.Log("key 1");
        Panels = Panels0;
        PanelReset();
        HDeactive.Invoke();
    }

    public void Envy1()
    {
        EnvironmentOn.Invoke();
    }

    public void Envy2()
    {
        EnvironmentOff.Invoke();
    }

    //reset the panel after a new mode has been activated
    private void PanelReset()
    {
        index = 0;
        PanelText.text = Panels[index];
        ButtonBack.GetComponent<UnityEngine.UI.Button>().interactable = false;
        ButtonNext.GetComponent<UnityEngine.UI.Button>().interactable = true;
        ButtonReset.SetActive(false);
        ButtonFinish.SetActive(false);
    }

}
