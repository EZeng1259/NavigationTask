using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class RandomizeBuildings : MonoBehaviour
{
    private List<string> buildingNames = new List<string> {"Gym", "Hardware Store", "Music Store", "Pharmacy", "Bakery", "Bank", "Dentist", "Cafe", "Jewelry", "Butcher", "Supermarket",
               "Bike Shop", "Pizzeria", "Toy Store", "Book Store", "Barber", "Boutique", "Gallery", "Pet Store"};

    private static List<string> randomizedList = new List<string>(); 


    public GameObject redbuilding1;
    public GameObject redbuilding2;
    public GameObject redbuilding3;
    public GameObject redbuilding4;
    public GameObject redbuilding5;
    public GameObject redbuilding6;
    public GameObject redbuilding7;
    public GameObject redbuilding8;
    public GameObject redbuilding9;
    public GameObject redbuilding10;
    public GameObject redbuilding11;
    public GameObject redbuilding12;
    public GameObject redbuilding13;
    public GameObject redbuilding14;
    public GameObject redbuilding15;
    public GameObject redbuilding16;
    public GameObject redbuilding17;
    public GameObject redbuilding18;
    public GameObject redbuilding19;

    string filename = "";

    // Start is called before the first frame update
    void Start()
    {
        if (CountBuildings.trialNum == 1)
        {
            randomizedList = Shuffle(buildingNames);
            filename = @"UserData/RandomizedOrderData/" + PlayerID.id + ".csv";

            TextWriter writer = new StreamWriter(filename, true);

            for (int i = 0; i < randomizedList.Count; i++)
            {
                writer.WriteLine(i + 1 + "," + randomizedList[i]);
            }
            writer.Close();
        }

        redbuilding1.GetComponentInChildren<TMP_Text>().text = randomizedList[0];
        redbuilding1.name = randomizedList[0];

        redbuilding2.GetComponentInChildren<TMP_Text>().text = randomizedList[1];
        redbuilding2.name = randomizedList[1];

        redbuilding3.GetComponentInChildren<TMP_Text>().text = randomizedList[2];
        redbuilding3.name = randomizedList[2];

        redbuilding4.GetComponentInChildren<TMP_Text>().text = randomizedList[3];
        redbuilding4.name = randomizedList[3];

        redbuilding5.GetComponentInChildren<TMP_Text>().text = randomizedList[4];
        redbuilding5.name = randomizedList[4];

        redbuilding6.GetComponentInChildren<TMP_Text>().text = randomizedList[5];
        redbuilding6.name = randomizedList[5];

        redbuilding7.GetComponentInChildren<TMP_Text>().text = randomizedList[6];
        redbuilding7.name = randomizedList[6];

        redbuilding8.GetComponentInChildren<TMP_Text>().text = randomizedList[7];
        redbuilding8.name = randomizedList[7];

        redbuilding9.GetComponentInChildren<TMP_Text>().text = randomizedList[8];
        redbuilding9.name = randomizedList[8];

        redbuilding10.GetComponentInChildren<TMP_Text>().text = randomizedList[9];
        redbuilding10.name = randomizedList[9];

        redbuilding11.GetComponentInChildren<TMP_Text>().text = randomizedList[10];
        redbuilding11.name = randomizedList[10];

        redbuilding12.GetComponentInChildren<TMP_Text>().text = randomizedList[11];
        redbuilding12.name = randomizedList[11];

        redbuilding13.GetComponentInChildren<TMP_Text>().text = randomizedList[12];
        redbuilding13.name = randomizedList[12];

        redbuilding14.GetComponentInChildren<TMP_Text>().text = randomizedList[13];
        redbuilding14.name = randomizedList[13];

        redbuilding15.GetComponentInChildren<TMP_Text>().text = randomizedList[14];
        redbuilding15.name = randomizedList[14];

        redbuilding16.GetComponentInChildren<TMP_Text>().text = randomizedList[15];
        redbuilding16.name = randomizedList[15];

        redbuilding17.GetComponentInChildren<TMP_Text>().text = randomizedList[16];
        redbuilding17.name = randomizedList[16];

        redbuilding18.GetComponentInChildren<TMP_Text>().text = randomizedList[17];
        redbuilding18.name = randomizedList[17];

        redbuilding19.GetComponentInChildren<TMP_Text>().text = randomizedList[18];
        redbuilding19.name = randomizedList[18];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public List<T> Shuffle<T>(List<T> list)
    {
        for (var i = 0; i < list.Count - 1; ++i)
        {
            var random = UnityEngine.Random.Range(i, list.Count);
            var tmp = list[i];
            list[i] = list[random];
            list[random] = tmp;
        }
        return list; 
    }
}
