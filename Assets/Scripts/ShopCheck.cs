using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ShopCheck : MonoBehaviour
{
    List<string> wordsToCheck = FreeRecall.wordList;

    List<string> storeNames = new List<string>
             {"gym", "hardware store", "music store", "pharmacy", "bakery", "bank", "dentist", "cafe", "jewelry", "butcher", "supermarket",
               "bike shop", "pizzeria", "toy store", "book store", "barber", "boutique", "gallery", "pet store"};

    List<string> storesSeen = new List<string>(); 

    public TMP_Text rewardCounter;

    public static int correctStores = 0; 

    private void Start()
    {
        CheckWords();
    }

    private void CheckWords()
    {
        foreach (string word in wordsToCheck)
        {
            if((!storesSeen.Contains(word)) && (storeNames.Contains(word) || storeNames.Any(word1 => word1.Contains(word) && word.Length > word1.Length * 0.3)))
            {
                correctStores += 1;
                storesSeen.Add(word);
            }

        }
        rewardCounter.text = correctStores.ToString();
    }
}
