# Prepare Bill – C# Implementation Using Enum and Dictionary

## Problem Description

The objective of this program is to implement a **C# application** that calculates the **total bill amount** for a list of commodities.  
Each commodity belongs to a specific category, and each category has a predefined tax rate.

The total bill amount should include:
- Base price of the commodity
- Applicable tax based on its category

The program must strictly follow the given specifications.  
Unless explicitly mentioned, **default visibility** should be assumed for classes, data fields, and methods.

---

## Specifications

### Enum Definition

**CommodityCategory**
- Furniture  
- Grocery  
- Service  

---

### Class: `Commodity`

Represents an individual commodity item.

#### Properties
- `Category` : `CommodityCategory`
- `CommodityName` : `string`
- `CommodityQuantity` : `int`
- `CommodityPrice` : `double`

#### Constructor
Initializes all the commodity details.

---

### Class: `PrepareBill`

Responsible for managing tax rates and calculating the final bill amount.

#### Member Variable
- `_taxRates` : `IDictionary<CommodityCategory, double>`  
  Stores tax rates for each commodity category.

#### Methods

1. **SetTaxRates**
   - Accepts a commodity category and tax rate
   - Adds the tax rate if the category is not already present

2. **CalculateBillAmount**
   - Accepts a list of commodities
   - Calculates total bill including tax
   - Throws `ArgumentException` if tax rate for a category is missing

---

## Sample Input

```csharp
var commodities = new List<Commodity>()
{
    new Commodity(CommodityCategory.Furniture, "Bed", 2, 50000),
    new Commodity(CommodityCategory.Grocery, "Flour", 5, 80),
    new Commodity(CommodityCategory.services, "Insurance", 8, 8500)
};

var prepareBill = new PrepareBill();

prepareBill.SetTaxRates(CommodityCategory.Furniture, 18);
prepareBill.SetTaxRates(CommodityCategory.Grocery, 5);
prepareBill.SetTaxRates(CommodityCategory.services, 12);

var billAmount = prepareBill.CalculateBillAmount(commodities);
Console.WriteLine(billAmount);
```

---

## Sample Output

```
194580
```

---

## Complete C# Implementation

```csharp
namespace PrepareBill
{
    enum CommodityCategory
    {
        Furniture,
        Grocery,
        services
    }

    /// <summary>
    /// Commodity class to represent an item with its details
    /// </summary>
    class Commodity
    {
        public CommodityCategory Category { get; set; }
        public string CommodityName { get; set; }
        public int CommodityQuantity { get; set; }
        public double CommodityPrice { get; set; }

        public Commodity(CommodityCategory category, string commodityName, int commodityQuantity, double commodityPrice)
        {
            Category = category;
            CommodityName = commodityName;
            CommodityQuantity = commodityQuantity;
            CommodityPrice = commodityPrice;
        }
    }

    /// <summary>
    /// PrepareBill class to calculate the total bill amount including taxes
    /// </summary>
    class PrepareBill
    {
        private readonly IDictionary<CommodityCategory, double> _taxRates; // Dictionary to hold tax rates for each category

        // Constructor to initialize the tax rates dictionary
        public PrepareBill()
        {
            _taxRates = new Dictionary<CommodityCategory, double>();
        }

        // Method to set tax rates for a specific commodity category
        public void SetTaxRates(CommodityCategory category, double taxRate)
        {
            if (!_taxRates.ContainsKey(category))
            {
                _taxRates[category] = taxRate;
            }
        }

        // Method to calculate the total bill amount including taxes
        public double CalculateBillAmount(IList<Commodity> items)
        {
            double totalAmount = 0.0;

            foreach (var item in items)
            {
                if (!_taxRates.ContainsKey(item.Category))
                {
                    throw new ArgumentException($"Tax rate for category {item.Category} is not defined.");
                }

                double taxRate = _taxRates[item.Category];
                double itemTotal = item.CommodityPrice * item.CommodityQuantity;
                double taxAmount = itemTotal * taxRate / 100;
                totalAmount += itemTotal + taxAmount;
            }

            return totalAmount;
        }
    }

    // Main program class
    public class Program
    {
        // Entry point of the program
        public static void Main(string[] args)
        {
            var commodities = new List<Commodity>()
            {
                new Commodity(CommodityCategory.Furniture, "Bed", 2, 50000),
                new Commodity(CommodityCategory.Grocery, "Flour", 5, 80),
                new Commodity(CommodityCategory.services, "Insurance", 8, 8500)
            };

            var prepareBill = new PrepareBill();

            prepareBill.SetTaxRates(CommodityCategory.Furniture, 18);
            prepareBill.SetTaxRates(CommodityCategory.Grocery, 5);
            prepareBill.SetTaxRates(CommodityCategory.services, 12);

            var billAmount = prepareBill.CalculateBillAmount(commodities);
            Console.WriteLine(billAmount);
        }
    }
}
```

---

## Notes

* This program uses **Enum** to categorize commodities.
* **Dictionary** is used to efficiently store and retrieve tax rates.
* Exception handling ensures tax rules are strictly enforced.
* Code is suitable for **exam answers, lab records, and GitHub repositories**.