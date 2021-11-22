using System;
using System.Collections.Generic;
using TP3.SpaceShips.Interfaces;
using TP3.SpaceShips.Players;


namespace TP3.SpaceShips.Enemies.Specials
{
    public class F18 : SpaceShip, IAptitude
    {
        public F18(Armory armory) : base(15, 0, armory)
        {
        }
        
        public override void Attack(SpaceShip spaceShip)
        {
            Console.WriteLine("F18 has no attack");
        }
        
        public void Use(List<SpaceShip> spaceShips)
        {
            for (int i = 0, max = spaceShips.Count; i < max ; i++)
            {
                if (spaceShips[i] is not F18 f18Ship)
                {
                    continue;
                }
                int before = i - 1; 
                int after = i + 1;

                if (before >= 0 && spaceShips[before] is ViperMKII playerShipBefore)
                {
                    playerShipBefore.Damage(10);
                    f18Ship.Damage(f18Ship.MaxStructurePoint);
                    
                    Console.WriteLine($"F18 exploded on the ship of {playerShipBefore.Player.NickName}");
                    break;
                }
                
                if (after < max && spaceShips[after] is ViperMKII playerShipAfter)
                {
                    playerShipAfter.Damage(10);
                    f18Ship.Damage(f18Ship.MaxStructurePoint);
                    Console.WriteLine($"F18 exploded on the ship of {playerShipAfter.Player.NickName}");
                    break;
                }
            }
        }
    }
}