using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private float rotationSpeed = 1800.0f; // Adjust the speed of rotation
    private bool isRotating = false; // Flag to check if rotation should happen
    private bool isDragging = false;
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (DragDropScript.isDraggingScript) return;

        isDragging = true;
        
        isRotating = (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetMouseButton(0);
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;
        if (DragDropScript.isDraggingScript) return;
        if (isRotating)
        {

            RotateObject();
        }
        else
        {

            TranslateObject();
        }
    }

    private void TranslateObject()
    {

        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;


        transform.position = cursorPosition;
    }

    private void RotateObject()
    {
        if (Input.GetKey(KeyCode.T))
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, -rotationX, Space.World);
            //transform.Rotate(Vector3.right, rotationY, Space.World);
            
        }
        else
        {
            float rotationZ = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotationZ, Space.World);
        }
   

    }
    void Update()
    {
        if (isDragging && Input.GetKey(KeyCode.Delete))
        {
          
            Destroy(gameObject);
        }
    }
    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
        }
    }

}
