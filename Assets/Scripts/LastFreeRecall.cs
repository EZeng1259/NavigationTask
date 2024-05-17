using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LastFreeRecall : MonoBehaviour
{
    public Button finishButton;
    public TMP_InputField input;

    public static int trialNum = 0;
    public float startTime = 0.00f;

    public string apiUrl = "https://salty-thicket-48002.herokuapp.com/write_data"; // API URL

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
            for (int i = 0; i < itemList.Count; i++)
            {
                SendDataToServer(PlayerID.id, itemList[i].trialNum, itemList[i].timestamp, itemList[i].buildingName);
            }
        }
    }

    public void SendDataToServer(string playerId, int trialNum, float timestamp, string buildingName)
    {
        StartCoroutine(SendDataCoroutine(playerId, trialNum, timestamp, buildingName));
    }

    private IEnumerator SendDataCoroutine(string playerId, int trialNum, float timestamp, string buildingName)
    {
        var data = new
        {
            table_name = "freerecall",
            participant_id = playerId,
            trial = trialNum,
            timestamp = timestamp,
            building_name = buildingName
        };

        string jsonData = JsonUtility.ToJson(data);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(apiUrl, ""))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Data sent successfully to FreeRecall table");
            }
        }
    }
}
