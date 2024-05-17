using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;

public class CountBuildings : MonoBehaviour
{
    public TMP_Text input;
    public int buildingCounter = 0; //counts number of red buildings encountered

    public static int trialNum = 0;

    public static int score = 0;

    public static float bestTime;
    public static float newTime;

    public static float totalDistance;
    public static float bestTotalDistance = Mathf.Infinity;
    Datapoint prevPoint = new Datapoint();

    float minDist = 5f;

    List<string> buildingsVisited = new List<string>(); // keeps track of buildings visited 

    float time = 0f;
    private float startTime;
    [SerializeField] float interval = 25f;

    public Transform cameraRotation;

    public string apiUrl = "https://salty-thicket-48002.herokuapp.com/write_data"; // API URL

    [System.Serializable]
    public class Datapoint
    {
        public float timestamp;
        public float x;
        public float y;
        public float rotx;
        public float roty;
    }

    public class DataList
    {
        public Datapoint[] data;
    }

    public List<Datapoint> dataPoints = new List<Datapoint>();

    void Start()
    {
        startTime = Time.time;

        totalDistance = 0;

        prevPoint.x = transform.position.x;
        prevPoint.y = transform.position.y;

        trialNum++;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("StartRecallScene");
        }

        float dist = Vector3.Distance(FindClosestRedBuilding().GetComponentInChildren<TMP_Text>().transform.position, transform.position);
        if (buildingsVisited.Contains(FindClosestRedBuilding().name)) { }
        else
        {
            if (dist <= minDist)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    buildingCounter++;
                    buildingsVisited.Add(FindClosestRedBuilding().name);
                    String name = FindClosestRedBuilding().name;
                    foreach (TMP_Text g in FindClosestRedBuilding().GetComponentsInChildren<TMP_Text>())
                    {
                        g.color = new Color(1, 0, 0, 1);
                    }
                    input.text = "" + buildingCounter;

                    float currTime = Time.time - startTime;
                    SendBuildingVisitedOrderToServer(PlayerID.id, trialNum, currTime, name);
                }
            }
        }

        time += Time.deltaTime;
        if (time > interval)
        {
            float x = transform.position.x;
            float y = transform.position.z;
            float rotate_x = CameraMovement.yRotation % 360;
            float rotate_y = (CameraMovement.xRotation % 360) * -1;
            float currTime = Time.time - startTime;

            Datapoint sample = new Datapoint();
            sample.timestamp = currTime;
            sample.x = x;
            sample.y = y;
            sample.rotx = rotate_x;
            sample.roty = rotate_y;
            dataPoints.Add(sample);
            time = 0;

            totalDistance += Mathf.Sqrt(Mathf.Pow((x - prevPoint.x), 2) + Mathf.Pow((y - prevPoint.y), 2));

            prevPoint.x = x;
            prevPoint.y = y;
        }

        writeToDatabase();
        dataPoints.Clear();

        if (buildingCounter == 19)
        {
            if (trialNum == 1)
            {
                bestTotalDistance = totalDistance;
                SceneManager.LoadScene("BestTimeScene");
            }
            else
            {
                if (totalDistance < bestTotalDistance)
                {
                    bestTotalDistance = totalDistance;
                    score += 5;
                    SceneManager.LoadScene("BeatBestTimeScene");
                }
                else
                {
                    SceneManager.LoadScene("FailBestTimeScene");
                }
            }
        }
    }

    public void writeToDatabase()
    {
        if (dataPoints.Count > 0)
        {
            for (int i = 0; i < dataPoints.Count; i++)
            {
                SendNavigationDataToServer(PlayerID.id, trialNum, dataPoints[i].timestamp, dataPoints[i].x, dataPoints[i].y, dataPoints[i].rotx, dataPoints[i].roty);
            }
        }
    }

    public void SendNavigationDataToServer(string playerId, int trialNum, float timestamp, float x, float y, float rotx, float roty)
    {
        StartCoroutine(SendNavigationDataCoroutine(playerId, trialNum, timestamp, x, y, rotx, roty));
    }

    private IEnumerator SendNavigationDataCoroutine(string playerId, int trialNum, float timestamp, float x, float y, float rotx, float roty)
    {
        var data = new
        {
            table_name = "navigation",
            participant_id = playerId,
            trial = trialNum,
            timestamp = timestamp,
            x = x,
            y = y,
            rotx = rotx,
            roty = roty
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
                Debug.Log("Navigation data sent successfully");
            }
        }
    }

    public void SendBuildingVisitedOrderToServer(string playerId, int trialNum, float timestamp, string buildingName)
    {
        StartCoroutine(SendBuildingVisitedOrderCoroutine(playerId, trialNum, timestamp, buildingName));
    }

    private IEnumerator SendBuildingVisitedOrderCoroutine(string playerId, int trialNum, float timestamp, string buildingName)
    {
        var data = new
        {
            table_name = "buildingvisitedorder",
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
                Debug.Log("Building visited order data sent successfully");
            }
        }
    }

    public GameObject FindClosestRedBuilding()
    {
        GameObject[] redBuildings;
        redBuildings = GameObject.FindGameObjectsWithTag("RedBuilding");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in redBuildings)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
