using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TP3
{
    public class Armory
    {
        private List<Weapon> Weapons { get; }

        public Armory(List<Weapon> weapons)
        {
            Weapons = weapons;
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
                return Weapons[index].Clone();
            }
            return null;
        }

        public Weapon GetWeapon(string name)
        {
            int index = Weapons.FindIndex(weapon => weapon.Name == name);
            return GetWeapon(index);
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

        public List<Weapon> GetWeaponsBestAverageDamage()
        {
            return Weapons.OrderByDescending(w => w.GetAverageDamage()).Take(5).ToList();
        }
        
        public List<Weapon> GetWeaponsBestMinimalDamage()
        {
            return Weapons.OrderByDescending(w => w.MinDamage).Take(5).ToList();
        }
    }
}