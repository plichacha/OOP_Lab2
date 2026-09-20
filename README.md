# Virtual Item Shop — OOP Lab-2

A C# console application developed as part of a practical assignment in **Object-Oriented Programming**.

The project demonstrates the principles of **encapsulation**, **properties**, **access modifiers**, **exception handling**, and **private helper methods**.

## Project Overview

The application simulates a shop for virtual RPG items such as weapons, artifacts, armor, and other game items.

Each item is represented by the `VirtualItem` class and has a rarity represented by the `ItemRarity` enumeration.

The program allows the user to:

* add virtual items;
* view all stored items;
* find an item;
* demonstrate item behavior;
* delete an item;
* exit the application.

## Technologies

* C#
* .NET
* Object-Oriented Programming
* Console Application

## Project Structure

```text
VirtualItemShop/
│
├── Program.cs
├── VirtualItem.cs
├── ItemRarity.cs
└── README.md
```

## How to Run

### Using Visual Studio

1. Clone the repository:

```text
git clone https://github.com/plichacha/OOP_Lab1.git
```

2. Open the solution file in Visual Studio.
3. Build the solution.
4. Run the project.

### Using .NET CLI

Navigate to the project directory and run:

```text
dotnet restore
dotnet build
dotnet run
```

## Main Class

### `VirtualItem`

The `VirtualItem` class contains the following characteristics:

| Property      | Type         | Description                              |
| ------------- | ------------ | ----------------------------------------- |
| `Name`        | `string`     | Item name                                |
| `Rarity`      | `ItemRarity` | Item rarity                              |
| `Price`       | `double`     | Base item price                          |
| `Durability`  | `int`        | Item durability from 0 to 100            |
| `IsTradable`  | `bool`       | Indicates whether the item can be traded |
| `CreatedDate` | `DateTime`   | Item creation date                       |
| `UsesCount`   | `int`        | Number of times the item was used        |
| `Summary`     | `string`     | Computed description of the item         |
| `SellPrice`   | `double`     | Computed selling price                   |

## Item Rarity

The `ItemRarity` enumeration contains six levels:

```text
COMMON
UNCOMMON
RARE
EPIC
LEGENDARY
MYTHIC
```

## Encapsulation

All internal fields of `VirtualItem` are declared as `private`.

Public properties are used to control access to the object's data.

For example:

```csharp
public double Price
{
    get => price;
    set
    {
        if (value < 0 || value > int.MaxValue)
            throw new ArgumentOutOfRangeException(
                nameof(value),
                "Price must be within the allowed range.");

        price = value;
    }
}
```

This prevents invalid values from being stored inside the object.

## Properties

The project demonstrates several types of properties.

### Property with validation

`Name`, `Rarity`, `Price`, and `Durability` validate values before changing the object's state.

### Auto-property

```csharp
public bool IsTradable { get; set; } = false;
```

The property has a default value of `false`.

### Property with different access levels

```csharp
public DateTime CreatedDate { get; private set; }
```

The creation date can be read from outside the class but can only be changed inside the class.

`UsesCount` also uses a public getter and a private setter, for the same reason: the usage counter must only change as a result of calling `Use()`, not be set directly from outside the class.

### Computed properties

```csharp
public string Summary => ...;

public double SellPrice => ComputeSellPrice();
```

These properties calculate their values using other properties and methods instead of storing separate values.

## Encapsulation Through Private Methods

The class contains private helper methods such as:

* `CanUse()`
* `ApplyUsage()`
* `CanRepair()`
* `ApplyRepair()`
* `IsMaxRarity()`
* `ComputeSellPrice()`

Public methods use these private methods to hide implementation details.

For example:

```csharp
public bool Use()
{
    if (!CanUse())
        return false;

    ApplyUsage();
    return true;
}
```

The calling code does not need to know how the possibility of using an item is checked or how its durability is changed.

## Item Behavior

### Use

The `Use()` method decreases item durability by 10 and increases the usage counter.

### Repair

The `Repair(int amount)` method increases durability without allowing it to exceed 100.

### Enchant

The `Enchant()` method increases the item rarity by one level until `MYTHIC` is reached.

### Calculate Sell Price

The `SellPrice` property calculates the item's selling price based on:

* base price;
* item rarity;
* current durability.

## Exception Handling

Invalid property values generate exceptions such as:

* `ArgumentException`
* `ArgumentOutOfRangeException`

The main program handles these exceptions using `try-catch` blocks and displays informative error messages to the user.

## Console Menu

```text
1 - Add item
2 - View all items
3 - Find item
4 - Demonstrate behavior
5 - Delete item
0 - Exit
```

## Testing

The application is tested using both valid and invalid input values.

The testing includes:

* invalid maximum number of items;
* empty and invalid item names;
* invalid rarity values;
* negative and excessive prices;
* durability values outside the range `0..100`;
* invalid boolean input;
* maximum number of stored objects;
* item search;
* item deletion;
* `Use()` behavior;
* `Repair()` behavior;
* `Enchant()` behavior;
* selling price calculation;
* correct program termination.

## Purpose of the Lab

The purpose of this practical assignment is to demonstrate:

1. Encapsulation of class fields.
2. Controlled access through properties.
3. Validation of property values.
4. Automatic properties with default values.
5. Computed properties.
6. Different access levels for `get` and `set`.
7. Private methods for hiding implementation details.
8. Exception handling.
9. Interaction with objects through their public interface.

## Repository

Source code is available on GitHub:

<https://github.com/plichacha/OOP_Lab1>

## Author

**plichacha**

Object-Oriented Programming — Practical Work Lab-2
