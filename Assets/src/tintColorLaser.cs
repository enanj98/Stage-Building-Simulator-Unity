using UnityEngine;

public class ChangeTintColor : MonoBehaviour
{
    private Renderer objectRenderer;
    private Material objectMaterial;
    private FlexibleColorPicker fcp;
    private GameObjectManager gameObjectManager;
    private DiscoLightState discoLightState;
    private bool colorSetByFCP = false;
    private bool objectJustSelected = true;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectMaterial = objectRenderer.material;

        fcp = FindObjectOfType<FlexibleColorPicker>();
        if (fcp == null)
        {
            return;
        }

        gameObjectManager = FindObjectOfType<GameObjectManager>();
        if (gameObjectManager == null)
        {
            return;
        }

        discoLightState = transform.root.GetComponent<DiscoLightState>();
        if (discoLightState == null)
        {
            return;
        }


        if (objectMaterial.HasProperty("_Color"))
        {
            objectMaterial.SetColor("_Color", Color.white);
        }


        if (objectMaterial.HasProperty("_TintColor"))
        {
            objectMaterial.SetColor("_TintColor", discoLightState.lastSelectedColor);
        }
        else
        {
            Debug.LogWarning("The material does not have a _TintColor property.");
        }
    }

    void Update()
    {
        if (objectMaterial != null && fcp != null && gameObjectManager != null)
        {
            fcp.gameObject.SetActive(!gameObjectManager.randomColorToggle.isOn);

            GameObject selectedObject = gameObjectManager.sceneObjects[gameObjectManager.currentIndex];

            if (selectedObject == transform.root.gameObject)
            {
                if (objectJustSelected)
                {
                    fcp.color = discoLightState.lastSelectedColor;
                    objectJustSelected = false;
                }

                if (!gameObjectManager.randomColorToggle.isOn)
                {
                    if (objectMaterial.HasProperty("_TintColor"))
                    {
                        objectMaterial.SetColor("_TintColor", fcp.color);
                        colorSetByFCP = true;
                        discoLightState.lastSelectedColor = fcp.color;
                    }
                }
            }
            else
            {
                objectJustSelected = true;
            }
        }
    }
}
