using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    private bool isOpening = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        // Assuming this script is attached to the parent GameObject
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + Vector3.up * openAngle);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isOpening = !isOpening;
        }

        if (isOpening)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
        }
    }
}

