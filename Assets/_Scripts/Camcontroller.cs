using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Độ nhạy chuột
    public Transform player; // Tham chiếu đến nhân vật
    public float distanceFromPlayer = 5f; // Khoảng cách từ camera đến nhân vật

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Giữ con trỏ chuột trong màn hình
    }

    void Update()
    {
        // Nhận đầu vào từ chuột
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Xoay camera theo chiều ngang
        transform.Rotate(Vector3.up * mouseX);

        // Xoay camera theo chiều dọc
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Giới hạn góc xoay dọc

        // Cập nhật vị trí camera
        Vector3 direction = new Vector3(0, 0, -distanceFromPlayer);
        Quaternion rotation = Quaternion.Euler(xRotation, transform.eulerAngles.y, 0);
        transform.position = player.position + rotation * direction;
        transform.LookAt(player.position);
    }
}