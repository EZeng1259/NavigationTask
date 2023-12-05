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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        input.ActivateInputField();

        finishButton.onClick.AddListener(finish);

        trialNum++;
        filename = Application.dataPath + "/recallList_" + PlayerID.id + ".csv";

        TextWriter writer = new StreamWriter(filename, true);
        writer.WriteLine("player ID, round, timestamp, shop name");
        writer.Close();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            String time = System.DateTime.Now.ToString("hh:mm:ss:fff");
            Recall item = new Recall();
            item.timestamp = time;
            item.trialNum = trialNum;
            item.buildingName = input.text;
            itemList.Add(item);
            input.text = "";
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
