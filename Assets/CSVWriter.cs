using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public class CSVWriter : MonoBehaviour
{
    string filename = "";
    public static string id; 

    [System.Serializable]
    public class Datapoint
    {
        public int trial; 
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

    public DataList dataPoints = new DataList(); 


    void Start()
    {
        filename = Application.dataPath + "/test.csv";
    }

    void Update()
    {
        writeCSV(); 
    }

    public void writeCSV()
    {
        if(dataPoints.data.Length > 0)
        {
            TextWriter writer = new StreamWriter(filename, false);
            writer.WriteLine("trial, x, y, z, rotx, roty, rotz");
            writer.Close();

            writer = new StreamWriter(filename, true);

            for(int i = 0; i < dataPoints.data.Length; i++)
            {
                writer.WriteLine(dataPoints.data[i].trial + "," + dataPoints.data[i].x + "," +
                    dataPoints.data[i].z + "," + dataPoints.data[i].rotx + "," + dataPoints.data[i].roty + "," +
                    dataPoints.data[i].rotz);
            }

            writer.Close(); 
        }

    }

}
