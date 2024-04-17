using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RewardToNavigation : MonoBehaviour
{
    public Button button;
    public TMP_Text rewardText; 

    // Start is called before the first frame update
    void Start()
    {
        rewardText.text = CountBuildings.score.ToString();
        button.onClick.AddListener(switchScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchScene()
    {
        if (CountBuildings.trialNum == 8){
            SceneManager.LoadScene("StartRecallScene");
        }
        else {
            SceneManager.LoadScene("BreakScene");
        }
    }

}
