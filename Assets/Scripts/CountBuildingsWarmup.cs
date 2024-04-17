using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System;


public class CountBuildingsWarmup : MonoBehaviour
{
    public TMP_Text toptext; 
    public int buildingCounter = 0;

    float minDist = 6.5f;

    List<string> buildingsVisited = new List<string>(); // keeps track of buildings visited 

    string filename = "";
    float time = 0f;
    private float startTime;
    [SerializeField] float interval = 250f;

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
        filename = @"UserData/WarmupNavigationData/" + PlayerID.id + ".csv";

        TextWriter writer = new StreamWriter(filename, true);
        writer.WriteLine("player ID, timestamp, x, y, rotx, roty");
        writer.Close();

    }

    void Update()
    {
        float dist = Vector3.Distance(FindClosestRedBuilding().GetComponentInChildren<TMP_Text>().transform.position, transform.position);
     
        if (buildingsVisited.Contains(FindClosestRedBuilding().name)) {toptext.SetText("Follow the white arrows"); }
        else
        {
            toptext.SetText("Follow the white arrows");
            if (dist <= minDist)
            {
                toptext.SetText("Press 'Spacebar' when you are near a red building");
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
                writer.WriteLine(PlayerID.id + "," + dataPoints[i].timestamp + "," + dataPoints[i].x + "," + dataPoints[i].y + "," + dataPoints[i].rotx + "," + dataPoints[i].roty);
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
