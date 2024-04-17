using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;


public class LastShopCheck : MonoBehaviour
{
    List<string> wordsToCheck = FreeRecall.wordList;

    List<string> storeNames = new List<string>
            {"gym", "hardware store", "music store", "pharmacy", "bakery", "bank", "dentist", "cafe", "jewelry", "butcher", "supermarket",
               "bike shop", "pizzeria", "toy store", "book store", "barber", "boutique", "gallery", "pet store"};

    List<string> storesSeen = new List<string>();

    public TMP_Text rewardCounter;

    private void Start()
    {
        CheckWords();
    }

    private void CheckWords()
    {
        for (int i = 0; i < wordsToCheck.Count; i++)
        {
            if (wordsToCheck[i].Equals("pizzaria") || wordsToCheck[i].Equals("pizzerria") || wordsToCheck[i].Equals("pizeria") || wordsToCheck[i].Equals("pizzareia"))
            {
                wordsToCheck[i] = "pizzeria";
            }

            if (wordsToCheck[i].Equals("jewery") || wordsToCheck[i].Equals("jewlery") || wordsToCheck[i].Equals("jewellery") || wordsToCheck[i].Equals("jewellery") || wordsToCheck[i].Equals("jewelru"))
            {
                wordsToCheck[i] = "jewelry";
            }

            if (wordsToCheck[i].Equals("mucis") || wordsToCheck[i].Equals("musicstore"))
            {
                wordsToCheck[i] = "music";
            }

            if (wordsToCheck[i].Equals("gallary"))
            {
                wordsToCheck[i] = "gallery";
            }

            if (wordsToCheck[i].Equals("botique") || wordsToCheck[i].Equals("bontique") || wordsToCheck[i].Equals("bouqiet"))
            {
                wordsToCheck[i] = "boutique";
            }

            if (wordsToCheck[i].Equals("baurber"))
            {
                wordsToCheck[i] = "barber";
            }

            if (wordsToCheck[i].Equals("bookstore"))
            {
                wordsToCheck[i] = "book store";
            }

            if (wordsToCheck[i].Equals("bikeshop"))
            {
                wordsToCheck[i] = "bike shop";
            }

            if (wordsToCheck[i].Equals("toystore"))
            {
                wordsToCheck[i] = "toy store";
            }

            if (wordsToCheck[i].Equals("petstore"))
            {
                wordsToCheck[i] = "pet store";
            }

            if (wordsToCheck[i].Equals("hardwarestore"))
            {
                wordsToCheck[i] = "hardware store";
            }

            if (wordsToCheck[i].Equals("super market"))
            {
                wordsToCheck[i] = "supermarket";
            }
        }

        foreach (string word in wordsToCheck)
        {
            if ((!storesSeen.Contains(word)) && (storeNames.Contains(word) || storeNames.Any(word1 => word1.Contains(word) && word.Length > word1.Length * 0.3)))
            {
                CountBuildings.score += 1;
                storesSeen.Add(word);
            }

        }
        rewardCounter.text = CountBuildings.score.ToString();
    }
}
