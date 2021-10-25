using TP1.Enum;

namespace TP1
{
    public class Weapon
    {
        public string Name { get; }
        public int MinDamage { get; }
        public int MaxDamage { get; }
        
        public WeaponType Type { get; }

        public Weapon(string name, int minDamage, int maxDamage, WeaponType type)
        {
            Name = name;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
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
        
        /// <summary>
        /// Return the name of the weapon and the min, max and average damage
        /// </summary>
        /// <returns>The name of the weapon and the min, max and average damage</returns>
        public override string ToString()
        {
            return $"Name: {Name} | Damage: {MinDamage} - {MaxDamage} : Average {GetAverageDamage()}";
        }
    }
}