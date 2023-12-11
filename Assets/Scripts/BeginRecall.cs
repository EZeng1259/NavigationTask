using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginRecall : MonoBehaviour
{
    public static int trialNum = 0; 
    
    // Start is called before the first frame update
    void Start()
    {
        trialNum++;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(trialNum >= 5)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("LastFreeRecallScene");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("FreeRecallScene");
            }
        }
    }
}
