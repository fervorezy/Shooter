using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _minClamp;
    [SerializeField] private float _maxClamp;
    [SerializeField] private Camera _camera;

    private Vector2 _cameraRotation;
    private Vector2 _playerRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Look(Vector2 rotate)
    {
        if (rotate.sqrMagnitude < 0.1)
            return;

        float scaledRotateSpeed = _rotateSpeed * Time.deltaTime;
        
        _cameraRotation.x = Mathf.Clamp(_cameraRotation.x - rotate.y * scaledRotateSpeed, _minClamp, _maxClamp);
        _camera.transform.localEulerAngles = _cameraRotation;

        _playerRotation.y += rotate.x * scaledRotateSpeed;
        transform.localEulerAngles = _playerRotation;
    }
}