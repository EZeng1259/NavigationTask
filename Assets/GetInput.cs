using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class GetInput : MonoBehaviour
{
    public Button button;
    public TMP_InputField input;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(getInput);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getInput()
    {
        SceneManager.LoadScene("NavigationScene");
    }
}
