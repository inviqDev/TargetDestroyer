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
        Time.timeScale = 0.0f;
        Cursor.visible = true;
    }



    public void PauseGame(float timeScaleValue = 0.0f, bool isVisible = true)
    {
        Time.timeScale = timeScaleValue;
        Cursor.visible = isVisible;
    }


    public void UnpauseGame(float timeScaleValue = 1.0f, bool isVisible = false)
    {
        Time.timeScale = 1.0f;
        Cursor.visible = isVisible;
        // To implement in the future :         Cursor.visible = false;
    }


    public void LoadStartMenu(bool isVisible = true)
    {
        Cursor.visible = isVisible;

        SceneManager.LoadScene(_startMenuSceneIndex);
    }


    public void LoadLastScene()
    {
        UnpauseGame();
        MainManager.Instance.ScoreManager.ResetPlayerCurrentScoreValue();

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