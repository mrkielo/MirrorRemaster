using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] bool isOverride;

    void Awake()
    {
        if (isOverride)
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
                instance = this;
            }
        }
        else
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
