using System.Collections;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    private GameObject[] _pooledTargets;

    [SerializeField] private BoxCollider[] _startPositonBoxes;

    public int targetsAmountToPool;
    public int _amountToLaunch;

    private CurrentLevelUIManager _levelUIManager;
    private AudioSource _audioSource;



    void Awake()
    {
        ObjectPooler pooler = ObjectPooler.Instance;

        if (pooler != null)
        {
            if (_pooledTargets == null)
            {
                _pooledTargets = SetUpPooledTargetsArray(pooler);
            }

            _levelUIManager = GetComponent<CurrentLevelUIManager>();
            _audioSource = GetComponent<AudioSource>();

            StartCoroutine(nameof(LaunchTargetCoroutine));
        }
    }



    private GameObject[] SetUpPooledTargetsArray(ObjectPooler poolerScript)
    {
        GameObject[] pooledTargets = poolerScript.SetPooledObjectsArray(targetsAmountToPool, _target, transform);

        // change start position for all pooled targets
        foreach (GameObject target in pooledTargets)
        {
            target.transform.position = SetTargetStartPosition();
        }

        return pooledTargets;
    }

    public Vector3 SetTargetStartPosition()
    {
        int randomIndex = Random.Range(0, _startPositonBoxes.Length);
        BoxCollider selectedBox = _startPositonBoxes[randomIndex];

        float xPosition = selectedBox.bounds.center.x;
        float yPosition = Random.Range(selectedBox.bounds.min.y, selectedBox.bounds.max.y);
        float zPosition = selectedBox.gameObject.transform.position.z;

        Vector3 startPosition = new(xPosition, yPosition, zPosition);
        return startPosition;
    }

    private void LaunchTarget(int index)
    {
        if (!_pooledTargets[index].activeInHierarchy)
        {
            GameObject targetToLaunch = _pooledTargets[index];
            TargetBehavior targetBehavior = targetToLaunch.GetComponent<TargetBehavior>();
            targetToLaunch.SetActive(true);

            targetBehavior.SetNeedToLaunchBoolean(true);
        }
    }

    private IEnumerator LaunchTargetCoroutine()
    {
        float launchDelay = 2.0f;
        yield return new WaitForSeconds(launchDelay);

        LaunchTargetWave();

        float stopDelay = 2.0f;
        Invoke(nameof(StopLaunchingCoroutine), stopDelay);
    }

    public void StopLaunchingCoroutine()
    {
        StopCoroutine(nameof(LaunchTargetCoroutine));
    }



    public void LaunchTargetWave()
    {
        bool activeTargetExist = FindActiveTargets();
        if (!activeTargetExist)
        {
            for (int i = 0; i < _amountToLaunch; i++)
            {
                LaunchTarget(i);
            }

            _amountToLaunch++;
        }
    }
    private bool FindActiveTargets()
    {
        for (int i = 0; i < _pooledTargets.Length; i++)
        {
            if (_pooledTargets[i].activeInHierarchy)
            {
                bool foundActiveTarget = true;
                return foundActiveTarget;
            }
        }

        return false;
    }



    public void PlayOnDestroySound()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
    }

    public void OnClickIncreaseScore()
    {
        _levelUIManager.SetScoreTextFieldValue();
    }
}