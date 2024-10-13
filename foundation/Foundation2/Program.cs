using System;
using System.Collections.Generic;

class MainProgram
{
    static void Main(string[] args)
    {
        // Crear artículos
        var item1 = new Item("Gaming Laptop", 201, 1299.99m, 1);
        var item2 = new Item("Wireless Mouse", 202, 29.99m, 2);
        var item3 = new Item("Mechanical Keyboard", 203, 89.99m, 1);
        
        // Crear ubicaciones
        var location1 = new Location("789 Pine St", "Metropolis", "NY", "USA");
        var location2 = new Location("321 Oak St", "Vancouver", "BC", "Canada");

        // Crear compradores
        var buyer1 = new Buyer("Alice Johnson", location1);
        var buyer2 = new Buyer("Bob Williams", location2);

        // Crear compras
        var purchase1 = new Purchase(buyer1);
        purchase1.AddItem(item1);
        purchase1.AddItem(item2);

        var purchase2 = new Purchase(buyer2);
        purchase2.AddItem(item3);
        
        // Mostrar resultados
        ShowPurchaseDetails(purchase1);
        ShowPurchaseDetails(purchase2);
    }

    static void ShowPurchaseDetails(Purchase purchase)
    {
        Console.WriteLine(purchase.GenerateShippingLabel());
        Console.WriteLine(purchase.GeneratePackingLabel());
        Console.WriteLine($"Final Amount: {purchase.CalculateFinalAmount():C}");
        Console.WriteLine();
    }
}

public class Item
{
    private string itemName;
    private int itemId;
    private decimal itemPrice;
    private int itemQuantity;

    public Item(string itemName, int itemId, decimal itemPrice, int itemQuantity)
    {
        this.itemName = itemName;
        this.itemId = itemId;
        this.itemPrice = itemPrice;
        this.itemQuantity = itemQuantity;
    }

    public decimal CalculateTotal()
    {
        return itemPrice * itemQuantity;
    }

    public string GetItemName() => itemName;
    public int GetItemId() => itemId;
}

public class Buyer
{
    private string buyerName;
    private Location buyerLocation;

    public Buyer(string buyerName, Location buyerLocation)
    {
        this.buyerName = buyerName;
        this.buyerLocation = buyerLocation;
    }

    public bool LivesInUSA() => buyerLocation.IsInUSA();

    public string GetBuyerName() => buyerName;
    public Location GetBuyerLocation() => buyerLocation;
}

public class Location
{
    private string streetAddress;
    private string cityName;
    private string stateOrProvince;
    private string countryName;

    public Location(string streetAddress, string cityName, string stateOrProvince, string countryName)
    {
        this.streetAddress = streetAddress;
        this.cityName = cityName;
        this.stateOrProvince = stateOrProvince;
        this.countryName = countryName;
    }

    public bool IsInUSA() => countryName.Equals("USA", StringComparison.OrdinalIgnoreCase);

    public override string ToString()
    {
        return $"{streetAddress}\n{cityName}, {stateOrProvince}\n{countryName}";
    }
}

public class Purchase
{
    private List<Item> itemList = new List<Item>();
    private Buyer buyer;
    private const decimal domesticCost = 5.00m;
    private const decimal internationalCost = 35.00m;

    public Purchase(Buyer buyer)
    {
        this.buyer = buyer;
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public decimal CalculateFinalAmount()
    {
        decimal total = 0;
        foreach (var item in itemList)
        {
            total += item.CalculateTotal();
        }
        total += buyer.LivesInUSA() ? domesticCost : internationalCost;
        return total;
    }

    public string GeneratePackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (var item in itemList)
        {
            label += $"{item.GetItemName()} (ID: {item.GetItemId()})\n";
        }
        return label;
    }

    public string GenerateShippingLabel()
    {
        return $"Shipping Label:\n{buyer.GetBuyerName()}\n{buyer.GetBuyerLocation()}";
    }
}
