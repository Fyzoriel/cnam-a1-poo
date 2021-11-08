using System;
using System.Text;
using TP2.Exceptions;


namespace TP2.SpaceShips
{
    public abstract class SpaceShip
    {
        private const int MaxWeapons = 3;
        
        protected Weapon[] Weapons { get; } = new Weapon[MaxWeapons];

        private Armory Armory { get; }
        public int MaxStructurePoint { get; }
        public int ActualStructurePoint { get; private set; }
        public int MaxShieldPoint { get; }
        public int ActualShieldPoint { get; private set; }
        public bool IsDestroyed => ActualShieldPoint <= 0 && ActualStructurePoint <= 0;

        public SpaceShip(int maxStructurePoint, int maxShieldPoint, Armory armory)
        {
            MaxStructurePoint = maxStructurePoint;
            MaxShieldPoint = maxShieldPoint;

            ActualStructurePoint = maxStructurePoint;
            ActualShieldPoint = maxShieldPoint;

            Armory = armory;
        }

        /// <summary>
        /// Inflict damage to the shield or the ship
        /// </summary>
        /// <param name="damagePoint">The number of damage the ship takes</param>
        public void Damage(int damagePoint)
        {
            // If ship has a shield then damage the shield first
            if (ActualShieldPoint > 0 && damagePoint > 0)
            {
                var shieldPointBeforeDamage = ActualShieldPoint;
                
                // Remove durability from shield and set to 0 if negative 
                ActualShieldPoint = ActualShieldPoint - damagePoint;
                if (ActualShieldPoint < 0)
                {
                    ActualShieldPoint = 0;
                }
                
                // Calculates the remaining damage 
                damagePoint -= shieldPointBeforeDamage;
                Console.WriteLine($"Shield has taken {shieldPointBeforeDamage - ActualShieldPoint} damage points | {ActualShieldPoint} left");
            }

            // If ship has no shield or there is damage left after the shield has lost durability
            if (damagePoint > 0)
            {
                ActualStructurePoint -= damagePoint;
                Console.WriteLine($"Ship has taken {damagePoint} damage points | {ActualStructurePoint} left");
            }
        }

        public abstract void Attack(SpaceShip spaceShip);
        
        
        /// <summary>
        /// Heal the shield of the ship
        /// </summary>
        /// <param name="heal">The amount of heal</param>
        public void HealShield(int heal)
        {
            if (heal < 0)
            {
                return;
            }
            ActualShieldPoint += heal;
            if (ActualShieldPoint > MaxShieldPoint)
            {
                ActualShieldPoint = MaxShieldPoint;
            }
        }

        /// <summary>
        /// Heal the structure if the ship
        /// </summary>
        /// <param name="heal">The amount of heal</param>
        public void HealStructure(int heal)
        {
            if (heal < 0)
            {
                return;
            }
            ActualStructurePoint += heal;
            if (ActualStructurePoint > MaxStructurePoint)
            {
                ActualStructurePoint = MaxStructurePoint;
            }
        }
        
        /// <summary>
        /// Get if the specified slot is empty or not
        /// </summary>
        /// <param name="position">The position of the slot</param>
        /// <returns>If the specified slot is empty or not</returns>
        /// <exception cref="Exception">Return an exception if the specified slot does not exist</exception>
        private bool IsWeaponSlotEmpty(int position)
        {
            if (position < 0 || position >= MaxWeapons)
            {
                throw new Exception("Invalid weapon position");
            }
            return Weapons[position] == null;
        }

        /// <summary>
        /// Add a new weapon to en empty slot
        /// </summary>
        /// <param name="weapon">The weapon to add</param>
        /// <param name="position">The slot for the weapon</param>
        /// <exception cref="ArmoryException">Throw exception if the weapon is not in Armory</exception>
        public void AddWeapon(Weapon weapon, int position)
        {
            if (!Armory.ContainWeapon(weapon))
            {
                throw new ArmoryException("This weapon is not in Armory");
            }
            
            if (IsWeaponSlotEmpty(position))
            {
                Weapons[position] = weapon;
            }
        }

        /// <summary>
        /// Remove the weapon of the specified slot
        /// </summary>
        /// <param name="position">The position of the weapon to remove</param>
        public void RemoveWeapon(int position)
        {
            if (!IsWeaponSlotEmpty(position))
            {
                Weapons[position] = null;
            }
        }

        /// <summary>
        /// Get the average damage the ship can do
        /// </summary>
        /// <returns>The average damage the ship can do</returns>
        private int GetAverageDamage()
        {
            int average = 0;
            for (int i = 0; i < MaxWeapons; i++)
            {
                Weapon weapon = Weapons[i];
                average += weapon?.GetAverageDamage() ?? 0;
            }
            return average / MaxWeapons;
        }

        /// <summary>
        /// Return the state of the ship, and the list of weapons
        /// </summary>
        /// <returns>The state of the ship, and the list of weapons</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(IsDestroyed ? "The ship is destroyed" : "The ship is not destroyed")
                .AppendLine()
                .Append($"Structure points: {ActualStructurePoint} / {MaxStructurePoint}")
                .AppendLine()
                .Append($"Shield points: {ActualShieldPoint} / {MaxShieldPoint}")
                .AppendLine()
                .Append("Weapons:")
                .AppendLine();
            for (int i = 0; i < MaxWeapons; i++)
            {
                Weapon weapon = Weapons[i];
                string weaponDescription = weapon?.ToString() ?? "Empty";
                stringBuilder.Append($"- Slot #{i + 1}: {weaponDescription}").AppendLine();
            }
            stringBuilder.Append($"Average Damage: {GetAverageDamage()}").AppendLine();
            return stringBuilder.ToString();
        }
    }
}