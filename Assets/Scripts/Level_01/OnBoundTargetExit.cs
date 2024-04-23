using UnityEngine;

public class OnBoundTargetExit : MonoBehaviour
{
    [SerializeField] private Transform _levelManager;
    private string _targetTag = "Target";



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_targetTag))
        {
            if (MainManager.Instance != null)
            {
                var sceneManager = _levelManager.GetComponent<CurrentLevelUIManager>();
                sceneManager.OnTargetExitOutOfBound();
            }
            else
            {
                Debug.Log("FIX EXIT !");
            }
        }
    }
}
