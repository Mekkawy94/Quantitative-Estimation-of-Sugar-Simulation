using UnityEngine;
using System.Collections; // Needed for IEnumerator

public class DragAndTiltObject : MonoBehaviour
{
    private Color FlaskLiquidColor;
    // Dragging Variables
    private Vector3 mOffset;
    private float mZCoord;
    private bool isDragging = false;
    private float zMovementSpeed = 3f;

    // Tilting Variables
    public float tiltAngle = 30f;
    public float tiltSpeed = 5f;
    private float currentTiltAngle = 0f;

    public float Ragtime = 0;
    private Color LightBlue = new Color(135f / 255f, 206f / 255f, 235f / 255f);

    private bool RagStep = false;
    // Rotation Reset
    private Quaternion originalRotation; // To store the original rotation
    public float resetSpeed = 2f; // Speed at which the rotation resets

    void Start()
    {
        originalRotation = transform.rotation; // Store the original rotation at start
    }

    void Update()
    {
        Renderer liquidRend = GameObject.Find("Flask").transform.Find("FlaskLiquid").GetComponent<Renderer>();
        if (liquidRend == null) return;
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        liquidRend.GetPropertyBlock(propBlock);

        FlaskLiquidColor = propBlock.GetColor("_SideColor");

        if(Ragtime == 100 && !RagStep)
        {
            propBlock.SetColor("_SideColor", LightBlue);
            propBlock.SetColor("_TopColor", LightBlue);
            liquidRend.SetPropertyBlock(propBlock);
            RagStep = true;
        }


        if (isDragging)
        {
            // Move the object in the Z direction based on vertical input
            float zMovement = Input.GetAxis("Vertical") * zMovementSpeed * Time.deltaTime;
            if (zMovement != 0)
            {
                MoveObjectInZDirection(zMovement);
            }

            // Tilt the object based on horizontal input while dragging
            float tiltDirection = Input.GetAxis("Horizontal"); // This returns -1, 0, or 1
            if (tiltDirection != 0)
            {
                TiltObject((int)Mathf.Sign(tiltDirection));
                if (FlaskLiquidColor == Color.blue)
                {
                    if (Ragtime < 100)
                    {
                        Ragtime++;
                    }
                   
                }
            }
        }
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        isDragging = true; // Begin dragging
        // Reset the tilt angle when we start dragging
        currentTiltAngle = 0f;
        StopAllCoroutines(); // Stop any ongoing rotation reset coroutine
    }

    void OnMouseUp()
    {
        isDragging = false; // End dragging
        StartCoroutine(ResetRotation()); // Start the rotation reset coroutine
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseAsWorldPoint() + mOffset;
        newPosition.z = transform.position.z; // Keep the current Z position constant during drag
        transform.position = newPosition;
    }

    private void MoveObjectInZDirection(float zMovement)
    {
        Vector3 position = transform.position;
        position.z += zMovement;
        transform.position = position;
    }

    void TiltObject(int direction)
    {
        // Calculate and apply tilt
        currentTiltAngle += direction * tiltSpeed * Time.deltaTime;
        currentTiltAngle = Mathf.Clamp(currentTiltAngle, -tiltAngle, tiltAngle);
        transform.rotation = Quaternion.Euler(0f, 0f, currentTiltAngle);
    }

    IEnumerator ResetRotation()
    {
        while (Quaternion.Angle(transform.rotation, originalRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * resetSpeed);
            yield return null;
        }

        transform.rotation = originalRotation; // Ensure the rotation is exactly reset to the original
    }
}
