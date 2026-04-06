// Assets/Scripts/CustomerBehaviour.cs
using UnityEngine;
using UnityEngine.UI;

public class CustomerBehaviour : MonoBehaviour
{
    [Header("Order")]
    public IceCreamOrder order = new IceCreamOrder();

    [Header("Patience")]
    public float maxPatience = 15f;
    private float timer;
    public bool hasLeft = false;

    [Header("Character Renderer")]
    public SpriteRenderer characterRenderer;

    [Header("Character Sprites - Sheep")]
    public Sprite sheepCasual;
    public Sprite sheepCorrect;
    public Sprite sheepIncorrect;
    public Sprite sheepLose;

    [Header("Character Sprites - Bunny")]
    public Sprite bunnyCasual;
    public Sprite bunnyCorrect;
    public Sprite bunnyIncorrect;
    public Sprite bunnyLose;

    [Header("Character Sprites - Tiger")]
    public Sprite tigerCasual;
    public Sprite tigerCorrect;
    public Sprite tigerIncorrect;
    public Sprite tigerLose;

    [Header("Character Sprites - Cat")]
    public Sprite catCasual;
    public Sprite catCorrect;
    public Sprite catIncorrect;
    public Sprite catLose;

    [Header("Character Sprites - Chihuahua")]
    public Sprite chihuahuaCasual;
    public Sprite chihuahuaCorrect;
    public Sprite chihuahuaIncorrect;
    public Sprite chihuahuaLose;

    private Sprite activeCasual;
    private Sprite activeCorrect;
    private Sprite activeIncorrect;
    private Sprite activeLose;

    public enum CharacterType { Sheep, Bunny, Tiger, Cat, Chihuahua }
    public CharacterType characterType;

    private bool isServing = false;
    private static bool isAnyServing = false;

    [Header("Patience Bar")]
    public Slider patienceBar;
    public Image patienceFill;

    [Header("Order Icon Images")]
    public Image containerIcon;
    public Image flavorIcon;
    public Image toppingIcon;

    [Header("Order Icon Sprites")]
    public Sprite iconCone;
    public Sprite iconCup;
    public Sprite iconParfait;
    public Sprite iconVanilla;
    public Sprite iconChocolate;
    public Sprite iconStrawberry;
    public Sprite iconSprinkles;
    public Sprite iconCherry;
    public Sprite iconWafer;

    public void SetCharacterType(CharacterType type)
    {
        characterType = type;
        switch (type)
        {
            case CharacterType.Sheep:
                activeCasual    = sheepCasual;
                activeCorrect   = sheepCorrect;
                activeIncorrect = sheepIncorrect;
                activeLose      = sheepLose;
                break;
            case CharacterType.Bunny:
                activeCasual    = bunnyCasual;
                activeCorrect   = bunnyCorrect;
                activeIncorrect = bunnyIncorrect;
                activeLose      = bunnyLose;
                break;
            case CharacterType.Tiger:
                activeCasual    = tigerCasual;
                activeCorrect   = tigerCorrect;
                activeIncorrect = tigerIncorrect;
                activeLose      = tigerLose;
                break;
            case CharacterType.Cat:
                activeCasual    = catCasual;
                activeCorrect   = catCorrect;
                activeIncorrect = catIncorrect;
                activeLose      = catLose;
                break;
            case CharacterType.Chihuahua:
                activeCasual    = chihuahuaCasual;
                activeCorrect   = chihuahuaCorrect;
                activeIncorrect = chihuahuaIncorrect;
                activeLose      = chihuahuaLose;
                break;
        }
        SetSprite(activeCasual);
    }

    void Start()
    {
        timer = maxPatience;
        if (activeCasual == null)
            SetCharacterType(CharacterType.Sheep);
        RefreshOrderIcons();
    }

    void Update()
    {
        if (hasLeft) return;

        timer -= Time.deltaTime;

        if (patienceBar != null)
            patienceBar.value = timer / maxPatience;

        if (patienceFill != null)
        {
            float ratio = timer / maxPatience;
            patienceFill.color = ratio > 0.5f ? Color.green
                               : ratio > 0.25f ? Color.yellow
                               : Color.red;
        }

        if (!hasLeft)
        {
            float ratio = timer / maxPatience;
            if (ratio > 0.25f)
                SetSprite(activeCasual);
            else
                SetSprite(activeIncorrect);
        }

        if (timer <= 0f)
            LeaveAngry();
    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown called!");

        if (isAnyServing || hasLeft || isServing) return;

        IceCreamOrder built = OrderBuilder.Instance.GetCurrentOrder();
        if (!built.IsComplete())
        {
            Debug.Log("เลือก container + flavor ก่อนนะ!");
            return;
        }

        isAnyServing = true;
        isServing    = true;
        IceCreamOrder served = OrderBuilder.Instance.SubmitOrder();
        TryServe(served);
    }

    public void TryServe(IceCreamOrder served)
    {
        if (hasLeft) return;
        if (order.Matches(served))
            LeaveHappy();
        else
            LeaveAngry();
    }

    void RefreshOrderIcons()
    {
        if (containerIcon != null)
        {
            containerIcon.sprite = order.container switch
            {
                Container.Cone => iconCone,
                Container.Cup  => iconCup,
                Container.Pafe => iconParfait,
                _              => null
            };
            containerIcon.enabled = containerIcon.sprite != null;
        }

        if (flavorIcon != null)
        {
            flavorIcon.sprite = order.flavor switch
            {
                Flavor.Vanilla    => iconVanilla,
                Flavor.Chocolate  => iconChocolate,
                Flavor.Strawberry => iconStrawberry,
                _                 => null
            };
            flavorIcon.enabled = flavorIcon.sprite != null;
        }

        if (toppingIcon != null)
        {
            toppingIcon.sprite = order.topping switch
            {
                Topping.Sprinkles => iconSprinkles,
                Topping.Cherry    => iconCherry,
                Topping.Wafer     => iconWafer,
                _                 => null
            };
            toppingIcon.enabled = toppingIcon.sprite != null;
        }
    }

    void SetSprite(Sprite s)
    {
        if (characterRenderer != null && s != null)
            characterRenderer.sprite = s;
    }

    void LeaveHappy()
    {
        hasLeft      = true;
        isAnyServing = false;
        SetSprite(activeCorrect);
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.AddCoins(20);
        if (CustomerSpawner.Instance != null)
            CustomerSpawner.Instance.OnCustomerLeft(this);
        Destroy(gameObject, 0.6f);
    }

    void LeaveAngry()
    {
        hasLeft      = true;
        isAnyServing = false;
        SetSprite(activeLose);
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.AddCoins(-5);
        if (CustomerSpawner.Instance != null)
            CustomerSpawner.Instance.OnCustomerLeft(this);
        Destroy(gameObject, 0.6f);
    }
}