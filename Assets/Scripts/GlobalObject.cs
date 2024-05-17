using UnityEngine;

public class GlobalObject : MonoBehaviour
{
    private static GlobalObject _instance;

    void Awake()
    {
        // If there's already an instance, destroy this one
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // Make this object persistent
        }
    }
}
