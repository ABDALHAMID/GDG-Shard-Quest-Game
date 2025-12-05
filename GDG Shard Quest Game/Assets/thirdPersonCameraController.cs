using UnityEngine;

public class thirdPersonCameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 0.125f;
    public float rotationSpeed = 5f;

    private float currentAngle = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        currentAngle += horizontal;

        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 rotatedOffset = rotation * offset;

        Vector3 desiredPosition = target.position + rotatedOffset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }

}
