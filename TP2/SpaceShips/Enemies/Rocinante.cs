using System;


namespace TP2.SpaceShips.Enemies
{
    public class Rocinante : SpaceShip
    {
        public Rocinante(Armory armory) : base(3, 5, armory)
        {
            Weapon weapon = armory.GetWeapon("Torpille");
            AddWeapon(weapon, 0);
            AddWeapon(weapon, 1);
            AddWeapon(weapon, 2);
        }
        public override void Attack(SpaceShip spaceShip)
        {
            int damage = Weapons[0].Use();
            Console.WriteLine($"Rocinante inflicts {damage} damage");
            spaceShip.Damage(damage);
        }
    }
}