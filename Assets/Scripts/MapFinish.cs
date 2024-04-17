using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(finish);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void finish()
    {
        Application.Quit(); 
    }

}
