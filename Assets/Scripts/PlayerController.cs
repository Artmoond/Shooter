using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _asset;
    private InputAction _move;
    private InputAction _rotate;
    private InputAction _jump;

    private float rotationSpeed = 100f;
    private PlayerCameraController _camera;
    private PlayerMovement _movement;


    private void OnEnable()
    {
        _asset.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        _asset.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        _move = _asset.FindAction("Move");
        _rotate = _asset.FindAction("Look");
        _jump = _asset.FindAction("Jump");
        _camera = GetComponentInChildren<PlayerCameraController>();
        _movement = GetComponentInChildren<PlayerMovement>();
    }
    private void Update()
    {
   
        _camera.CameraRotate(new Vector2(0, _rotate.ReadValue<Vector2>().y), rotationSpeed);
        _movement.PlayerRotate(_rotate.ReadValue<Vector2>().x, rotationSpeed);
        
        
    }
    private void FixedUpdate()
    {
        _movement.PlayerMove(_move.ReadValue<Vector2>());
        if (_jump.IsPressed())
        {
            _movement.Jump();
        }
    }

}
