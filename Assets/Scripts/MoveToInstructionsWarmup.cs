using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;


public class MoveToInstructionsWarmup : MonoBehaviour
{
    public Button button;
    public TMP_InputField input;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(changeScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerID.id = input.text; 
            SceneManager.LoadScene("WarmupSlide1");
        }
    }

    void changeScene()
    {
        SceneManager.LoadScene("WarmupSlide1");
    }
}
