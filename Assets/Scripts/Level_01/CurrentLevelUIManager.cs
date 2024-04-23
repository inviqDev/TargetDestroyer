using UnityEngine;
using TMPro;


public class CurrentLevelUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTextField;
    private string _defaultText = "DESTROYED : ";


    [SerializeField] private Canvas _gameOnCanvas;
    [SerializeField] private Canvas _gameOverCanvas;

    [SerializeField] private TextMeshProUGUI _finalScoreText;
    [SerializeField] private Transform _saveScoreUI;
    [SerializeField] private TextMeshProUGUI _playerInputField;



    private void Start()
    {
        if (MainManager.Instance != null)
        {
            SetUpOnStartUIAppearence();

            string currentScore = MainManager.Instance.ScoreManager.CurrentScore.ToString();
            _scoreTextField.text = _defaultText + currentScore;
        }
        else
        {
            Debug.Log("MainManager component is not found in the game !");
        }
        
    }


    private void SetUpOnStartUIAppearence()
    {
        _gameOnCanvas.gameObject.SetActive(true);
        _gameOverCanvas.gameObject.SetActive(false);
        _saveScoreUI.gameObject.SetActive(false);
    }


    public void OnPlayButtonClick()
    {
        MainManager.Instance.GlobalSceneManager.PauseGame();
    }


    public void OnPauseButtonClick()
    {
        MainManager.Instance.GlobalSceneManager.PauseGame();
    }


    public void OpenMainMenu()
    {
        MainManager.Instance.GlobalSceneManager.LoadStartMenu();
    }



    public void OnTargetExitOutOfBound()
    {
        _gameOnCanvas.gameObject.SetActive(false);
        _gameOverCanvas.gameObject.SetActive(true);

        MainManager.Instance.GlobalSceneManager.PauseGame();

        string finalScoreDefaultText = _finalScoreText.text;
        string scoreValue = MainManager.Instance.ScoreManager.CurrentScore.ToString();
        _finalScoreText.text = finalScoreDefaultText + scoreValue;
    }


    public void SetScoreTextFieldValue()
    {
        MainManager.Instance.ScoreManager.IncreaseCurrentScoreValue();
        _scoreTextField.text = _defaultText + MainManager.Instance.ScoreManager.CurrentScore;
    }


    public void RestartGame()
    {
        MainManager.Instance.GlobalSceneManager.ReloadCurrentScene();
    }


    public void OpenSaveScoreMenu()
    {
        _saveScoreUI.gameObject.SetActive(!_saveScoreUI.gameObject.activeInHierarchy);
    }

    public void SaveCurrentScore()
    {
        string playerName = _playerInputField.text;
        MainManager.Instance.ScoreManager.SaveScore(playerName);

        OpenSaveScoreMenu();
    }


    public void ExitGame()
    {
        MainManager.Instance.GlobalSceneManager.ExitGame();
    }
}