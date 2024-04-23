using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }


    public GlobalSceneManager GlobalSceneManager { get; private set; }
    public ScoreManager ScoreManager { get; private set; }
    


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            GlobalSceneManager = GetComponent<GlobalSceneManager>();
            ScoreManager = GetComponent<ScoreManager>();
        }
    }
}