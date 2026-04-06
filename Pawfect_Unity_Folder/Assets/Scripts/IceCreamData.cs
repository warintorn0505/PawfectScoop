// Assets/Scripts/IceCreamData.cs

public enum Flavor    { None, Vanilla, Chocolate, Strawberry }
public enum Topping   { None, Sprinkles, Cherry, Wafer }
public enum Container { None, Cup, Cone, Pafe }

[System.Serializable]
public class IceCreamOrder
{
    public Flavor     flavor    = Flavor.None;
    public Topping    topping   = Topping.None;
    public Container  container = Container.None;

    public bool Matches(IceCreamOrder other)
    {
        return flavor    == other.flavor
            && topping   == other.topping
            && container == other.container;
    }

    public bool IsComplete()
{
    // Topping ไม่จำเป็นต้องเลือก (None ก็ได้)
    return flavor    != Flavor.None
        && container != Container.None;
}
}