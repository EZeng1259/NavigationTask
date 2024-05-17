using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO; 

public class LastRewardToNavigation : MonoBehaviour
{
    public Button button;
    public string filename = "";

    // Start is called before the first frame update
    void Start()
    {
        //filename = @"UserData/RecallData/" + PlayerID.id + ".csv";

        button.onClick.AddListener(moveToMap);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moveToMap()
    {
        /*TextWriter writer1 = File.AppendText(filename);
        writer1.WriteLine("Final Score: " + CountBuildings.score);
        writer1.Close();*/
        SceneManager.LoadScene("MapSlide1");
    }

}
