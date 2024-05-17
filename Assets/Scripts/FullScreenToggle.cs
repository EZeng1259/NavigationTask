using UnityEngine;
using UnityEngine.SceneManagement;

public class FullScreenManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        SetFullScreen();  // Ensure full-screen mode on start
    }

    void SetFullScreen()
    {
        if (!Screen.fullScreen)
        {
            Screen.fullScreen = true;
        }
    }
}
