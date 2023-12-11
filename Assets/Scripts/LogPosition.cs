using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System; 

public class LogPosition : MonoBehaviour
{
    string FILE_NAME = "Assets/position.txt";
    float time = 0f;
    [SerializeField] float interval = 250f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; 
        if (time > interval)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            float rotate_x = transform.rotation.x;
            float rotate_y = transform.rotation.y;
            float rotate_z = transform.rotation.z;

            StreamWriter sw = File.AppendText(FILE_NAME);
            DateTime currTime = System.DateTime.Now;
            sw.WriteLine("[" + currTime.ToString() +  "]:      " + "Coordinates: " + "(" + x + ", " + z + ")              " + "Rotation: " + "(" + rotate_x + ", " + rotate_y + ", " + rotate_z + ")");
            sw.Close();
            Debug.Log("Processed");
            time = 0;
        }
        else
            time++;
    }
}
