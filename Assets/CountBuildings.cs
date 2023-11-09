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

    string filename = "";
    float time = 0f;
    [SerializeField] float interval = 250f;

    [System.Serializable]
    public class Datapoint
    {
        public string timestamp;
        public float x;
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
        filename = Application.dataPath + "/test.csv";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buildingCounter++;
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

        if (buildingCounter == 19)
        {
            SceneManager.LoadScene("FreeRecallScene");
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
            DateTime currTime = System.DateTime.Now;

            Datapoint sample = new Datapoint();
            sample.timestamp = currTime.ToString();
            sample.x = x;
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
    }

    public void writeCSV()
    {
        if (dataPoints.Count > 0)
        {
            TextWriter writer = new StreamWriter(filename, false);
            writer.WriteLine("trial, x, y, z, rotx, roty, rotz");
            writer.Close();

            writer = new StreamWriter(filename, true);

            for (int i = 0; i < dataPoints.Count; i++)
            {
                writer.WriteLine(dataPoints[i].timestamp + "," + dataPoints[i].x + "," +
                    dataPoints[i].z + "," + dataPoints[i].rotx + "," + dataPoints[i].roty + "," +
                    dataPoints[i].rotz);
            }

            writer.Close();
        }

    }
    
}
