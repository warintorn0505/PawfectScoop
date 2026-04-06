// Assets/Scripts/CounterUIManager.cs
using UnityEngine;

public class CounterUIManager : MonoBehaviour
{
    [Header("Container Buttons")]
    public GameObject coneButton;
    public GameObject cupButton;
    public GameObject pafeButton;

    [Header("Flavor Buttons")]
    public GameObject vanillaButton;
    public GameObject chocolateButton;
    public GameObject strawberryButton;

    [Header("Topping Buttons")]
    public GameObject noToppingButton; 
    public GameObject sprinklesButton;
    public GameObject cherryButton;
    public GameObject waferButton;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        int level = LevelManager.Instance.currentLevel;

        // Container — แสดงทุก level
        SetActive(coneButton, true);      // Level 1+
        SetActive(cupButton, true);       // Level 1+
        SetActive(pafeButton, level >= 3); // Level 3+

        // Flavor
        SetActive(vanillaButton, true);                    // Level 1+
        SetActive(chocolateButton, level >= 2);            // Level 2+
        SetActive(strawberryButton, level >= 3);           // Level 3+

        // Topping
        SetActive(noToppingButton, true);
        SetActive(sprinklesButton, true);                  // Level 1+
        SetActive(cherryButton, level >= 2);               // Level 2+
        SetActive(waferButton, level >= 3);                // Level 3+
    }

    void SetActive(GameObject obj, bool active)
    {
        if (obj != null)
            obj.SetActive(active);
    }
}