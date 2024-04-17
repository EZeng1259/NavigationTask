using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityEngine.UI;

public class FreeRecall : MonoBehaviour
{
    public Button returnButton;
    public TMP_InputField input;

    string filename = "";

    private float startTime; 

    [System.Serializable]
    public class Recall
    {
        public string playerName; 
        public float timestamp;
        public string buildingName; 
    }

    public List<Recall> itemList = new List<Recall>();
    public static List<string> wordList = new List<string>(); 

    void Start()
    {
        startTime = Time.time; 
        wordList.Clear(); 

        returnButton.onClick.AddListener(rewardScene);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        input.ActivateInputField();

        filename = @"UserData/RecallData/" + PlayerID.id +  ".csv";

        TextWriter writer1 = File.AppendText(filename);
        writer1.WriteLine("ID" + "," + "Timestamp" + "," + "Store Name");
        writer1.Close(); 

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            wordList.Add(input.text.Trim().ToLower());

            Recall item = new Recall();
            item.timestamp = Time.time - startTime;
            item.buildingName = input.text;
            itemList.Add(item);
            input.text = "";
            input.ActivateInputField();
        }
        writeList();
        itemList.Clear(); 
    }

    public void rewardScene()
    {
        SceneManager.LoadScene("LastRewardScene");
    }

    public void writeList()
    {
        if(itemList.Count > 0)
        {
            TextWriter writer = File.AppendText(filename);
            for (int i = 0; i < itemList.Count; i++)
            {
                writer.WriteLine(PlayerID.id + "," + itemList[i].timestamp + "," + itemList[i].buildingName);
            }

            writer.Close();
        }
    }
}
