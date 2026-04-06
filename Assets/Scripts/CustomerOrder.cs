// Assets/Scripts/CustomerOrder.cs
// This is a wrapper so CustomerBehaviour can have a named order field
// IceCreamOrder already handles logic — this just generates random orders

using UnityEngine;

public class CustomerOrder
{
    public static IceCreamOrder GenerateForLevel(int level)
    {
        Flavor[] flavors = level switch
        {
            1 => new[] { Flavor.Vanilla },
            2 => new[] { Flavor.Vanilla, Flavor.Chocolate },
            _ => new[] { Flavor.Vanilla, Flavor.Chocolate, Flavor.Strawberry }
        };

        Topping[] toppings = level switch
        {
            1 => new[] { Topping.Sprinkles },
            2 => new[] { Topping.Sprinkles, Topping.Cherry },
            _ => new[] { Topping.Sprinkles, Topping.Cherry, Topping.Wafer }
        };

        Container[] containers = new[]
        {
            Container.Cup, Container.Cone, Container.Pafe
        };

        return new IceCreamOrder
        {
            flavor    = flavors[Random.Range(0, flavors.Length)],
            topping   = toppings[Random.Range(0, toppings.Length)],
            container = containers[Random.Range(0, containers.Length)]
        };
    }
}