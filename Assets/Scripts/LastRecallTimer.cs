using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LastRecallTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 120f;
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
            SceneManager.LoadScene("LastRewardScene");
        }
    }
}
