using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayCurrTime : MonoBehaviour
{
    public TMP_Text currTimeText;

    // Start is called before the first frame update
    void Start()
    {    
        currTimeText.text = CountBuildings.totalDistance.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
