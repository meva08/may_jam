using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);

    // Camera half-sizes (adjusted automatically based on orthographic size)
    private float camHalfHeight;
    private float camHalfWidth;

    // Map bounds based on center (10, -10) and size 40x40
    private float minX = -10f;  // 10 - 20
    private float maxX = 30f;   // 10 + 20
    private float minY = -30f;  // -10 - 20
    private float maxY = 10f;   // -10 + 20

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;

        // Clamp so camera doesn't go outside map edges
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX + camHalfWidth, maxX - camHalfWidth);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY + camHalfHeight, maxY - camHalfHeight);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}