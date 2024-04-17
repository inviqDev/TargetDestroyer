using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Transform _startGameMenu;
    [SerializeField] private Transform _endGameMenu;

    private float _playGameTimeScaleValue = 1.0f;
    private float _pauseGameTimeScaleValue = 0.0f;



    private void Awake()
    {
        Time.timeScale = _pauseGameTimeScaleValue;
        Cursor.visible = true;


        if (!_startGameMenu.gameObject.activeInHierarchy)
        {
            _startGameMenu.gameObject.SetActive(true);
        }
    }



    public void StartGame()
    {
        Time.timeScale = _playGameTimeScaleValue;
        Cursor.visible = false;

        _startGameMenu.gameObject.SetActive(false);
        _endGameMenu.gameObject.SetActive(false);
    }


    public void ShowEndGameMenu()
    {
        Time.timeScale = _pauseGameTimeScaleValue;
        Cursor.visible = true;

        _endGameMenu.gameObject.SetActive(true);
    }


    public void RestartCurrentScene()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
    }
}
