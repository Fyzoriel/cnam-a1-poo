using System;
using TP2.Enum;


namespace TP2
{
    public class Weapon
    {
        public string Name { get; }
        public int MinDamage { get; }
        public int MaxDamage { get; }

        public int ReloadTime { get; set; }
        public int TurnCounter { get; private set; }

        public bool CanFire => ReloadTime == 0;
        
        public WeaponType Type { get; }

        public Weapon(string name, int minDamage, int maxDamage, float reloadTime, WeaponType type)
        {
            int realReloadTime = Type == WeaponType.Explosive ? (int) reloadTime * 2 : (int) reloadTime; 
            
            Name = name;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            ReloadTime = TurnCounter = realReloadTime;
            Type = type;
        }

        /// <summary>
        /// Get the average damage the weapon can inflict
        /// </summary>
        /// <returns>Returns the average damage the weapon can inflict</returns>
        public int GetAverageDamage()
        {
            return (MinDamage + MaxDamage) / 2;
        }

        private int GetDamage()
        {
            var random = new Random();
            return random.Next(MinDamage, MaxDamage);
        }

        private void DecrementReload()
        {
            TurnCounter--;
            if (TurnCounter < 0)
            {
                TurnCounter = 0;
            }
        }

        public int Use()
        {
            DecrementReload();
            
            if (TurnCounter != 0)
            {
                return 0;
            }

            var random = new Random();
            TurnCounter = ReloadTime;
            
            switch (Type)
            {
                case WeaponType.Direct:
                    return random.Next(1, 10) == 1 ? 0 : GetDamage();
                
                case WeaponType.Explosive:
                    return random.Next(1, 4) == 1 ? 0 : GetDamage() * 2;
                
                case WeaponType.Guided:
                    return MinDamage;
                default:
                    return 0;
            }
        }
        
        /// <summary>
        /// Return the name of the weapon and the min, max and average damage
        /// </summary>
        /// <returns>The name of the weapon and the min, max and average damage</returns>
        public override string ToString()
        {
            return $"Name: {Name} | Damage: {MinDamage} - {MaxDamage} : Average {GetAverageDamage()}";
        }

        public override bool Equals(object obj)
        {
            return obj is Weapon weapon && weapon.Name == Name;
        }
    }
}