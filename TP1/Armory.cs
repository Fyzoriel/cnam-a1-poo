using System.Collections.Generic;
using System.Text;
using TP1.Enum;

namespace TP1
{
    public class Armory
    {
        private List<Weapon> Weapons { get; } = new();

        public Armory()
        {
            Init();
        }
        
        /// <summary>
        /// Init 3 weapons for the Armory
        /// </summary>
        private void Init()
        {
            Weapons.Add(new Weapon("Machine gun", 10, 15, WeaponType.DIRECT));
            Weapons.Add(new Weapon("Missile", 7, 12, WeaponType.EXPLOSIVE));
            Weapons.Add(new Weapon("Auto-guided Missile", 5, 8, WeaponType.GUIDED));
        }

        /// <summary>
        /// Check if the Armory contains the specified weapon
        /// </summary>
        /// <param name="weapon">The weapon</param>
        /// <returns>If the armory contains the weapon</returns>
        public bool ContainWeapon(Weapon weapon)
        {
            return Weapons.Contains(weapon);
        }

        /// <summary>
        /// Get the weapon at the specific index
        /// </summary>
        /// <param name="index">The index of the weapon</param>
        /// <returns>The weapon or null if index is invalid</returns>
        public Weapon GetWeapon(int index)
        {
            if (index >= 0 && index < Weapons.Count)
            {
                return Weapons[index];
            }
            return null;
        }
        
        /// <summary>
        /// Return the list of weapons and their information
        /// </summary>
        /// <returns>The list of weapons and their information</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Weapon weapon in Weapons)
            {
                stringBuilder.Append(weapon).AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}