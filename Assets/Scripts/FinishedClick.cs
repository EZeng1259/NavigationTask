using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class FinishedClick : MonoBehaviour
{
    [SerializeField] GameObject[] Objects;
    public Button button;

    public string apiUrl = "https://your-heroku-app.herokuapp.com/write_data"; // API URL

    private void Start()
    {
        button.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (DragandDrop.alreadyDragged.Count == 19)
        {
            button.gameObject.SetActive(true);
            button.onClick.AddListener(RecordLocations);
        }
    }

    public void RecordLocations()
    {
        string[] objNames = new string[19]
        {
            "Gym", "Hardware Store", "Music Store", "Pharmacy",  "Bakery", "Bank", "Dentist", "Cafe",
            "Jewelry", "Butcher", "Supermarket", "Bike Shop", "Pizzeria", "Toy Store", "Book Store",
            "Barber", "Boutique", "Gallery", "Pet Store"
        };

        for (int i = 0; i < Objects.Length; i++)
        {
            var xPos = Objects[i].transform.position.x;
            var yPos = Objects[i].transform.position.y;
            SendMapCoordinatesToServer(PlayerID.id, Objects[i].name, xPos, yPos);
        }

        // Move to next scene
        SceneManager.LoadScene("Finished");
    }

    public void SendMapCoordinatesToServer(string playerId, string storeName, float xPos, float yPos)
    {
        StartCoroutine(SendMapCoordinatesCoroutine(playerId, storeName, xPos, yPos));
    }

    private IEnumerator SendMapCoordinatesCoroutine(string playerId, string storeName, float xPos, float yPos)
    {
        var data = new
        {
            table_name = "mapcoordinates",
            participant_id = playerId,
            store_name = storeName,
            x_pos = xPos,
            y_pos = yPos
        };

        string jsonData = JsonUtility.ToJson(data);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(apiUrl, ""))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Map coordinates data sent successfully");
            }
        }
    }
}
