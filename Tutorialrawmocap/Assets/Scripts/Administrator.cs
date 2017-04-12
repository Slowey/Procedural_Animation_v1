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
    float selectCooldown = 0f;
    ClipIncrement CurrentClipRight;
    bool passed = true;
    GameObject LoadingScreenObj;
    GameObject VotingOverlay;
    GameObject ExperimentOverObj;
    GameObject ExpOverImage;
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
        ExperimentOverObj = GameObject.Find("ExperimentOver").gameObject;
        LoadingScreenObj.SetActive(false);
        ExperimentOverObj.SetActive(false);
        VotingOverlay = GameObject.Find("VotingOverlay").gameObject;
        ExpOverImage = GameObject.Find("ExpOverImage");
        ExpOverImage.SetActive(false);
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
        if (selectCooldown >= 0)
        {
            selectCooldown -= Time.deltaTime;
        }
    }
    public void DeselectOtherButtons(GameObject thisButton)
    {
        //print(thisButton.name);
        if (selectCooldown>= 0f)
        {
            return;
        }
        selectCooldown = 0.01f;
        if (thisButton.name == "1Toggle")
        {
            //GameObject.Find("1Toggle").GetComponent<Toggle>().isOn = true;
            GameObject.Find("2Toggle").GetComponent<Toggle>().isOn = false;
            GameObject.Find("3Toggle").GetComponent<Toggle>().isOn = false;
            GameObject.Find("4Toggle").GetComponent<Toggle>().isOn = false;
            GameObject.Find("5Toggle").GetComponent<Toggle>().isOn = false;
        }
        else if(thisButton.name == "2Toggle")
        {
            GameObject.Find("1Toggle").GetComponent<Toggle>().isOn = false;
            //GameObject.Find("2Toggle").GetComponent<Toggle>().isOn = true;
            GameObject.Find("3Toggle").GetComponent<Toggle>().isOn = false;
            GameObject.Find("4Toggle").GetComponent<Toggle>().isOn = false;
            GameObject.Find("5Toggle").GetComponent<Toggle>().isOn = false;
        }
        else if (thisButton.name == "3Toggle")
        {
            GameObject.Find("1Toggle").GetComponent<Toggle>().isOn = false;
            GameObject.Find("2Toggle").GetComponent<Toggle>().isOn = false;
            //GameObject.Find("3Toggle").GetComponent<Toggle>().isOn = true;
            GameObject.Find("4Toggle").GetComponent<Toggle>().isOn = false;
            GameObject.Find("5Toggle").GetComponent<Toggle>().isOn = false;
        }
        else if (thisButton.name == "4Toggle")
        {
            GameObject.Find("1Toggle").GetComponent<Toggle>().isOn = false;
            GameObject.Find("2Toggle").GetComponent<Toggle>().isOn = false;
            GameObject.Find("3Toggle").GetComponent<Toggle>().isOn = false;
            //GameObject.Find("4Toggle").GetComponent<Toggle>().isOn = true;
            GameObject.Find("5Toggle").GetComponent<Toggle>().isOn = false;
        }
        else if (thisButton.name == "5Toggle")
        {
            GameObject.Find("1Toggle").GetComponent<Toggle>().isOn = false;
            GameObject.Find("2Toggle").GetComponent<Toggle>().isOn = false;
            GameObject.Find("3Toggle").GetComponent<Toggle>().isOn = false;
            GameObject.Find("4Toggle").GetComponent<Toggle>().isOn = false;
            //GameObject.Find("5Toggle").GetComponent<Toggle>().isOn = true;
        }
        //thisButton.GetComponent<Toggle>().isOn = true;
        //print(selectedToggle.name);
    }
    public void SaveRatingToFile()
    {
        string whatButton = "";
        bool anyButtonPressed = true;
        if (TransitionUpdater.m_instance.m_flip)
        {
            if (GameObject.Find("1Toggle").GetComponent<Toggle>().isOn)
            {
                whatButton = "Hybrid significantly better";
            }
            else if (GameObject.Find("2Toggle").GetComponent<Toggle>().isOn)
            {
                whatButton = "Hybrid slightly better";
            }
            else if(GameObject.Find("3Toggle").GetComponent<Toggle>().isOn)
            {
                whatButton = "The two animations are quality wise equal";
            }
            else if(GameObject.Find("4Toggle").GetComponent<Toggle>().isOn)
            {
                whatButton = "Original slightly better";
            }
            else if(GameObject.Find("5Toggle").GetComponent<Toggle>().isOn)
            {
                whatButton = "Original significantly better";
            }
            else
            {
                anyButtonPressed = false;
            }
            
        }
        else
        {
            if (GameObject.Find("1Toggle").GetComponent<Toggle>().isOn)
            {
                whatButton = "Original significantly better";
            }
            else if(GameObject.Find("2Toggle").GetComponent<Toggle>().isOn)
            {
                whatButton = "Original slightly better";
            }
            else if(GameObject.Find("3Toggle").GetComponent<Toggle>().isOn)
            {
                whatButton = "The two animations are quality wise equal";
            }
            else if(GameObject.Find("4Toggle").GetComponent<Toggle>().isOn)
            {
                whatButton = "Hybrid slightly better";
            }
            else if(GameObject.Find("5Toggle").GetComponent<Toggle>().isOn)
            {
                whatButton = "Hybrid significantly better";
            }
            else
            {
                anyButtonPressed = false;
            }
        }
        //find which box is toggled.
        if(anyButtonPressed)
        {
            string t_lineToWrite;// = p_clipName1 + p_rating1.ToString();
            switch (CurrentClipRight.clipNumber)
            {
                case 0:
                        t_lineToWrite = "Walking : " + whatButton + ", Clipincrement:  " + CurrentClipRight.increment;
                    break;
                case 1:
                        t_lineToWrite = "Running : " + whatButton + ", Clipincrement:  " + CurrentClipRight.increment;
                    break;
                case 2:
                    t_lineToWrite = "Crouching : " + whatButton + ", Clipincrement:  " + CurrentClipRight.increment;
                    break;
                case 3:
                    t_lineToWrite = "Idle : " + whatButton + ", Clipincrement:  " + CurrentClipRight.increment;
                    break;
                default:
                    t_lineToWrite = "Something went wrong ur in defaul case in administrator";
                    break;
            }
            fileName = Application.dataPath+"/ratings.txt";
            //print(fileName);
            //fileName = "D:/_Skolgrejer/ratings.txt";
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
            //print(t_lineToWrite);
            ChangeClips();

        }
    }
    private void LoadingScreen()
    {
        //Unmark every toggle
        GameObject.Find("1Toggle").GetComponent<Toggle>().isOn = false;
        GameObject.Find("2Toggle").GetComponent<Toggle>().isOn = false;
        GameObject.Find("3Toggle").GetComponent<Toggle>().isOn = false;
        GameObject.Find("4Toggle").GetComponent<Toggle>().isOn = false;
        GameObject.Find("5Toggle").GetComponent<Toggle>().isOn = false;

        ToggleLoadingScreen();
        Invoke("ToggleLoadingScreen", 2.0f);
    }
    private void ExperimentOver()
    {
        ExperimentOverObj.SetActive(true);
        GameObject.Find("HybridAnimation").SetActive(false);
        GameObject.Find("OriginalAnimation").SetActive(false);
        VotingOverlay.SetActive(false);
    }
    private void ToggleLoadingScreen()
    {
        LoadingScreenObj.SetActive(!LoadingScreenObj.activeSelf);
        VotingOverlay.SetActive(!VotingOverlay.activeSelf);
    }
    public void SaveDetails()
    {
        int t_dropDownValue = 0;
        string t_lineToWriteEndExp = "";
        t_dropDownValue = GameObject.Find("DropdownTech").GetComponent<Dropdown>().value;
        if(t_dropDownValue == 1)
        {
            t_lineToWriteEndExp = "Technical experience: Yes";
        }
        else if(t_dropDownValue == 2)
        {
            t_lineToWriteEndExp = "Technical experience: No";
        }
        else if(t_dropDownValue == 0)
        {
            t_lineToWriteEndExp = "Technical experience: Don't know";
        }

        t_dropDownValue = GameObject.Find("DropdownGame").GetComponent<Dropdown>().value;
        if (t_dropDownValue == 1)
        {
            t_lineToWriteEndExp += " Gaming experience: Yes";
        }
        else if (t_dropDownValue == 2)
        {
            t_lineToWriteEndExp += " Gaming experience: No";
        }
        else if (t_dropDownValue == 0)
        {
            t_lineToWriteEndExp += " Gaming experience: Don't know";
        }
        fileName = Application.dataPath + "/ratings.txt";
        //print(fileName);
        //fileName = "D:/_Skolgrejer/ratings.txt";

        using (StreamWriter sw = File.AppendText(fileName))
        {
            sw.WriteLine(t_lineToWriteEndExp);
            sw.WriteLine("Participant done.");
            sw.WriteLine("");
        }
        ExpOverImage.SetActive(true);
    }
    private void ChangeClips()
    {
        //passed = false;
        if (!passed)
        {
            //experiment over thank you
            //print("Experiment over thank yoU!");
            ExperimentOver();
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

        //List<ClipIncrement> t_unusedClips = new List<ClipIncrement>();

        //for (int i = 0; i < 4; i++)
        //{
        //    for (int k = 0; k < 2; k++)
        //    {
        //        for (int j = 0; j < 2; j++)
        //        {
        //            if (clipsNotUsed[i][k][j] == 1)
        //            {
        //                ClipIncrement t_tempclip;
        //                t_tempclip.clipNumber = i;
        //                t_tempclip.increment = k;
        //                t_tempclip.flip = j;
        //                t_unusedClips.Add(t_tempclip);
        //            }
        //        }
        //    }
        //}
        //if (t_unusedClips.Count == 3)
        //{
        //    for (int i = 0; i < length; i++)
        //    {

        //    }
        //}
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
