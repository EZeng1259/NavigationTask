using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Reminder : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(changeToNavigationScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeToNavigationScene()
    {
        SceneManager.LoadScene("NavigationScene");
    }
}
