using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayTime : MonoBehaviour
{
    public TMP_Text timeText;
    public Button continueButton; 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        timeText.text = CountBuildings.bestTotalDistance.ToString(); 
        continueButton.onClick.AddListener(nextScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void nextScene(){

        if(CountBuildings.trialNum == 1)
        {
            SceneManager.LoadScene("BreakScene");
        }
        else
        {
            SceneManager.LoadScene("RewardScene");
        }


    }
}
