using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ChangeBackgroundWarmup : MonoBehaviour
{

    private string folderPath;
    List<Texture2D> images;
    private int counter;

    [SerializeField] private Image setImage;

    public string url; 

    IEnumerator Start()
    {
        ///url = Application.dataPath + "/StreamingAssets/shareImage.png";
        url = System.IO.Path.Combine(Application.streamingAssetsPath, "ImagesWarmup/Slide1.jpg");

        byte[] imgData;
        Texture2D tex = new Texture2D(2, 2);

        //Check if we should use UnityWebRequest or File.ReadAllBytes
        if (url.Contains("://") || url.Contains(":///"))
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            imgData = www.downloadHandler.data;
        }
        else
        {
            imgData = File.ReadAllBytes(url);
        }
        Debug.Log(imgData.Length);

        //Load raw Data into Texture2D 
        tex.LoadImage(imgData);

        //Convert Texture2D to Sprite
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);

        //Apply Sprite to SpriteRenderer
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
    }


    /*
    // Start is called before the first frame update
    void Start()
    {
        counter = 0; 
        folderPath = Application.dataPath + "/StreamingAssets/ImagesWarmup";
        string[] files = Directory.GetFiles(folderPath, "*.jpg");
        images = new List<Texture2D>(); 

        foreach(string filePath in files)
        {
            byte[] imageData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imageData);
            images.Add(texture);
        }

        setImage.sprite = Sprite.Create(images[counter], new Rect(0, 0, images[counter].width, images[counter].height), Vector2.zero);
    }
    */

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(counter < images.Count - 1)
            {
                counter += 1; 
                setImage.sprite = Sprite.Create(images[counter], new Rect(0, 0, images[counter].width, images[counter].height), Vector2.zero);
            }

        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(counter - 1 < 0)
            {

            }
            else{
                counter--; 
                setImage.sprite = Sprite.Create(images[counter], new Rect(0, 0, images[counter].width, images[counter].height), Vector2.zero);
            }
        }

        if(Input.GetKeyDown(KeyCode.Return)  && counter == 8)
        {
            SceneManager.LoadScene("WarmUpScene");
        }
    }


}

