using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset InputActions;
    private InputAction move;
    private InputAction look;
    private InputAction jump;

    private Vector3 velocity = Vector3.zero;
    private Vector2 moveDir;
    private Vector2 lookDir;
    private Animator anim;
    private CharacterController controller;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 5.0f;
    [SerializeField] private float jumpHeight = 2.0f;

    private Vector3 g;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        g = Physics.gravity;

        move = InputSystem.actions.FindAction("Move");
        look = InputSystem.actions.FindAction("Look");
        jump = InputSystem.actions.FindAction("Jump");

        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        
        
        moveDir = move.ReadValue<Vector2>();
        lookDir = look.ReadValue<Vector2>();
        Debug.Log($"Look Input: {look}");

        if (jump.WasPressedThisFrame() && controller.isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Gravity();
        Rotating();
        Movement();
    }

    private void Movement()
    {
        // Движение в локальном пространстве персонажа (relative to rotation)
        Vector3 moveVelocity = transform.right * moveDir.x + transform.forward * moveDir.y;
        moveVelocity *= walkSpeed;

        // Добавляем гравитацию к итоговому вектору
        Vector3 finalVelocity = new Vector3(moveVelocity.x, velocity.y, moveVelocity.z);

        controller.Move(finalVelocity * Time.deltaTime);
    }

    private void Rotating()
    {
        // Вращаем тело по Y (yaw)
        float yaw = lookDir.x * rotateSpeed * Time.deltaTime;
        transform.Rotate(0, yaw, 0, Space.Self);

        // Вращаем камеру по X (pitch)
        Transform cameraTransform = transform.Find("Main Camera");
        if (cameraTransform != null)
        {
            float pitch = -lookDir.y * rotateSpeed * Time.deltaTime;

            // ✅ Получаем текущий локальный угол X и накапливаем изменение
            float currentPitch = cameraTransform.localEulerAngles.x;

            // Конвертируем из [0, 360] в [-180, 180] для правильной работы
            if (currentPitch > 180f) currentPitch -= 360f;

            // Добавляем новый pitch и ограничиваем
            currentPitch += pitch;
            currentPitch = Mathf.Clamp(currentPitch, -90f, 90f);

            // Применяем обратно
            cameraTransform.localEulerAngles = new Vector3(currentPitch, 0, 0);
        }
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(g.y));
    }

    private void Gravity()
    {
        velocity.y += g.y * Time.deltaTime;

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Маленькое отрицательное значение
        }
    }
}
