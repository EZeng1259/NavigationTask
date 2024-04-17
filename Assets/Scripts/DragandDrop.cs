using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class DragandDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
	private RectTransform rectTransform;
	private string filename;
	private float startTime; 

	List<string> alreadyDragged = new List<string>();

	private static int counter = 0; 

	public void Start () {
		filename = @"UserData/BuildingOrderData/" + PlayerID.id + ".csv";
		startTime = Time.time; 

		if(counter == 0)
        {
			TextWriter writer = new StreamWriter(filename, true);
			writer.WriteLine("Time" + "," + "Building Name");
			writer.Close();
			counter = 1; 
		}
	}


	private void Awake(){
		rectTransform = GetComponent<RectTransform>();
	}
	
	public void OnDrag(PointerEventData eventData){
		Debug.Log("OnDrag");
		rectTransform.position = Input.mousePosition;
		rectTransform.anchoredPosition += eventData.delta;
	}
	public void OnBeginDrag(PointerEventData eventData){
		Debug.Log("BeginDrag");
	}

	public void OnEndDrag(PointerEventData eventData){

		print(rectTransform.position);
		if(!alreadyDragged.Contains(rectTransform.name)){
			if(rectTransform.position.x < 1150)
            {
				TextWriter writer = new StreamWriter(filename, true);

				float currTime = Time.time - startTime;
				writer.WriteLine(currTime + "," + rectTransform.name);
				writer.Close();

				alreadyDragged.Add(rectTransform.name);
			}
		}

	}

}
