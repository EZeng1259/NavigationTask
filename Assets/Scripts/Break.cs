using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 

public class Break : MonoBehaviour
{
    public Button button;
    public TMP_Text roundsLeft; 
    public float timeToWait = 30f;
    private float currentWaitTime;
    private bool checkTime;

    void Awake()
    {
        ResetTimer();
    }

    private void Start()
    {
        roundsLeft.SetText((8 - CountBuildings.trialNum).ToString());
    }

    void Update()
    {
        if (checkTime)
        {
            currentWaitTime -= Time.deltaTime;
            button.GetComponentInChildren<TextMeshProUGUI>().text = Mathf.Round(currentWaitTime).ToString();
            if (currentWaitTime < 0)
            {
                TimerFinished();
                checkTime = false;
            }
        }
    }

    public void ResetTimer()
    {
        currentWaitTime = timeToWait;
        checkTime = true;
        button.interactable = false; 
    }
    void TimerFinished()
    {
        button.interactable = true;
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Continue"; 
        button.onClick.AddListener(changeScene);
    }

    void changeScene()
    {
        SceneManager.LoadScene("ReminderScene");
    }
}
