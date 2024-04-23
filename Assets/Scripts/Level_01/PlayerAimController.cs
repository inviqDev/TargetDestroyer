using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAimController : MonoBehaviour
{
    [SerializeField] private CurrentLevelUIManager _currentUIManager;

    [SerializeField] private Transform _weapon;
    [SerializeField] private Transform _aimSprite;

    private Camera _mainCamera;
    public LayerMask hitDetector;
    private Quaternion _aimDefaultRotation;

    private Vector3 _defaultAimScale;
    private Vector3 _scaledAimSize;

    private float offsetMultiplier = -8;


    private void Awake()
    {
        _mainCamera = Camera.main;

        _aimDefaultRotation = _aimSprite.rotation;

        _defaultAimScale = _aimSprite.localScale;
        float scaleMultiplier = 2.0f;
        _scaledAimSize = _defaultAimScale * scaleMultiplier;
    }

    private void LateUpdate()
    {
        RotateWeapon();
    }


    
    private void RotateWeapon()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitDetector))
        {
            Vector3 hitDirection = hit.point - _weapon.position;
            Quaternion lookAt = Quaternion.LookRotation(hitDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, 0.5f);

            Vector3 direction = hit.point - transform.position;
            Vector3 offset = direction.normalized * offsetMultiplier;
            _aimSprite.SetPositionAndRotation(hit.point + offset, _aimDefaultRotation);

            _aimSprite.localScale = _defaultAimScale;
        }
    }
}