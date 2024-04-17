using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public static float currentTime = 0f;
    float startingTime = 2000f;

    private void Start()
    {
        currentTime = startingTime;
        Cursor.visible = true;
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if (currentTime <= 0)
        {       
            if(CountBuildings.trialNum == 1)
            {
                SceneManager.LoadScene("BestTimeScene");
            }
            else
            {
                SceneManager.LoadScene("FailBestTimeScene");
            }
        }
    }
}
