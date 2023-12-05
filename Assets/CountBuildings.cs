using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System;


public class CountBuildings : MonoBehaviour
{
    public TMP_Text input;
    public int buildingCounter = 0; //counts number of red buildings encountered
    public int trialNum = 0; //counts number of times player has navigated the environment

    float minDist = 15f;

    List<string> buildingsVisited = new List<string>(); // keeps track of buildings visited 

    string filename = "";
    float time = 0f;
    float startTime = 0.00f; 
    [SerializeField] float interval = 250f;

    [System.Serializable]
    public class Datapoint
    {
        public float timestamp;
        public float x;
        public float y; 
        public float z;
        public float rotx;
        public float roty;
        public float rotz;
    }

    public class DataList
    {
        public Datapoint[] data;
    }

    public List<Datapoint> dataPoints = new List<Datapoint>(); 

    void Start()
    {
        trialNum++;
        filename = Application.dataPath + "/playerData_" + PlayerID.id + ".csv";
        TextWriter writer = File.AppendText(filename);
        writer.WriteLine("player ID, trial, timestamp, x, y, z, rotx, roty, rotz");
        writer.Close();
    }

    void Update()
    {
        float dist = Vector3.Distance(FindClosestRedBuilding().transform.position, transform.position);
        if (buildingsVisited.Contains(FindClosestRedBuilding().name)) { }
        else
        {
            if (dist <= minDist)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    buildingCounter++;
                    buildingsVisited.Add(FindClosestRedBuilding().name);
                    foreach (TMP_Text g in FindClosestRedBuilding().GetComponentsInChildren<TMP_Text>())
                    {
                        g.color = new Color(0, 0, 0);
                    }
                    input.text = "" + buildingCounter;
                }


                if (Input.GetKeyDown(KeyCode.P))
                {
                    if (buildingCounter > 0)
                    {
                        buildingCounter--;
                        input.text = "" + buildingCounter;
                    }
                }
            }
        }


        if (buildingCounter == 19)
        {
            SceneManager.LoadScene("StartRecallScene");
        }

        time += Time.deltaTime;
        if (time > interval)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            float rotate_x = transform.rotation.x;
            float rotate_y = transform.rotation.y;
            float rotate_z = transform.rotation.z;
            startTime += Time.deltaTime;
            float currTime = startTime; 

            Datapoint sample = new Datapoint();
            sample.timestamp = currTime;
            sample.x = x;
            sample.y = y; 
            sample.z = z;
            sample.rotx = rotate_x;
            sample.roty = rotate_y;
            sample.rotz = rotate_z;
            dataPoints.Add(sample);
            time = 0;
        }
        else
            time++;

        writeCSV();
        dataPoints.Clear(); 
    }

    public void writeCSV()
    {
        if (dataPoints.Count > 0)
        {
            TextWriter writer = new StreamWriter(filename, true);

            for (int i = 0; i < dataPoints.Count; i++)
            {
                writer.WriteLine(PlayerID.id + "," + trialNum + "," + dataPoints[i].timestamp + "," + dataPoints[i].x + "," + dataPoints[i].y + "," +
                    dataPoints[i].z + "," + dataPoints[i].rotx + "," + dataPoints[i].roty + "," +
                    dataPoints[i].rotz);
            }

            writer.Close();
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
