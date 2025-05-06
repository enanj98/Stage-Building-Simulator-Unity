using UnityEngine;

public class SyncLightWithMaterial : MonoBehaviour
{
    public Material lightBeamMaterial;
    public Light spotlight; // This can be null in the inspector
    public float fadeDuration = 2f; // Duration of the fade in seconds
    public GameObjectManager gameObjectManager; // Reference to the GameObjectManager

    private Color targetColor;
    private Color startColor;
    private float timer;
    private FlexibleColorPicker fcp;
    private DiscoLightState discoLightState;
    private bool colorSetByFCP = false; // Flag to track if color was set by FCP
    private bool objectJustSelected = true; // Flag to check if the object has just been selected

    void Start()
    {
        if (lightBeamMaterial == null)
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                lightBeamMaterial = renderer.material;
            }
            else
            {
                Debug.LogError("No material found on the object!");
                return;
            }
        }

        if (spotlight == null)
        {
            spotlight = transform.parent.Find("Spotlight")?.GetComponent<Light>();
            if (spotlight == null)
            {
                Debug.LogError("No Light component found in sibling objects!");
                return;
            }
        }

        fcp = FindObjectOfType<FlexibleColorPicker>();
        if (fcp == null)
        {
            Debug.LogError("No FlexibleColorPicker found in the scene!");
            return;
        }

        // Get the DiscoLightState component attached to the root of this MyDiscoLight
        discoLightState = transform.root.GetComponent<DiscoLightState>();
        if (discoLightState == null)
        {
            Debug.LogError("No DiscoLightState component found on the root MyDiscoLight object!");
            return;
        }

        gameObjectManager = FindObjectOfType<GameObjectManager>();
        if (gameObjectManager == null)
        {
            Debug.LogError("No GameObjectManager found in the scene!");
            return;
        }

        targetColor = GetRandomColor();
        startColor = lightBeamMaterial.GetColor("_TintColor");
    }

    void Update()
    {
        if (lightBeamMaterial != null && spotlight != null && fcp != null && gameObjectManager != null)
        {
            // Show/hide the FlexibleColorPicker based on the toggle state from the GameObjectManager
            fcp.gameObject.SetActive(!gameObjectManager.randomColorToggle.isOn);

            GameObject selectedObject = gameObjectManager.sceneObjects[gameObjectManager.currentIndex];

            // Check if this object is the currently selected one
            if (selectedObject == transform.root.gameObject)
            {
                if (objectJustSelected)
                {
                    // On first selection, set FCP's color to the last selected color
                    fcp.color = discoLightState.lastSelectedColor;
                    objectJustSelected = false; // Mark that the object has been initialized
                }

                if (discoLightState.randomColorToggleState)
                {
                    // Use random color if the toggle is on
                    timer += Time.deltaTime / fadeDuration;
                    Color newColor = Color.Lerp(startColor, targetColor, timer);
                    lightBeamMaterial.SetColor("_TintColor", newColor);
                    spotlight.color = newColor;

                    if (timer >= 1f)
                    {
                        startColor = targetColor;
                        targetColor = GetRandomColor();
                        timer = 0f;
                    }

                    // Save the last selected color
                    discoLightState.lastSelectedColor = lightBeamMaterial.GetColor("_TintColor");
                }
                else
                {
                    // Use the FlexibleColorPicker color if the toggle is off
                    lightBeamMaterial.SetColor("_TintColor", fcp.color);
                    spotlight.color = fcp.color;
                    colorSetByFCP = true; // Mark that the color was set by FCP

                    // Save the last selected color
                    discoLightState.lastSelectedColor = fcp.color;
                }
            }
            else
            {
                // Object is not selected, reset the just selected flag
                objectJustSelected = true;
            }
        }
    }

    Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value, 1f);
    }
}
