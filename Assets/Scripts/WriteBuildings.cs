using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class WriteBuildings : MonoBehaviour
{
    public Button quitButton;
    public Button returnButton; 
    public TMP_InputField input;
    string FILE_NAME = "Assets/FreeRecall.txt";

    // Start is called before the first frame update
    void Start()
    {
        quitButton.onClick.AddListener(quit);
        returnButton.onClick.AddListener(returnToEnvironment);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StreamWriter sw = File.AppendText(FILE_NAME);
            sw.WriteLine(input.text);
            sw.Close();

            input.text = "";
        }

    }

    public void quit()
    {
        Application.Quit(); 
    }

    public void getName()
    {
        StreamWriter sw = File.AppendText(FILE_NAME);
        sw.WriteLine(input.text);
        sw.Close();

        input.text = "";
    }

    public void returnToEnvironment()
    {
        SceneManager.LoadScene("NavigationScene");
    }

}
