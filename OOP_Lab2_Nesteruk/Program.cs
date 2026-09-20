using System;
using VirtualItemShop;

class Program
{
    static VirtualItem[] items;
    static int count = 0;
    static int N;

    static void Main()
    {
        Console.WriteLine("=== Virtual Item Shop ===");

        N = ReadN();
        items = new VirtualItem[N];

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Add item");
            Console.WriteLine("2 - View all items");
            Console.WriteLine("3 - Find item");
            Console.WriteLine("4 - Demonstrate behavior");
            Console.WriteLine("5 - Delete item");
            Console.WriteLine("0 - Exit");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddItem(); break;
                case "2": ViewAll(); break;
                case "3": FindItem(); break;
                case "4": DemonstrateBehavior(); break;
                case "5": DeleteItem(); break;
                case "0":
                    Console.WriteLine("Program finished.");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid menu option.");
                    break;
            }
        }
    }

    static int ReadN()
    {
        while (true)
        {
            Console.Write("Enter maximum number of items N (N > 0): ");
            if (int.TryParse(Console.ReadLine(), out int n) && n > 0) return n;
            Console.WriteLine("Error: N must be a positive integer!");
        }
    }

    static void AddItem()
    {
        if (count >= N)
        {
            Console.WriteLine($"Maximum number of items reached ({N}).");
            return;
        }

        VirtualItem item = new VirtualItem();
        bool hasError;

        do
        {
            Console.Write("Item name (3-20 letters only): ");
            string name = Console.ReadLine();
            try
            {
                item.Name = name;
                hasError = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                hasError = true;
            }
        } while (hasError);

        do
        {
            Console.Write("Rarity (0-Common,1-Uncommon,2-Rare,3-Epic,4-Legendary,5-Mythic): ");
            try
            {
                int val = int.Parse(Console.ReadLine());
                item.Rarity = (ItemRarity)val;
                hasError = false;
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: rarity must be a whole number!");
                hasError = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                hasError = true;
            }
        } while (hasError);

        do
        {
            Console.Write($"Item price (0..{int.MaxValue}): ");
            try
            {
                double price = double.Parse(Console.ReadLine());
                item.Price = price;
                hasError = false;
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: price must be a number!");
                hasError = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                hasError = true;
            }
        } while (hasError);

        do
        {
            Console.Write("Item durability (0..100): ");
            try
            {
                int durability = int.Parse(Console.ReadLine());
                item.Durability = durability;
                hasError = false;
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: durability must be a whole number!");
                hasError = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                hasError = true;
            }
        } while (hasError);

        item.IsTradable = ReadValidBool("Is the item tradable? (yes/no): ");
        item.InitializeCreationDate();
        Console.WriteLine("Item added successfully:");

        items[count] = item;
        count++;
        PrintItem(item);
    }

    static bool ReadValidBool(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine()?.Trim().ToLower();
            if (input == "yes") return true;
            if (input == "no") return false;
            Console.WriteLine("Error: enter 'yes' or 'no'!");
        }
    }

    static void ViewAll()
    {
        if (count == 0) { Console.WriteLine("No items found."); return; }

        Console.WriteLine("#   Name           Rarity      Price       Durability  Tradable  Created");
        for (int i = 0; i < count; i++)
            PrintRow(i + 1, items[i]);
    }

    static void PrintRow(int index, VirtualItem item)
    {
        Console.WriteLine($"{index,-4}{item.Name,-15}{item.Rarity,-12}{item.Price,-12}{item.Durability,-12}{(item.IsTradable ? "yes" : "no"),-10}{item.CreatedDate:dd.MM.yyyy HH:mm:ss}");
    }

    static void PrintItem(VirtualItem item)
    {
        Console.WriteLine(item.Summary);
    }

    static int ReadValidIndex(string prompt)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= count) return index;
        Console.WriteLine("Error: invalid number!");
        return -1;
    }

    static void FindItem()
    {
        if (count == 0) { Console.WriteLine("No items found."); return; }

        ViewAll();
        int index = ReadValidIndex("Enter item number: ");
        if (index == -1) return;

        Console.WriteLine("#   Name           Rarity      Price       Durability  Tradable  Created");
        PrintRow(index, items[index - 1]);
    }

    static void DemonstrateBehavior()
    {
        if (count == 0) { Console.WriteLine("No items found."); return; }

        ViewAll();
        int index = ReadValidIndex("Enter item number: ");
        if (index == -1) return;
        VirtualItem item = items[index - 1];

        bool inSubMenu = true;
        while (inSubMenu)
        {
            Console.WriteLine("1 - Use, 2 - Repair, 3 - Show UsesCount, 4 - Enchant, 5 - Show SellPrice, 6 - Show Summary, 0 - Back");
            Console.Write("Your choice: ");
            string sub = Console.ReadLine();
            switch (sub)
            {
                case "1":
                    if (item.Use()) Console.WriteLine($"Item used. Durability: {item.Durability}.");
                    else Console.WriteLine("Error: durability too low to use item (must be at least 10)!");
                    break;
                case "2":
                    Console.Write("Repair amount: ");
                    int.TryParse(Console.ReadLine(), out int amount);
                    if (item.Repair(amount)) Console.WriteLine($"Repaired. Durability: {item.Durability}.");
                    else Console.WriteLine("Error: invalid repair amount, or item already at full durability!");
                    break;
                case "3":
                    Console.WriteLine($"Use count: {item.UsesCount}.");
                    break;
                case "4":
                    if (item.Enchant()) Console.WriteLine($"Item enchanted. New rarity: {item.Rarity}.");
                    else Console.WriteLine("Error: item already has maximum rarity (Mythic)!");
                    break;
                case "5":
                    Console.WriteLine($"Sell price: {item.SellPrice}.");
                    break;
                case "6":
                    Console.WriteLine(item.Summary);
                    break;
                case "0":
                    inSubMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid menu option.");
                    break;
            }
        }
    }

    static void DeleteItem()
    {
        if (count == 0) { Console.WriteLine("No items found."); return; }

        ViewAll();
        int index = ReadValidIndex("Enter item number to delete: ");
        if (index == -1) return;

        for (int i = index - 1; i < count - 1; i++) items[i] = items[i + 1];
        items[count - 1] = null;
        count--;
        Console.WriteLine("Item deleted successfully.");
    }
}