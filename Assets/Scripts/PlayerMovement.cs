using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private float _movementSpeed = 5f;
    private float _jumpHeight = 2f;
    private Vector3 _g = Physics.gravity;
    private Vector3 _stdGravity = new Vector3(0, -2f, 0);
    private Vector3 _verticalVelocity;
    
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    public void PlayerRotate(float value, float k)
    {
        transform.Rotate(new Vector3(0,value) * k * Time.deltaTime);
    }
    public float PlayerMove(Vector2 dir)
    {
        Vector3 moveDir = transform.right * dir.x + transform.forward * dir.y;
        Vector3 horizontalMove = moveDir * _movementSpeed * Time.deltaTime;
        Vector3 verticalMove = Gravity() * Time.deltaTime;
        
        
        _controller.Move(horizontalMove + verticalMove);

        // делаем отдельный вектор, так как для анимации нужна относительно максимальной скорости скорость горизонтальная, но HorizontalMove
        // не подойдёт так как там уже помножено на deltaTime, а просто velocity мы не можем взять из за скорости вертикальной
        Vector3 horizontalVelocity = new Vector3(_controller.velocity.x, 0f, _controller.velocity.z);
        if (horizontalVelocity.magnitude > 0.01f && _controller.isGrounded)
        {
            return horizontalVelocity.magnitude / _movementSpeed;
        }

        return 0f;

    }
    
    private Vector3 Gravity()
    {
        if (_controller.isGrounded)
        {
            // проверь универсально ли условие
            if(_verticalVelocity.y < 0.0f)
            {
                _verticalVelocity = _stdGravity;
            }
        }
        _verticalVelocity += _g * Time.deltaTime;
        return _verticalVelocity;
    }
    public void Jump()
    {
        if (_controller.isGrounded)
        {
            _verticalVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _g.y);
        }
    }
}
