using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    [SerializeField] private Transform _aimSprite;

    private Camera _mainCamera;
    private Vector3 _mainCameraPos;
    private float offset = 4.0f;



    private void Awake()
    {
        Cursor.visible = false;
    }


    private void Start()
    {
        _mainCamera = Camera.main;
        _mainCameraPos = _mainCamera.transform.position;
    }


    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = hit.point - _mainCameraPos;
            direction.Normalize();

            Vector3 offsetPosition = direction * -offset;
            _aimSprite.position = hit.point + offsetPosition;
        }
    }
}