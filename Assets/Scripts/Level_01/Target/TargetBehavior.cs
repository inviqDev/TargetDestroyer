using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    private TargetManager _targetManager;
    private PlayerAimController _playerAimController;

    private string _playZoneTag = "PlayZone";
    private Bounds _playZoneBounds;
    private string _boundTag = "Bound";

    private Rigidbody _targetRb;

    private Vector3 _defaultLocalScale;
    private float _minScaleModifier = 0.75f;
    private float _maxScaleModifier = 2.25f;

    private Vector3 _startPosition;

    public float minSpeedForce;
    public float maxSpeedForce;
    public float torqueForce;

    public bool NeedToLaunch { get; private set; }


    private void Awake()
    {
        _playerAimController = SetAimControllerReference();
        GameObject playZone = GameObject.FindGameObjectWithTag(_playZoneTag);

        if (playZone != null && _playerAimController != null)
        {
            _targetManager = transform.parent.GetComponent<TargetManager>();
            _startPosition = transform.position;

            BoxCollider playZoneBox = playZone.GetComponent<BoxCollider>();
            _playZoneBounds = playZoneBox.bounds;
        }

        _targetRb = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        _defaultLocalScale = _targetRb.transform.localScale;
    }


    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (NeedToLaunch)
            {
                LaunchTarget();
            }
        }
    }


    private PlayerAimController SetAimControllerReference()
    {
        PlayerAimController aimController = FindObjectOfType<PlayerAimController>();
        if (aimController != null)
        {
            return aimController;
        }
        else
        {
            Debug.Log("PlayerAimController component is not found in the game !");
            return null;
        }
    }


    private Vector3 GetPointToMovePosition()
    {
        float xPosition = Random.Range(_playZoneBounds.min.x, _playZoneBounds.max.x);
        float yPosition = Random.Range(_playZoneBounds.min.y, _playZoneBounds.max.y);
        float zPosition = _playZoneBounds.center.z;

        Vector3 pointToMove = new(xPosition, yPosition, zPosition);
        return pointToMove;
    }

    private void AddForceToTarget()
    {
        Vector3 pointToMove = GetPointToMovePosition();
        Vector3 moveDirection = pointToMove - transform.position;
        moveDirection.Normalize();

        float moveSpeed = Random.Range(minSpeedForce, maxSpeedForce);

        _targetRb.AddForce(moveDirection * moveSpeed, ForceMode.Impulse);
    }

    private void AddTorqueToTarget()
    {
        float xTorqueValue = GetRandomTorqueValue();
        float yTorqueValue = GetRandomTorqueValue();
        float zTorqueValue = GetRandomTorqueValue();
        Vector3 torqueDirection = new(xTorqueValue, yTorqueValue, zTorqueValue);

        _targetRb.AddTorque(torqueDirection * torqueForce, ForceMode.Impulse);
    }

    private float GetRandomTorqueValue(float torqueValue = 10.0f)
    {
        return Random.Range(-torqueValue, torqueValue);
    }

    private Vector3 SetRandomLocalScale()
    {
        float scaleModifier = Random.Range(_minScaleModifier, _maxScaleModifier);
        Vector3 randomScale = _targetRb.transform.localScale * scaleModifier;

        return randomScale;
    }



    private void LaunchTarget()
    {
        _targetRb.transform.localScale = SetRandomLocalScale();

        AddForceToTarget();
        AddTorqueToTarget();

        SetNeedToLaunchBoolean(false);
    }

    public void SetNeedToLaunchBoolean(bool currentValue)
    {
        NeedToLaunch = currentValue;
    }




    private void ResetTargetBehavior()
    {
        transform.position = _targetManager.SetTargetStartPosition();

        _targetRb.velocity = Vector3.zero;
        _targetRb.angularVelocity = Vector3.zero;
        _targetRb.transform.localScale = _defaultLocalScale;

        gameObject.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_boundTag))
        {
            CurrentLevelSceneLoader sceneLoader = GameObject.FindObjectOfType<CurrentLevelSceneLoader>();
            sceneLoader.ShowEndGameMenu();
            Debug.Log("FIX");
        }
    }


    private void OnMouseDown()
    {
        ResetTargetBehavior();

        _targetManager.LaunchTargetWave();
        _targetManager.PlayOnDestroySound();

        _targetManager.OnClickIncreaseScore();
    }
}