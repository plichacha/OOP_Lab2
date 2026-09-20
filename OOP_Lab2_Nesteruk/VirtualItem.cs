using System;

namespace VirtualItemShop
{
    public class VirtualItem
    {
        private string name;
        private ItemRarity rarity = ItemRarity.COMMON;
        private double price;
        private int durability;

        public bool IsTradable { get; set; } = false;
        public DateTime CreatedDate { get; private set; }
        public int UsesCount { get; private set; }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Error: item name can't be null or empty!");
                if (value.Length < 3 || value.Length > 20)
                    throw new ArgumentException("Error: item name length must be between 3 and 20 characters!");
                foreach (char c in value)
                    if (!char.IsLetter(c))
                        throw new ArgumentException("Error: item name can only contain letters!");
                name = value;
            }
        }

        public ItemRarity Rarity
        {
            get => rarity;
            set
            {
                if (!Enum.IsDefined(typeof(ItemRarity), value))
                    throw new ArgumentException("Error: item rarity value isn't correct!");
                rarity = value;
            }
        }

        public double Price
        {
            get => price;
            set
            {
                if (value < 0 || value > int.MaxValue)
                    throw new ArgumentOutOfRangeException(nameof(value), $"Error: price must be in range [0..{int.MaxValue}]!");
                price = value;
            }
        }

        public int Durability
        {
            get => durability;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(value), "Error: durability must be in range [0..100]!");
                durability = value;
            }
        }

        public string Summary => $"{Name} ({Rarity}) - {Price:F2} gold, durability {Durability}%, tradable: {(IsTradable ? "yes" : "no")}";

        public double SellPrice => ComputeSellPrice();

        public void InitializeCreationDate()
        {
            CreatedDate = DateTime.Now;
        }

        public bool Use()
        {
            if (!CanUse()) return false;
            ApplyUsage();
            return true;
        }

        public bool Repair(int amount)
        {
            if (!CanRepair(amount)) return false;
            ApplyRepair(amount);
            return true;
        }

        public bool Enchant()
        {
            if (IsMaxRarity()) return false;
            rarity++;
            return true;
        }

        private bool CanUse()
        {
            return durability >= 10;
        }

        private void ApplyUsage()
        {
            durability -= 10;
            UsesCount++;
        }

        private bool CanRepair(int amount)
        {
            return durability < 100 && amount > 0;
        }

        private void ApplyRepair(int amount)
        {
            durability += amount;
            if (durability > 100) durability = 100;
        }

        private bool IsMaxRarity()
        {
            return rarity == ItemRarity.MYTHIC;
        }

        private double ComputeSellPrice()
        {
            double rarityMultiplier = 1 + (int)rarity * 0.5;
            double durabilityFactor = durability / 100.0;
            return Math.Round(price * rarityMultiplier * durabilityFactor, 2);
        }
    }
}