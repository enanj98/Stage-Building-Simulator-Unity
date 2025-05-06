using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject prefabToInstantiate; //my prefab that i assigned from unity
    public float fixedDistanceFromCamera = 10f; // where to spawn object from camera
    
    public static bool isDraggingScript = false;
    
    public void OnDrag(PointerEventData eventData)
    {
        isDraggingScript = true;


    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraggingScript = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraggingScript = false;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 worldPosition = ray.origin + ray.direction * fixedDistanceFromCamera;

        Instantiate(prefabToInstantiate, worldPosition, Quaternion.identity);
         
    }
}