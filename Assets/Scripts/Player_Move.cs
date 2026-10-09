using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Параметры движения и прыжка")]
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float jumpHeight = 1.5f;
    

    [Header("Проверка земли")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    
    

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

   

    private void HandleMovement()
    {
        // на земле ли мы
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // ввод
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // перемещение
        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * moveSpeed * Time.deltaTime);
        }

        // прыг
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * -Physics.gravity.magnitude);
        }

        // гравитация
        velocity.y += -Physics.gravity.magnitude * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    
}