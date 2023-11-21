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
    public static int trialNum = 0;

    [System.Serializable]
    public class Recall
    {
        public string playerName; 
        public string timestamp;
        public int trialNum;
        public string buildingName; 
    }

    public List<Recall> itemList = new List<Recall>();

    void Start()
    {
        returnButton.onClick.AddListener(returnToEnvironment);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        trialNum++;
        if (trialNum > 5)
        {
            Application.Quit();
        }

        filename = Application.dataPath + "/recallList.csv";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DateTime time = System.DateTime.Now;
            Recall item = new Recall();
            item.timestamp = time.ToString();
            item.trialNum = trialNum;
            item.buildingName = input.text;
            itemList.Add(item);
            input.text = "";
            input.ActivateInputField();
        }
        writeList();
        itemList.Clear(); 
    }

    public void returnToEnvironment()
    {
        SceneManager.LoadScene("NavigationScene");
    }

    public void writeList()
    {
        if(itemList.Count > 0)
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
