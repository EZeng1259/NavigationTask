using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;

public class CountBuildingsWarmup : MonoBehaviour
{
    public TMP_Text toptext;
    public int buildingCounter = 0;

    float minDist = 6.5f;

    List<string> buildingsVisited = new List<string>(); // keeps track of buildings visited 

    float time = 0f;
    private float startTime;
    [SerializeField] float interval = 250f;

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

    public List<Datapoint> dataPoints = new List<Datapoint>();

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float dist = Vector3.Distance(FindClosestRedBuilding().GetComponentInChildren<TMP_Text>().transform.position, transform.position);

        if (buildingsVisited.Contains(FindClosestRedBuilding().name))
        {
            toptext.SetText("Follow the white arrows");
        }
        else
        {
            toptext.SetText("Follow the white arrows");
            if (dist <= minDist)
            {
                toptext.SetText("Press 'Spacebar' when you are directly in front of the name of a red building");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    buildingCounter++;
                    Debug.Log(buildingCounter);
                    buildingsVisited.Add(FindClosestRedBuilding().name);
                    foreach (TMP_Text g in FindClosestRedBuilding().GetComponentsInChildren<TMP_Text>())
                    {
                        g.color = new Color(0, 0, 0);
                    }
                }
            }
        }

        if (buildingCounter == 3)
        {
            toptext.SetText("Freely explore the environment and press 'esc' when you're ready to move on");

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("InstructionScene");
            }
        }

        time += Time.deltaTime;
        if (time > interval)
        {
            float x = transform.position.x;
            float y = transform.position.z;
            float rotate_x = CameraMovement.yRotation % 360;
            float rotate_y = CameraMovement.xRotation % 360;
            float currTime = Time.time - startTime;

            Datapoint sample = new Datapoint();
            sample.timestamp = currTime;
            sample.x = x;
            sample.y = y;
            sample.rotx = rotate_x;
            sample.roty = rotate_y;
            dataPoints.Add(sample);
            time = 0;

            // Send data to server
            SendDataToServer(PlayerID.id, sample.timestamp, sample.x, sample.y, sample.rotx, sample.roty);
        }
    }

    public void SendDataToServer(string playerId, float timestamp, float x, float y, float rotx, float roty)
    {
        StartCoroutine(SendDataCoroutine(playerId, timestamp, x, y, rotx, roty));
    }

    private IEnumerator SendDataCoroutine(string playerId, float timestamp, float x, float y, float rotx, float roty)
    {
        var data = new
        {
            table_name = "warmupnavigation",
            participant_id = playerId,
            player_id = playerId,
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
                Debug.Log("Data sent successfully to WarmupNavigation table");
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
