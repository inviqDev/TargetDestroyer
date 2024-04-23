using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneManager : MonoBehaviour
{
    [SerializeField] private SceneAsset[] _scenes;
    private int _startMenuSceneIndex = 0;
    private int lastSceneIndex = 1;


    private void Awake()
    {
        UnpauseGame();
    }



    public void PauseGame(float timeScaleValue = 0.0f, bool isVisible = true)
    {
        Time.timeScale = timeScaleValue;
        // Cursor.visible = isVisible;
    }


    public void UnpauseGame(float timeScaleValue = 1.0f, bool isVisible = false)
    {
        Time.timeScale = 1.0f;
        // To implement in the future :         Cursor.visible = false;
    }


    public void LoadStartMenu()
    {
        SceneManager.LoadScene(_startMenuSceneIndex);
    }


    public void LoadLastScene()
    {
        SceneManager.LoadScene(lastSceneIndex);
    }


    public void ReloadCurrentScene()
    {
        UnpauseGame();

        MainManager.Instance.ScoreManager.ResetPlayerCurrentScoreValue();

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}