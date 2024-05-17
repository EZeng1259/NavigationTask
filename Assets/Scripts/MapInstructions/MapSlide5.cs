using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class MapSlide5  : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("DragAndDrop");
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SceneManager.LoadScene("MapSlide4");
        }

    }
}
