using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityEngine.UI;

public class LastFreeRecall: MonoBehaviour
{
    public Button finishButton;
    public TMP_InputField input;

    string filename = "";
    public static int trialNum = 0;

    public float startTime = 0.00f; 

    [System.Serializable]
    public class Recall
    {
        public string playerName;
        public float timestamp;
        public int trialNum;
        public string buildingName;
    }

    public List<Recall> itemList = new List<Recall>();
    public static List<string> wordList = new List<string>();

    void Start()
    {
        wordList.Clear(); 

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        input.ActivateInputField();

        finishButton.onClick.AddListener(finish);

        trialNum++;
        filename = @"UserData/RecallData/recallList_" + PlayerID.id + ".csv";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            wordList.Add(input.text.Trim().ToLower());

            Recall item = new Recall();
            item.timestamp = startTime;
            item.trialNum = trialNum;
            item.buildingName = input.text;
            itemList.Add(item);
            input.text = "";
            startTime += Time.deltaTime;
            input.ActivateInputField();
        }
        writeList();
        itemList.Clear();
    }

    public void finish()
    {
        SceneManager.LoadScene("LastRewardScene");
    }

    public void writeList()
    {
        if (itemList.Count > 0)
        {
            TextWriter writer = File.AppendText(filename);
            for (int i = 0; i < itemList.Count; i++)
            {
                writer.WriteLine(PlayerID.id + "," + itemList[i].trialNum + "," + itemList[i].timestamp + "," + itemList[i].buildingName);
            }

            writer.Close();
        }
    }
}
