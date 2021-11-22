using System;
using TP3.Enum;


namespace TP3.SpaceShips.Enemies
{
    public class BWings : SpaceShip
    {
        public BWings(Armory armory) : base(30, 0, armory)
        {
            Weapon weapon = armory.GetWeapon("Hammer");
            AddWeapon(weapon, 0);
            AddWeapon(weapon, 1);
            AddWeapon(weapon, 2);
        }
        public override void Attack(SpaceShip spaceShip)
        {
            int damage = Weapons[0].Use();
            Console.WriteLine($"B-Wing inflicts {damage} damage");
            spaceShip.Damage(damage);
        }

        public new void AddWeapon(Weapon weapon, int position)
        {
            if (weapon.Type == WeaponType.Explosive)
            {
                weapon.ReloadTime = 1;
            }
            
            base.AddWeapon(weapon, position);
        }
    }
}