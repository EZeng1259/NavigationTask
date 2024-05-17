using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FinishedClick : MonoBehaviour {
	[SerializeField] GameObject[] Objects;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(RecordLocations);
    }

    public void RecordLocations(){
    	var csv = new StringBuilder();
        var columnNames = "ParticipantID,Store,xPos,yPos";
        string[] objNames;
        objNames = new string[19]{ "Gym", "Hardware Store", "Music Store", "Pharmacy",  "Bakery", "Bank", "Dentist", "Cafe", "Jewelry", "Butcher", "Supermarket",
                "Bike Shop", "Pizzeria", "Toy Store", "Book Store", "Barber", "Boutique", "Gallery", "Pet Store"};
        csv.AppendLine(columnNames);
        for (int i = 0; i < Objects.Length; i++)
        {
            var xPos = Objects[i].transform.position.x.ToString();
            var yPos = Objects[i].transform.position.y.ToString();
            var newLine = string.Format("{0},{1},{2},{3}", PlayerID.id, Objects[i].name, xPos, yPos);
            csv.AppendLine(newLine);
        }
        string filePath = @"UserData/MapData/" + PlayerID.id + ".csv";
        //after your loop
        File.WriteAllText(filePath, csv.ToString());
        // move to next scene
        SceneManager.LoadScene("Finished");
    }
}
