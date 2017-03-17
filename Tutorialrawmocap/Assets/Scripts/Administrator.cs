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
    }
    private List<List<int>> clipsNotUsed = new List<List<int>>();
    // Use this for initialization
    public void Start () {
        relativePath = Application.dataPath; ;
        //fileName = relativePath + "/ratings.txt";
        //print(fileName);
        LoadingScreenObj = GameObject.Find("LoadingScreen").gameObject;
        LoadingScreenObj.SetActive(false);
        VotingOverlay = GameObject.Find("VotingOverlay").gameObject;
        for (int i = 0; i < 4; i++)
        {
            List<int> t_list = new List<int>();
            for (int j = 0; j < 3; j++)
            {
                if (i==1 && j==2)
                {
                    t_list.Add(0);
                    print(i + " " + j);
                }
                else
                {
                    // För att run inte har en extra increment
                    t_list.Add(1);
                }
            }
            clipsNotUsed.Add(t_list);
        }
        ChangeClips();
	}
	void Update()
    {

    }
    public void SaveRatingToFile()
    {
        int leftSliderValue = (int)GameObject.Find("LeftScreenSlider").GetComponent<Slider>().value;
        int rightSliderValue = (int)GameObject.Find("RightScreenSlider").GetComponent<Slider>().value;
        string t_lineToWrite;// = p_clipName1 + p_rating1.ToString();
        switch (CurrentClipRight.clipNumber)
        {
            case 0:
                t_lineToWrite = "Walking FullKeyFrame Rating: " + leftSliderValue + " Walking Increment: " + CurrentClipRight.increment + " Rating: " + rightSliderValue;
                break;
            case 1:
                t_lineToWrite = "Running FullKeyFrame Rating: " + leftSliderValue + " Running Increment: " + CurrentClipRight.increment + " Rating: " + rightSliderValue;
                break;
            case 2:
                t_lineToWrite = "Crouching FullKeyFrame Rating: " + leftSliderValue + " Crouching Increment: " + CurrentClipRight.increment + " Rating: " + rightSliderValue;
                break;
            case 3:
                t_lineToWrite = "Idle FullKeyFrame Rating: " + leftSliderValue + " Idle Increment: " + CurrentClipRight.increment + " Rating: " + rightSliderValue;
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
        int t_clip = Random.Range(0, 4);
        int t_increment = Random.Range(0, 3);
        while (clipsNotUsed[t_clip][t_increment] == 0 && passed)
        {
            t_clip = Random.Range(0, 4);
            t_increment = Random.Range(0, 3);
        }
        CurrentClipRight.clipNumber = t_clip;
        CurrentClipRight.increment = t_increment;
        clipsNotUsed[t_clip][t_increment] = 0;
        passed = false;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (clipsNotUsed[i][j] == 1)
                {
                    passed = true;
                }
            }
        }

        LoadingScreen();
        GameObject.Find("DefaultAvatar").GetComponent<TransitionUpdater>().ChangeClips(CurrentClipLeft.clipNumber, CurrentClipLeft.increment, CurrentClipRight.clipNumber, CurrentClipRight.increment);
        // Använd listan för vilket clip å vilken increment som ska användas
    }
}
