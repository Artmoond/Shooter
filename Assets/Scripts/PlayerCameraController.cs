using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    private float minPitch = -90f;
    private float maxPitch = 90f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 currentAngles = transform.localEulerAngles;
        verticalRotation = currentAngles.x;
        horizontalRotation = currentAngles.y;
        
    }
    public void CameraRotate(Vector2 dir, float k)
    {
        verticalRotation -= dir.y * k * Time.deltaTime;
        horizontalRotation += dir.x * k * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, minPitch, maxPitch);


        transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }
}