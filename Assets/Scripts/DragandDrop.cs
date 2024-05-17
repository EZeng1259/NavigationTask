using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragandDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private float startTime;
    public Image image;

    public static List<string> alreadyDragged = new List<string>();

    private static int counter = 0;

    public string apiUrl = "https://salty-thicket-48002.herokuapp.com/write_data"; // API URL

    public void Start()
    {
        startTime = Time.time;

        if (counter == 0)
        {
            counter = 1;
        }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Begin Drag
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform imageCoords = image.rectTransform;
        Vector3[] corners = new Vector3[4];
        imageCoords.GetWorldCorners(corners);

        if (!alreadyDragged.Contains(rectTransform.name))
        {
            if (rectTransform.position.x < corners[2].x && rectTransform.position.x > corners[0].x && rectTransform.position.y < corners[2].y && rectTransform.position.y > corners[0].y)
            {
                float currTime = Time.time - startTime;
                SendMapPlacementOrderToServer(PlayerID.id, currTime, rectTransform.name);
                alreadyDragged.Add(rectTransform.name);
            }
        }
    }

    public void SendMapPlacementOrderToServer(string playerId, float timestamp, string buildingName)
    {
        StartCoroutine(SendMapPlacementOrderCoroutine(playerId, timestamp, buildingName));
    }

    private IEnumerator SendMapPlacementOrderCoroutine(string playerId, float timestamp, string buildingName)
    {
        var data = new
        {
            table_name = "mapplacementorder",
            participant_id = playerId,
            timestamp = timestamp,
            building_name = buildingName
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
                Debug.Log("Map placement order data sent successfully");
            }
        }
    }
}
