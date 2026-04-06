// Assets/Scripts/CustomerSpawner.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner Instance;

    [Header("Prefab")]
    public GameObject customerPrefab;

    [Header("Slots")]
    public Transform slot1;
    public Transform slot2;
    public Transform slot3;

    private Transform[] slots;
    private bool[] slotOccupied;

    [Header("Timing")]
    public float[] spawnIntervals = { 5f, 4f, 3f, 3f };

    private Dictionary<CustomerBehaviour, int> customerSlotMap
        = new Dictionary<CustomerBehaviour, int>();

    void Awake()
    {
        Instance     = this;
        slots        = new Transform[] { slot1, slot2, slot3 };
        slotOccupied = new bool[]      { false, false, false };
    }

    void Start()
    {
        Debug.Log("CustomerSpawner started!");
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
{
    SpawnCustomer();
    while (true)
    {
        int level = LevelManager.Instance.currentLevel;
        int index = Mathf.Min(level - 1, spawnIntervals.Length - 1);
        float interval = spawnIntervals[index];
        yield return new WaitForSeconds(interval);
        SpawnCustomer();
    }
}

    void SpawnCustomer()
{
    Debug.Log("Trying to spawn customer...");
    int level      = LevelManager.Instance.currentLevel;
    int maxAllowed = level <= 2 ? 2 : 3;

    if (customerSlotMap.Count >= maxAllowed)
    {
        Debug.Log("Max customers reached: " + customerSlotMap.Count);
        return;
    }

    for (int i = 0; i < slots.Length; i++)
    {
        if (!slotOccupied[i])
        {
            slotOccupied[i] = true;

            Vector3 spawnPos = new Vector3(
                slots[i].position.x,
                slots[i].position.y,
                0f);

            GameObject obj = Instantiate(customerPrefab,
                                         spawnPos,
                                         Quaternion.identity);

            CustomerBehaviour cb = obj.GetComponent<CustomerBehaviour>();
            cb.order = GenerateOrder(level);
            cb.SetCharacterType(GetRandomCharacter(level));

            // Chihuahua มี patience น้อยกว่าปกติเสมอ
            if (cb.characterType == CustomerBehaviour.CharacterType.Chihuahua)
                cb.maxPatience = 5f;
            // Level 3+ มี fast customer แบบสุ่ม 30%
            else if (level >= 3 && Random.value < 0.3f)
                cb.maxPatience = 8f;
            // ปกติใช้ค่า default จาก prefab
            
            customerSlotMap[cb] = i;
            Debug.Log("Customer spawned in slot " + i + 
                      " | Character: " + cb.characterType + 
                      " | Patience: " + cb.maxPatience);
            return;
        }
    }
}

    CustomerBehaviour.CharacterType GetRandomCharacter(int level)
{
    switch (level)
    {
        case 1:
            // Level 1 = Sheep เท่านั้น
            return CustomerBehaviour.CharacterType.Sheep;

        case 2:
            // Level 2 = Sheep, Bunny
            CustomerBehaviour.CharacterType[] level2 = {
                CustomerBehaviour.CharacterType.Sheep,
                CustomerBehaviour.CharacterType.Bunny
            };
            return level2[Random.Range(0, level2.Length)];

        case 3:
            // Level 3 = Bunny, Tiger, Cat
            CustomerBehaviour.CharacterType[] level3 = {
                CustomerBehaviour.CharacterType.Bunny,
                CustomerBehaviour.CharacterType.Tiger,
                CustomerBehaviour.CharacterType.Cat
            };
            return level3[Random.Range(0, level3.Length)];

        default:
            // Level 4 = Tiger, Cat, Chihuahua
            CustomerBehaviour.CharacterType[] level4 = {
                CustomerBehaviour.CharacterType.Tiger,
                CustomerBehaviour.CharacterType.Cat,
                CustomerBehaviour.CharacterType.Chihuahua
            };
            return level4[Random.Range(0, level4.Length)];
    }
}

    IceCreamOrder GenerateOrder(int level)
    {
        Flavor[] flavors = level switch
        {
            1 => new[] { Flavor.Vanilla },
            2 => new[] { Flavor.Vanilla, Flavor.Chocolate },
            _ => new[] { Flavor.Vanilla, Flavor.Chocolate, Flavor.Strawberry }
        };

        Topping[] toppings = level switch
{
    1 => new[] { Topping.None, Topping.Sprinkles },
    2 => new[] { Topping.None, Topping.Sprinkles, Topping.Cherry },
    _ => new[] { Topping.None, Topping.Sprinkles, Topping.Cherry, Topping.Wafer }
};

        Container[] containers = level switch
        {
            1 => new[] { Container.Cup, Container.Cone },
            2 => new[] { Container.Cup, Container.Cone },
            _ => new[] { Container.Cup, Container.Cone, Container.Pafe }
        };

        return new IceCreamOrder
        {
            flavor    = flavors   [Random.Range(0, flavors.Length)],
            topping   = toppings  [Random.Range(0, toppings.Length)],
            container = containers[Random.Range(0, containers.Length)]
        };
    }

    public void OnCustomerLeft(CustomerBehaviour cb)
    {
        if (customerSlotMap.ContainsKey(cb))
        {
            int slotIndex = customerSlotMap[cb];
            slotOccupied[slotIndex] = false;
            customerSlotMap.Remove(cb);
            Debug.Log("Slot " + slotIndex + " is now free!");
        }
    }
}