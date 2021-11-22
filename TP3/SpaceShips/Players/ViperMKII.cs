using System;


namespace TP3.SpaceShips.Players
{
    public class ViperMKII : SpaceShip
    {
        public Player Player { get; set; }
        public ViperMKII(Armory armory) : base(10, 15, armory)
        {
            Weapon w1 = armory.GetWeapon("Mitrailleuse");
            Weapon w2 = armory.GetWeapon("EMG");
            Weapon w3 = armory.GetWeapon("Missile");
            AddWeapon(w1, 0);
            AddWeapon(w2, 1);
            AddWeapon(w3, 2);
        }
        public override void Attack(SpaceShip spaceShip)
        {
            var rand = new Random();
            int max = rand.Next(Weapons.Length - 1);
            var selectedWeapon = Weapons[max];
            int damage = selectedWeapon.Use();
            Console.WriteLine($"Player {Player.NickName} inflicts {damage} damage");
            spaceShip.Damage(damage);
        }
    }
}