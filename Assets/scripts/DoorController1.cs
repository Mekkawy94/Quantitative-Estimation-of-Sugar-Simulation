using UnityEngine;

public class DoorController1 : MonoBehaviour
{
    public Transform pivotPoint; // Point around which the door should pivot
    public float moveSpeed = 2f; // Speed at which the door moves
    private Vector3 closedPosition; // Initial position of the door when closed
    private bool isOpening = false; // Is the door currently opening?


    public Transform BoxDoorSound;

    void Start()
    {
        closedPosition = transform.position; // Capture the initial position of the door
    }

    void Update()
    {
        // Toggle door open/close state when 'O' key is pressed
        if (Input.GetKeyDown(KeyCode.O))
        {
            isOpening = !isOpening;
            CollectingBoxDoorSoundOn(transform.position);
        }

        // Move the door towards the pivot point if it's opening
        if (isOpening)
        {
            MoveDoor(pivotPoint.position);
        }
        // Move the door back to its closed position if it's not opening
        else
        {
            MoveDoor(closedPosition);
        }
    }

    // Function to move the door towards a target position
    void MoveDoor(Vector3 targetPosition)
    {
        // Calculate the direction towards the target position
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        
        // Calculate the distance to the target position
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // Move the door towards the target position
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // If the door is close enough to the target position, set its position to the target position
        if (distanceToTarget <= moveSpeed * Time.deltaTime)
        {
            transform.position = targetPosition;
        }
    }

    void CollectingBoxDoorSoundOn(UnityEngine.Vector3 pos)
    {
        Transform obj = Instantiate(BoxDoorSound, pos, new UnityEngine.Quaternion());
        obj.gameObject.SetActive(true);
    }
}
