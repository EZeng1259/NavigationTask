using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerID: MonoBehaviour
{
    public Button button;
    public TMP_InputField input;

    //public static PlayerID Instance;
    public static string id; 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        button.onClick.AddListener(getInput);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getInput()
    {
        id = input.text;
    }
}
