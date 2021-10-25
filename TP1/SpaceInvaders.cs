using System;
using System.Collections.Generic;

namespace TP1
{
    class SpaceInvaders
    {
        private List<Player> Players { get; } = new();
        private Armory Armory { get; } = new();

        static void Main(string[] args)
        {
            var game = new SpaceInvaders();

            game.DisplayArmory();
            game.DisplayPlayers();
        }

        public SpaceInvaders()
        {
            Init();
        }
        
        /// <summary>
        /// Init the players with a default ship
        /// </summary>
        private void Init()
        {
            SpaceShip spaceShip = new(100, 100, Armory);
            
            spaceShip.AddWeapon(Armory.GetWeapon(1), 1);

            Players.Add(new Player("Seguin","leo","Fyzoriel", spaceShip));
            Players.Add(new Player("Second", "Player", "nickName2", spaceShip));
            Players.Add(new Player("Name", "FirstName", "Lae", spaceShip));
            
            // Test methods
            // spaceShip.AddWeapon(Armory.GetWeapon(1), 4);
            // spaceShip.AddWeapon(new Weapon("test Exception", 10, 10, WeaponType.GUIDED), 2);
        }

        /// <summary>
        /// Display all players and their ships
        /// </summary>
        public void DisplayPlayers()
        {
            foreach (Player player in Players)
            {
                Console.WriteLine(player);
                Console.WriteLine("Ship:");
                Console.WriteLine(player.SpaceShip);
            }
        }

        /// <summary>
        /// Display all weapon in the Armory
        /// </summary>
        public void DisplayArmory()
        {
            Console.WriteLine(Armory);
        }
    }
}