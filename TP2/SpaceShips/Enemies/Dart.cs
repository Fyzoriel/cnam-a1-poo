using System;
using TP2.Enum;


namespace TP2.SpaceShips.Enemies
{
    public class Dart : SpaceShip
    {
        public Dart(Armory armory) : base(10, 3, armory)
        {
            Weapon weapon = armory.GetWeapon("Laser");

            AddWeapon(weapon, 0);
            AddWeapon(weapon, 1);
            AddWeapon(weapon, 2);
            
        }

        public override void Attack(SpaceShip spaceShip)
        {
            int damage = Weapons[0].Use();
            Console.WriteLine($"Dart inflicts {damage} damage");
            spaceShip.Damage(damage);
        }
        
        public new void AddWeapon(Weapon weapon, int position)
        {
            if (weapon.Type == WeaponType.Direct)
            {
                weapon.ReloadTime = 1;
            }
            
            base.AddWeapon(weapon, position);
        }
    }
}