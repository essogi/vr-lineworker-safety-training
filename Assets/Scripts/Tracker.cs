using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tracker : MonoBehaviour
{
    public TextMeshProUGUI textfield;

    private List<string> eventOrder;
    private List<string> infractions;
    private int countOfPowerChecks = 0;
    private bool[] powerChecks = new bool[] { false, false, false, false };
    private List<string[]> lineClamps;
    private bool bucketInRange = false;

    private string clampInfractionsText = "";
    private string powerInfractionsText = "";
    float currentTime = 0;
    bool timeActive = false;
    int poleDropCount = 0;
    int handleGrabbed = 0;

    private void Awake()
    {
        eventOrder = new List<string>();
        infractions = new List<string>();
        infractions.Add("noPowerCheck");
        infractions.Add("noClamps");
        lineClamps = new List<string[]>();
    }
    private void Update()
    {
        if (timeActive)
        {
            currentTime += Time.deltaTime;

            
        }
    }

    public void trackEvent(string message) {
        if (eventOrder.Contains(message))
           return;
        
        eventOrder.Add(message);

        switch(message)
        {
            case "Line 1 | Power Check":
                powerChecks[0] = true;
                countOfPowerChecks++;
                break;
            case "Line 2 | Power Check":
                powerChecks[1] = true;
                countOfPowerChecks++;
                break;
            case "Line 3 | Power Check":
                powerChecks[2] = true;
                countOfPowerChecks++;
                break;
            case "Line 4 | Power Check":
                powerChecks[3] = true;
                countOfPowerChecks++;
                break;
            default:
                clampOrder(message);
                break;
        }
    }

    private void clampOrder(string order)
    {
        if (!powerChecks[0] || !powerChecks[1] || !powerChecks[2] || !powerChecks[3])
        {
            if (!infractions.Contains("insufficientChecksForLinePower"))
            {
                infractions.Add("insufficientChecksForLinePower");
                powerInfractionsText += "-Task 2: Insufficient Power Checks Before Setting Neutral Lines\n";
                /*for (int i = 0; i < powerChecks.Length; i++) {
                    if(!powerChecks[i])
                    {
                        powerInfractionsText += "--Did not check " + "Line " + (i+1) + " for power.\n";
                    }
                }*/
            }
        }

        //order = order.Replace(" ", "");
        string[] parts = order.Split("|");

        switch(lineClamps.Count) {
            case 0:
                {
                    if (parts[0] != "Line 1 ")
                    {
                        addClampInfraction(parts, 1);
                    }
                    lineClamps.Add(parts);
                }break;
            case 1:
                {
                    if (parts[0] != "Line 2 ")
                    {
                        addClampInfraction(parts, 2);
                    }
                    else
                    {
                        if (parts[1] != lineClamps[0][1])
                        {
                            if (!infractions.Contains("incorrectClampPlacementOrder"))
                            {
                                infractions.Add("incorrectClampPlacementOrder");
                                clampInfractionsText += "-Task 3: Incorrect Neutral Clamp Placement Order\n";
                            }
                        }
                    }
                    lineClamps.Add(parts);
                }
                break;
            case 2:
                {
                    if (parts[0] != "Line 2 ")
                    {
                        addClampInfraction(parts, 2);
                    }
                    lineClamps.Add(parts);
                }
                break;
            case 3:
                {
                    if (parts[0] != "Line 3 ")
                    {
                        addClampInfraction(parts, 3);
                    }
                    else
                    {
                        if (parts[1] != lineClamps[2][1])
                        {
                            if (!infractions.Contains("incorrectClampPlacementOrder"))
                            {
                                infractions.Add("incorrectClampPlacementOrder");
                            }
                        }   
                    }
                    lineClamps.Add(parts);
                }
                break;
            case 4:
                {
                    if (parts[0] != "Line 1 ")
                    {
                        addClampInfraction(parts, 1);
                    }
                    lineClamps.Add(parts);
                }
                break;
            case 5:
                {
                    if (parts[0] != "Line 4 ")
                    {
                        addClampInfraction(parts, 4);
                    }
                    else
                    {
                        if (parts[1] != lineClamps[4][1])
                        {
                            if (!infractions.Contains("incorrectClampPlacementOrder"))
                            {
                                infractions.Add("incorrectClampPlacementOrder");
                            }
                        }
                    }
                    lineClamps.Add(parts);
                }
                break;
            default:
                Debug.LogError("Impossible Clamp Case Error!");
                break;
        }
    }

    private void addClampInfraction(string[] parts, int correctNum)
    {
        if (!infractions.Contains("incorrectClampPlacementOrder"))
        {
            infractions.Add("incorrectClampPlacementOrder");
            clampInfractionsText += "-Task 3: Incorrect Neutral Clamp Placement Order\n";
        }
    }

    public void bucketRangeToggle()
    {
        bucketInRange = !bucketInRange;
    }

    public void startTimer()
    {
        timeActive = true;
    }

    public void droppedPole()
    {
        poleDropCount++;
    }

    public void timesHandleGrapped()
    {
        handleGrabbed++;
    }

    public string FinishButton()
    {
        timeActive = false;
        bool success = true;
        string ResultsPanel = "Time Spent: " + Mathf.Round(currentTime).ToString() + " sec \n";
        ResultsPanel += "Tools dropped " + poleDropCount + " times. " + "Bucket handle grabbed " + handleGrabbed + " times.\n";
        ResultsPanel += "Total Power Checks:  " + countOfPowerChecks + "/4. Total Clamps Placed: " + lineClamps.Count + "/6\n";

        string bucketInfractionsText = "";
        if (!bucketInRange)
        {
            if (!infractions.Contains("incorrectBucketPosition"))
            {
                infractions.Add("incorrectBucketPosition");
                bucketInfractionsText += "-Task 1: Bucket was not moved into the designated work zone\n";
            }
        }
        

        if (infractions.Count > 0)
        {
            ResultsPanel += "------------------------\n";
            ResultsPanel += "<b>Infractions:</b>\n";
            success = false;
        }
        if (infractions.Contains("incorrectBucketPosition"))
        {
            ResultsPanel += bucketInfractionsText;
        }
        if (infractions.Contains("insufficientChecksForLinePower"))
        {
            ResultsPanel += powerInfractionsText;
        }
        
        if (infractions.Contains("incorrectClampPlacementOrder"))
        {   
            ResultsPanel += clampInfractionsText;
        }

        if (success)
        {
            ResultsPanel += "------------------------\n";
            if (countOfPowerChecks == 4 && lineClamps.Count == 6)
            {
                ResultsPanel += "<b>Completed Tasks:</b>\n";
                ResultsPanel += "+Tasks 1,2,3 completed successfully in correct order.\n";
            }
            else if (countOfPowerChecks == 4)
            {
                ResultsPanel += "<b>Completed Tasks:</b>\n";
                ResultsPanel += "+Task 1: Bucket was moved into the designated work zone\n";
                ResultsPanel += "+Task 2: All lines were checked for power.\n";
                ResultsPanel += "<b>Incomplete Tasks:</b>\n";
                ResultsPanel += "-Task 3: Did not finish setting all neutral clamps.\n";
            }
            else
            {
                ResultsPanel += "<b>Completed Tasks:</b>\n";
                ResultsPanel += "+Task 1: Bucket was moved into the designated work zone\n";
                ResultsPanel += "<b>Incomplete Tasks:</b>\n";
                ResultsPanel += "-Task 2: Did not finish checking lines for power.\n";
                ResultsPanel += "-Task 3: Did not finish setting all neutral clamps.\n";
            }
        }
        else if (!success)
        {
            ResultsPanel += "------------------------\n";
            ResultsPanel += "<b>Completed Tasks:</b>\n";
            if (bucketInRange)
            {
                ResultsPanel += "+Task 1: Bucket was moved into the designated work zone\n";
            }
            if (!infractions.Contains("insufficientChecksForLinePower") && countOfPowerChecks == 4)
            {
                ResultsPanel += "+Task 2: All lines were checked for power.\n";
            }
            if (!infractions.Contains("incorrectClampPlacementOrder") && lineClamps.Count == 6)
            {
                ResultsPanel += "+Task 3: All neutral clamps set in in correct order.\n";
            }
        }
        //ResultsPanel += "------------------------\n";
        /*foreach (var val in eventOrder)
        {
            ResultsPanel += val + "\n";
            Debug.Log(val);
        }*/
        return ResultsPanel;
    }
}


