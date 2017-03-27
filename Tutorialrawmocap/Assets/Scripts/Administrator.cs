using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class Administrator : MonoBehaviour{

    private string relativePath = ""; 
    private string fileName = "";
    /*
        0 = Walking
        1 = Running
        2 = Crouching
        3 = Idle
    */
    ClipIncrement CurrentClipLeft;
    ClipIncrement CurrentClipRight;
    bool passed = true;
    GameObject LoadingScreenObj;
    GameObject VotingOverlay;
    struct ClipIncrement
    {
        public int clipNumber;
        public int increment;
        public int flip;
    }
    private List<List<List<int>>> clipsNotUsed = new List<List<List<int>>>();
    // Use this for initialization
    public void Start () {
        relativePath = Application.dataPath; ;
        //fileName = relativePath + "/ratings.txt";
        //print(fileName);
        LoadingScreenObj = GameObject.Find("LoadingScreen").gameObject;
        LoadingScreenObj.SetActive(false);
        VotingOverlay = GameObject.Find("VotingOverlay").gameObject;

        // Nu vill vi ha increment 0 och 1 och sen flippa dom.
        //for (int i = 0; i < 4; i++)
        //{
        //    List<int> t_list = new List<int>();
        //    for (int j = 0; j < 3; j++)
        //    {
        //        if (i==1 && j==2)
        //        {
        //            t_list.Add(0);
        //            print(i + " " + j);
        //        }
        //        else
        //        {
        //            // För att run inte har en extra increment
        //            t_list.Add(1);
        //        }
        //    }
        //    clipsNotUsed.Add(t_list);
        //}

        for (int i = 0; i < 4; i++)
        {
            List<List<int>> t_listList = new List<List<int>>();
            for (int j = 0; j < 2; j++)
            {
                List<int> t_list = new List<int>();
                t_list.Add(1);
                t_list.Add(1);
                t_listList.Add(t_list);
            }
            clipsNotUsed.Add(t_listList);
        }

        ChangeClips();
	}
	void Update()
    {

    }
    public void SaveRatingToFile()
    {
        //int leftSliderValue = (int)GameObject.Find("LeftScreenSlider").GetComponent<Slider>().value;
        int rightSliderValue = (int)GameObject.Find("RightScreenSlider").GetComponent<Slider>().value;
        bool leftToggle = GameObject.Find("LeftToggle").GetComponent<Toggle>().isOn;
        bool rightToggle = GameObject.Find("RightToggle").GetComponent<Toggle>().isOn;
        string t_lineToWrite;// = p_clipName1 + p_rating1.ToString();
        switch (CurrentClipRight.clipNumber)
        {
           // case 0:
           //     t_lineToWrite = "Walking FullKeyFrame Rating: " + leftSliderValue + " Walking Increment: " + CurrentClipRight.increment + " Rating: " + rightSliderValue;
           //     break;
           // case 1:
           //     t_lineToWrite = "Running FullKeyFrame Rating: " + leftSliderValue + " Running Increment: " + CurrentClipRight.increment + " Rating: " + rightSliderValue;
           //     break;
           // case 2:
           //     t_lineToWrite = "Crouching FullKeyFrame Rating: " + leftSliderValue + " Crouching Increment: " + CurrentClipRight.increment + " Rating: " + rightSliderValue;
           //     break;
           // case 3:
           //     t_lineToWrite = "Idle FullKeyFrame Rating: " + leftSliderValue + " Idle Increment: " + CurrentClipRight.increment + " Rating: " + rightSliderValue;
           //     break;
           // default:
           //     t_lineToWrite = "Something went wrong ur in defaul case in administrator";
           //     break;

            case 0:
                if (TransitionUpdater.m_instance.m_flip)
                {
                    if (leftToggle)
                    {
                        t_lineToWrite = "Walking Preferred: Hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                    else
                    {
                        t_lineToWrite = "Walking Preferred: Original against hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                }
                else
                {
                    if (rightToggle)
                    {
                        t_lineToWrite = "Walking Preferred: Hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                    else
                    {
                        t_lineToWrite = "Walking Preferred: Original against hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                }
                break;
            case 1:
                if (TransitionUpdater.m_instance.m_flip)
                {
                    if (leftToggle)
                    {
                        t_lineToWrite = "Running Preferred: Hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                    else
                    {
                        t_lineToWrite = "Running Preferred: Original against hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                }
                else
                {
                    if (rightToggle)
                    {
                        t_lineToWrite = "Running Preferred: Hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                    else
                    {
                        t_lineToWrite = "Running Preferred: Original against hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                }
                break;
            case 2:
                if (TransitionUpdater.m_instance.m_flip)
                {
                    if (leftToggle)
                    {
                        t_lineToWrite = "Crouching Preferred: Hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                    else
                    {
                        t_lineToWrite = "Crouching Preferred: Original against hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                }
                else
                {
                    if (rightToggle)
                    {
                        t_lineToWrite = "Crouching Preferred: Hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                    else
                    {
                        t_lineToWrite = "Crouching Preferred: Original against hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                }
                break;
            case 3:
                if (TransitionUpdater.m_instance.m_flip)
                {
                    if (leftToggle)
                    {
                        t_lineToWrite = "Idle Preferred: Hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                    else
                    {
                        t_lineToWrite = "Idle Preferred: Original against hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                }
                else
                {
                    if (rightToggle)
                    {
                        t_lineToWrite = "Idle Preferred: Hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                    else
                    {
                        t_lineToWrite = "Idle Preferred: Original against hybrid with " + CurrentClipRight.increment + " increments SliderValue was: " + rightSliderValue;
                    }
                }
                break;
            default:
                t_lineToWrite = "Something went wrong ur in defaul case in administrator";
                break;
        }
        fileName = "D:/_Skolgrejer/ratings.txt";
        if (!File.Exists(fileName))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(fileName))
            {
                sw.WriteLine("Ratings per clip");
            }
        }
        using (StreamWriter sw = File.AppendText(fileName))
        {
            sw.WriteLine(t_lineToWrite);
        }
        ChangeClips();
    }
    private void LoadingScreen()
    {
        ToggleLoadingScreen();
        Invoke("ToggleLoadingScreen", 1.5f);
    }
    private void ToggleLoadingScreen()
    {
        LoadingScreenObj.SetActive(!LoadingScreenObj.activeSelf);
        VotingOverlay.SetActive(!VotingOverlay.activeSelf);
    }
    private void ChangeClips()
    {
        if (!passed)
        {
            //experiment over thank you
            print("Experiment over thank yoU!");
            return;
        }
        //int t_clip = Random.Range(0, 4);
        //int t_increment = Random.Range(0, 3);
        //while (clipsNotUsed[t_clip][t_increment] == 0 && passed)
        //{
        //    t_clip = Random.Range(0, 4);
        //    t_increment = Random.Range(0, 3);
        //}
        //CurrentClipRight.clipNumber = t_clip;
        //CurrentClipRight.increment = t_increment;
        //clipsNotUsed[t_clip][t_increment] = 0;
        //passed = false;
        //for (int i = 0; i < 4; i++)
        //{
        //    for (int j = 0; j < 3; j++)
        //    {
        //        if (clipsNotUsed[i][j] == 1)
        //        {
        //            passed = true;
        //        }
        //    }
        //}
        int t_clip = Random.Range(0, 4);
        int t_increment = Random.Range(0, 2);
        int t_flip = Random.Range(0, 2);
        while (clipsNotUsed[t_clip][t_increment][t_flip] == 0 && passed)
        {
            t_clip = Random.Range(0, 4);
            t_increment = Random.Range(0, 2);
            t_flip = Random.Range(0, 2);
        }
        CurrentClipRight.clipNumber = t_clip;
        CurrentClipRight.increment = t_increment;
        CurrentClipRight.flip = t_flip;
        clipsNotUsed[t_clip][t_increment][t_flip] = 0;
        passed = false;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    if (clipsNotUsed[i][j][k] == 1)
                    {
                        passed = true;
                    }
                }
            }
        }
        LoadingScreen();
        GameObject.Find("HybridAnimation").GetComponent<TransitionUpdater>().ChangeClips(CurrentClipRight.clipNumber, CurrentClipRight.increment, CurrentClipRight.flip);
        // Använd listan för vilket clip å vilken increment som ska användas
    }
}
