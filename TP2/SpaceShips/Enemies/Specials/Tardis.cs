using System;
using System.Collections.Generic;
using TP2.SpaceShips.Interfaces;


namespace TP2.SpaceShips.Enemies.Specials
{
    public class Tardis : SpaceShip, IAptitude
    {
        public Tardis(Armory armory) : base(1, 0, armory)
        {
        }
        
        public override void Attack(SpaceShip spaceShip)
        {
            Console.Write("Tardis has no attack");
        }
        
        public void Use(List<SpaceShip> spaceShips)
        {
            var random = new Random();
            int max = spaceShips.Count;
            int insertIndex = random.Next(max);
            int shipIndex = random.Next(max - 1);
            SpaceShip spaceShip = spaceShips[shipIndex];
            spaceShips.RemoveAt(shipIndex);
            spaceShips.Insert(insertIndex, spaceShip);
            Console.WriteLine("The tardis has teleport a ship to another position");
        }
    }
}