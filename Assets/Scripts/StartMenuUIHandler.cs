using UnityEngine;

public class StartMenuUIHandler : MonoBehaviour
{
    [SerializeField] private Transform _scoreboardPanel;
    [SerializeField] private Transform _scoreboardSlotsParent;



    private void Start()
    {
        _scoreboardPanel.gameObject.SetActive(false);
    }


    public Transform GetScoreboardSlotsParentReference()
    {
        return _scoreboardSlotsParent;
    }


    public void StartGame()
    {
        MainManager.Instance.GlobalSceneManager.LoadLastScene();
    }


    public void ShowScoreboard()
    {
        _scoreboardPanel.gameObject.SetActive(!_scoreboardPanel.gameObject.activeInHierarchy);
    }



    public void QuitGame()
    {
        MainManager.Instance.GlobalSceneManager.ExitGame();
    }
}