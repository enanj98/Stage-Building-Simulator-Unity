using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectManager : MonoBehaviour
{

    public Text selectedObjectText;
    public Slider xSlider;
    public Slider ySlider;
    public Slider zSlider;
    public Toggle randomColorToggle; // toggle on the UI
    public List<GameObject> sceneObjects = new List<GameObject>();
    public int currentIndex = 0;

    void Start()
    {

        xSlider.minValue = -400;
        xSlider.maxValue = 400;
        ySlider.minValue = -400;
        ySlider.maxValue = 400;
        zSlider.minValue = -400;
        zSlider.maxValue = 400;

        UpdateSceneObjects();

        //UpdateObjectList();
        UpdateSelectedObject();


        xSlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        ySlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        zSlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });


        randomColorToggle.onValueChanged.AddListener(delegate { OnRandomColorToggleChanged(); });
    }

    void Update()
    {
        UpdateSceneObjects();
        UpdateSelectedObject();
    }

    void UpdateSceneObjects()
    {


        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        sceneObjects.Clear();
        foreach (GameObject obj in allObjects)
        {
            if (obj.transform.parent == null && obj.activeInHierarchy)
            {
                if (obj.name.Contains("MyDiscoLight") || obj.name.Contains("LaserProjector"))
                {
                    sceneObjects.Add(obj);
                }
            }
        }
    }

  

    void UpdateSelectedObject()
    {
        if (sceneObjects.Count > 0)
        {
            currentIndex = Mathf.Clamp(currentIndex, 0, sceneObjects.Count - 1);
            GameObject selectedObject = sceneObjects[currentIndex];
            selectedObjectText.text = "Selected Object: " + selectedObject.name + " (X: " + selectedObject.transform.position.x + ", Y: " + selectedObject.transform.position.y + ", Z: " + selectedObject.transform.position.z + ")";


            xSlider.onValueChanged.RemoveAllListeners();
            ySlider.onValueChanged.RemoveAllListeners();
            zSlider.onValueChanged.RemoveAllListeners();


            xSlider.value = selectedObject.transform.position.x;
            ySlider.value = selectedObject.transform.position.y;
            zSlider.value = selectedObject.transform.position.z;

            // Load the toggle state from the selected object's DiscoLightState component
            DiscoLightState currentToggle = selectedObject.GetComponent<DiscoLightState>();
           
            randomColorToggle.isOn = currentToggle.randomColorToggleState;
            

            // Re-add the listeners
            xSlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
            ySlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
            zSlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        }
        else
        {
            selectedObjectText.text = "There is no objects in the scene!";
        }
    }

    public void NextObject()
    {
        if (sceneObjects.Count > 0)
        {
            currentIndex = (currentIndex + 1) % sceneObjects.Count;
            UpdateSelectedObject();
            GameObject selectedObject = sceneObjects[currentIndex];
            Camera.main.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z-10.0f);
            Camera.main.transform.LookAt(selectedObject.transform.position);
        }
    }

    public void PreviousObject()
    {
        if (sceneObjects.Count != 0)
        {
            currentIndex = (currentIndex - 1 + sceneObjects.Count) % sceneObjects.Count;
            UpdateSelectedObject();
            GameObject selectedObject = sceneObjects[currentIndex];
            Camera.main.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z - 10.0f);
            Camera.main.transform.LookAt(selectedObject.transform.position);
        }
    }

    public void OnSliderValueChanged()
    {
        if (sceneObjects.Count != 0)
        {
            GameObject selectedObject = sceneObjects[currentIndex];
            selectedObject.transform.position = new Vector3(xSlider.value, ySlider.value, zSlider.value);
            
        }
    }

    private void OnRandomColorToggleChanged()
    {
        if (sceneObjects.Count != 0)
        {
            GameObject selectedObject = sceneObjects[currentIndex];
            DiscoLightState state = selectedObject.GetComponent<DiscoLightState>();

            state.randomColorToggleState = randomColorToggle.isOn;
            
        }
    }
}
