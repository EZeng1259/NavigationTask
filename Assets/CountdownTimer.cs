using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 60f;

    [SerializeField] TMP_Text countdownText; 

    private void Start()
    {
        currentTime = startingTime;
        Cursor.visible = true;
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");
        if (currentTime <= 0)
        {
            SceneManager.LoadScene("FreeRecallScene");
            print(currentTime);
        }
    }
}
