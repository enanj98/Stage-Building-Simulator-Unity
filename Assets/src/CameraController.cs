using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 6.0f; // Speed at which the camera moves
    public float lookSpeed = 2.0f; 

    void Update()
    {
        Vector3 moveAmount = new Vector3(0,0,0);

    
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveAmount -= transform.right; 
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveAmount += transform.right; 
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveAmount += transform.forward; 
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveAmount -= transform.forward; 
        }

       
        moveAmount = moveAmount.normalized * speed * Time.deltaTime;


        transform.Translate(moveAmount, Space.World);


        if (Input.GetMouseButton(1)) // for case of moving mouse with right click pressed
        {
            float mouseHorizontal = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseVertical = Input.GetAxis("Mouse Y") * lookSpeed;

       
            transform.Rotate(0f, mouseHorizontal, 0f, Space.World);
            transform.Rotate(-mouseVertical, 0f, 0f);
        }
    }
}
