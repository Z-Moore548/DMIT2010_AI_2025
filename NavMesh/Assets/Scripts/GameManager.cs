using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject selectedObject;

    [SerializeField] InputAction move;

    Vector2 moveValue;

    float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Get the mouse position in screen space
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            // Convert the mouse position to a ray from the camera
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            // Store the hit information
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "AI")
                {
                    selectedObject = hit.transform.parent.gameObject;
                }
                else if (hit.transform.gameObject.tag == "Floor")
                {
                    if (selectedObject != null)
                    {
                        selectedObject.GetComponent<AIMover>().SetTarget(hit.point);
                    }                    
                }
            }
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            // Get the mouse position in screen space
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            // Convert the mouse position to a ray from the camera
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            // Store the hit information
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "AI")
                {
                    if (selectedObject != null && selectedObject != hit.transform.parent.gameObject)
                    {
                        selectedObject.GetComponent<AIMover>().SetTarget(hit.transform.parent.gameObject);
                    }
                }
                else
                {
                    selectedObject.GetComponent<AIMover>().SetTarget(null);
                }
            }
        }
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            moveSpeed = 6.0f;
        }
        else
        {
            moveSpeed = 3.0f;
        }

        moveValue = move.ReadValue<Vector2>();

        transform.Translate(new Vector3(moveValue.x, 0, moveValue.y) * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnEnable()
    {
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }
}
