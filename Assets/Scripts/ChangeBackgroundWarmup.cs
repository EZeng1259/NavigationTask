using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement; 


public class ChangeBackgroundWarmup : MonoBehaviour
{

    private string folderPath;
    List<Texture2D> images;
    private int counter;

    [SerializeField] private Image setImage; 

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
