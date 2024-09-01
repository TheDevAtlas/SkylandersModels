using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of the rotation
    public float mouseRotationSpeed = 1000f;
    public float returnSpeed = 50f;    // Speed at which the model returns to default rotation
    public float returnDelay = 5f;     // Time before the model starts returning to default rotation
    private float lastInputTime = 0f;  // Time of the last user input

    public float zoomSpeed = 10f;      // Speed of zooming
    public float minZoom = 5f;         // Minimum zoom distance
    public float maxZoom = 50f;        // Maximum zoom distance

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        bool inputReceived = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Check if the touch is moving (dragging)
            if (touch.phase == TouchPhase.Moved)
            {
                inputReceived = true;
                float touchDeltaX = touch.deltaPosition.x;
                transform.Rotate(Vector3.up, -touchDeltaX * rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            // Mouse input handling
            if (Input.GetMouseButton(0))
            {
                if (Input.GetAxis("Mouse X") != 0)
                {
                    inputReceived = true;
                    float mouseX = Input.GetAxis("Mouse X");
                    transform.Rotate(Vector3.up, -mouseX * mouseRotationSpeed * Time.deltaTime);
                }
            }
        }

        // Zoom handling
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        inputReceived = true;
        float zoomAmount = scrollInput * zoomSpeed;
        float newDistance = Mathf.Clamp(mainCamera.transform.localPosition.z + zoomAmount, -maxZoom, -minZoom);
        mainCamera.transform.localPosition = new Vector3(0f, 0f, newDistance);

        if (inputReceived)
        {
            lastInputTime = Time.time;
        }
        else
        {
            if (Time.time - lastInputTime > returnDelay)
            {
                RotateBackToDefault();
            }
        }
    }

    private void RotateBackToDefault()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, returnSpeed * Time.deltaTime);
    }
}
