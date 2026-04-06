// Assets/Scripts/OrderBuilder.cs
using UnityEngine;
using UnityEngine.UI;

public class OrderBuilder : MonoBehaviour
{
    public static OrderBuilder Instance;
    public static float LastUIClickTime = 0f;

    private IceCreamOrder current = new IceCreamOrder();

    // Lock flags
    private bool containerLocked = false;
    private bool flavorLocked    = false;
    private bool toppingLocked   = false;

    [Header("Preview UI")]
    public Image previewContainerIcon;
    public Image previewFlavorIcon;
    public Image previewToppingIcon;

    [Header("Preview Icon Sprites")]
    public Sprite iconCup, iconCone, iconPafe;
    public Sprite iconVanilla, iconChocolate, iconStrawberry;
    public Sprite iconSprinkles, iconCherry, iconWafer;

    [Header("Lock Indicator (optional)")]
    public GameObject containerLockIcon;
    public GameObject flavorLockIcon;
    public GameObject toppingLockIcon;

    void Awake() { Instance = this; }

    // ── Select methods ──────────────────────────────

    public void SelectContainer(string name)
    {
        if (containerLocked)
        {
            Debug.Log("Container locked! Click trashcan to reset.");
            return;
        }
        LastUIClickTime = Time.unscaledTime;
        current.container = (Container)System.Enum.Parse(typeof(Container), name);
        containerLocked = true;
        Debug.Log("Container selected: " + current.container);
        UpdatePreview();
        UpdateLockIcons();
    }

    public void SelectFlavor(string name)
    {
        if (flavorLocked)
        {
            Debug.Log("Flavor locked! Click trashcan to reset.");
            return;
        }
        LastUIClickTime = Time.unscaledTime;
        current.flavor = (Flavor)System.Enum.Parse(typeof(Flavor), name);
        flavorLocked = true;
        Debug.Log("Flavor selected: " + current.flavor);
        UpdatePreview();
        UpdateLockIcons();
    }

    public void SelectTopping(string name)
{
    if (toppingLocked)
    {
        Debug.Log("Topping locked! Click trashcan to reset.");
        return;
    }

    // ต้องเลือก Container และ Flavor ก่อน
    if (current.container == Container.None || current.flavor == Flavor.None)
    {
        Debug.Log("เลือก Container และ Flavor ก่อนนะ!");
        return;
    }

    // ถ้า order เพิ่ง submit ไป (containerLocked และ flavorLocked ต้องเป็น true ก่อน)
    if (!containerLocked || !flavorLocked)
    {
        Debug.Log("เลือก Container และ Flavor ก่อนนะ!");
        return;
    }

    LastUIClickTime = Time.unscaledTime;
    current.topping = (Topping)System.Enum.Parse(typeof(Topping), name);
    toppingLocked = true;
    Debug.Log("Topping selected: " + current.topping);
    UpdatePreview();
    UpdateLockIcons();
}

    // ── Trashcan resets everything ──────────────────
    public void ResetOrder()
    {
        current         = new IceCreamOrder();
        containerLocked = false;
        flavorLocked    = false;
        toppingLocked   = false;
        Debug.Log("Order reset!");
        UpdatePreview();
        UpdateLockIcons();
    }

    // ── Called when clicking sheep ──────────────────
    public IceCreamOrder GetCurrentOrder() => current;

    public IceCreamOrder SubmitOrder()
    {
        IceCreamOrder submitted = current;
        current         = new IceCreamOrder();
        containerLocked = false;
        flavorLocked    = false;
        toppingLocked   = false;
        UpdatePreview();
        UpdateLockIcons();
        return submitted;
    }

    void UpdateLockIcons()
    {
        if (containerLockIcon != null)
            containerLockIcon.SetActive(containerLocked);
        if (flavorLockIcon != null)
            flavorLockIcon.SetActive(flavorLocked);
        if (toppingLockIcon != null)
            toppingLockIcon.SetActive(toppingLocked);
    }

    void UpdatePreview()
{
    Debug.Log("=== UpdatePreview called ===");
    Debug.Log("Current order - Container: " + current.container + 
              " | Flavor: " + current.flavor + 
              " | Topping: " + current.topping);

    if (previewContainerIcon != null)
    {
        previewContainerIcon.sprite = current.container switch
        {
            Container.Cup  => iconCup,
            Container.Cone => iconCone,
            Container.Pafe => iconPafe,
            _              => null
        };
        previewContainerIcon.enabled = previewContainerIcon.sprite != null;
        Debug.Log("Container icon - sprite: " + previewContainerIcon.sprite + 
                  " | enabled: " + previewContainerIcon.enabled);
    }
    else Debug.LogWarning("previewContainerIcon is NULL!");

    if (previewFlavorIcon != null)
    {
        previewFlavorIcon.sprite = current.flavor switch
        {
            Flavor.Vanilla    => iconVanilla,
            Flavor.Chocolate  => iconChocolate,
            Flavor.Strawberry => iconStrawberry,
            _                 => null
        };
        previewFlavorIcon.enabled = previewFlavorIcon.sprite != null;
        Debug.Log("Flavor icon - sprite: " + previewFlavorIcon.sprite + 
                  " | enabled: " + previewFlavorIcon.enabled);
    }
    else Debug.LogWarning("previewFlavorIcon is NULL!");

    if (previewToppingIcon != null)
    {
        if (current.topping == Topping.None)
        {
            previewToppingIcon.sprite  = null;
            previewToppingIcon.enabled = false;
        }
        else
        {
            previewToppingIcon.sprite = current.topping switch
            {
                Topping.Sprinkles => iconSprinkles,
                Topping.Cherry    => iconCherry,
                Topping.Wafer     => iconWafer,
                _                 => null
            };
            previewToppingIcon.enabled = previewToppingIcon.sprite != null;
        }
        Debug.Log("Topping icon - sprite: " + previewToppingIcon.sprite + 
                  " | enabled: " + previewToppingIcon.enabled);
    }
    else Debug.LogWarning("previewToppingIcon is NULL!");
}
}