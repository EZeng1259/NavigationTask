using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RewardToNavigationWarmup : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(switchScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchScene()
    {
        SceneManager.LoadScene("InstructionScene");
    }

}
