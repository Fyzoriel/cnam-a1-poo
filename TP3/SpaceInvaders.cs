using System;
using System.Collections.Generic;
using System.Threading;
using TP3.Enum;
using TP3.SpaceShips;
using TP3.SpaceShips.Enemies;
using TP3.SpaceShips.Enemies.Specials;
using TP3.SpaceShips.Interfaces;
using TP3.SpaceShips.Players;
using TP3.Utils;


namespace TP3
{
    public class SpaceInvaders
    {
        private List<Player> Players { get; } = new();
        private List<SpaceShip> Enemies { get; } = new();
        private Armory PlayerArmory { get; set; }
        private Armory EnemiesArmory { get; set; }
        
        private WeaponImporter WeaponImporter { get; }

        static void Main(string[] args)
        {

            var game = new SpaceInvaders();
            
            int choice = 0;
            do
            {
                game.DisplayArmory();
                game.DisplayPlayers();
                game.DisplayEnemies();
                
                Console.WriteLine("=== MENU ===");
                Console.WriteLine("0 - Quit");
                Console.WriteLine("1 - Play");
                Console.WriteLine("2 - Players Management");
                Console.WriteLine("3 - Ships Management");
                Console.WriteLine("4 - Weapons Management");

                choice = Ask.AskInt();

                switch (choice)
                {
                    case 1:
                        game.Play();
                        break;
                    case 2:
                        game.ManagePlayers();
                        break;
                    case 3:
                        Console.WriteLine("Not yet implemented");
                        break;
                    case 4:
                        Console.WriteLine("Not yet implemented");
                        break;
                }

            } while (choice != 0);
            
        }

        private SpaceInvaders()
        {            
            WeaponImporter = new WeaponImporter("/Users/fyzoriel/Desktop/test.txt");
            InitArmory();
            InitPlayer();
            InitEnemies();
        }

        private void Play()
        {
            int i = 1;
            while (AlivePlayersSpaceShips().Count != 0 && AliveEnemiesSpaceShips().Count != 0)
            {
                Console.WriteLine($"Turn {i}");
                Turn();
                DisplayPlayers();
                // game.DisplayEnemies();
                Console.WriteLine("");
                i++;
            }
            if (HasPlayersWon())
            {
                Console.WriteLine("Players has won c:");
            }
            else
            {
                DisplayEnemies();
                Console.WriteLine("Enemies has won :c");
            }
        }

        private void InitArmory()
        {
            List<Weapon> weaponsFromImporter = WeaponImporter.Weapons;
            
            var playerWeapons = new List<Weapon>();
            playerWeapons.Add(new ("Mitrailleuse", 2, 3, 1, WeaponType.Direct));
            playerWeapons.Add(new ("EMG", 1, 7, 1.5f, WeaponType.Explosive));
            playerWeapons.Add(new ("Missile", 4, 100, 4, WeaponType.Guided));
            playerWeapons.AddRange(weaponsFromImporter);
            PlayerArmory = new Armory(playerWeapons);
            
            var enemiesWeapons = new List<Weapon>();
            enemiesWeapons.Add(new ("Laser", 3, 10, 1, WeaponType.Direct));
            enemiesWeapons.Add(new ("Hammer", 1, 8, 1.5f, WeaponType.Explosive));
            enemiesWeapons.Add(new ("Torpille", 3, 3, 2, WeaponType.Guided));
            enemiesWeapons.AddRange(weaponsFromImporter);
            EnemiesArmory = new Armory(enemiesWeapons);
        }
        
        /// <summary>
        /// Init the players with a default ship
        /// </summary>
        private void InitPlayer()
        {
            ViperMKII playerShip = new(PlayerArmory);

            var player = new Player("Seguin", "leo", "Fyzoriel", playerShip);
            Players.Add(player);
            playerShip.Player = player;
        }

        private void InitEnemies()
        {
            Enemies.Add(new Dart(EnemiesArmory));
            Enemies.Add(new F18(EnemiesArmory));
            Enemies.Add(new Rocinante(EnemiesArmory));
            Enemies.Add(new Tardis(EnemiesArmory));
            Enemies.Add(new BWings(EnemiesArmory));
        }

        public void Turn()
        {
            RegenShield();

            List<SpaceShip> spaceShips = AllAliveSpaceShips();
            
            // Aptitudes
            foreach(SpaceShip spaceShip in AllAliveSpaceShips())
            {
                if(spaceShip is IAptitude aptitudeShip)
                {
                    aptitudeShip.Use(spaceShips);
                }
            }

            var playersAlreadyAttack = new List<SpaceShip>();

            var random = new Random();
            
            List<SpaceShip> aliveEnemies = AliveEnemiesSpaceShips();
            List<ViperMKII> alivePlayers = AlivePlayersSpaceShips();
            
            var playTurnCount = 1;
            
            foreach (SpaceShip spaceShip in AliveEnemiesSpaceShips())
            {
                if (aliveEnemies.Count == 0 || alivePlayers.Count == 0)
                {
                    break;
                }

                // Player Attack
                foreach (ViperMKII playerShip in alivePlayers)
                {
                    if (random.Next(aliveEnemies.Count) <= playTurnCount 
                        && !playersAlreadyAttack.Contains(playerShip)
                        && !playerShip.IsDestroyed)
                    {
                        SpaceShip enemy = aliveEnemies[random.Next(aliveEnemies.Count - 1)];
                        Console.WriteLine("Player attack");
                        playerShip.Attack(enemy);
                        Console.WriteLine(" ");
                        
                        if (enemy.IsDestroyed)
                        {
                            aliveEnemies = AliveEnemiesSpaceShips();
                            Console.WriteLine($"Enemy destroyed | {aliveEnemies.Count} left");
                        }
                        playersAlreadyAttack.Add(playerShip);
                    }
                }
                
                // Enemy attack
                if (spaceShip.IsDestroyed)
                {
                    continue;
                }
                ViperMKII player = alivePlayers[random.Next(alivePlayers.Count - 1)];
                Console.WriteLine("Enemy attack");
                spaceShip.Attack(player);
                
                if (player.IsDestroyed)
                {
                    alivePlayers = AlivePlayersSpaceShips();
                    Console.WriteLine($"Player destroyed | {alivePlayers.Count} left");
                }
                playTurnCount++;
                Console.WriteLine(" ");
            }
            Thread.Sleep(1000);
        }

        private void RegenShield()
        {
            foreach (SpaceShip spaceShip in AllAliveSpaceShips())
            {
                spaceShip.HealShield(2);
            }
            Console.WriteLine("The shields have been slightly repaired");
        }

        private List<SpaceShip> AllAliveSpaceShips()
        {
            List<SpaceShip> damaged = AliveEnemiesSpaceShips();
            damaged.AddRange(AlivePlayersSpaceShips());
            return damaged;
        }

        private List<SpaceShip> AliveEnemiesSpaceShips()
        {
            return Enemies.FindAll(ship => !ship.IsDestroyed);
        }
        
        private List<ViperMKII> AlivePlayersSpaceShips()
        {
            var alivePlayersSpaceShip = new List<ViperMKII>();
            foreach (Player player in Players)
            {
                if (!player.SpaceShip.IsDestroyed)
                {
                    alivePlayersSpaceShip.Add(player.SpaceShip);
                }
            }
            return alivePlayersSpaceShip;
        }

        private List<Player> AlivePlayers()
        {
            return Players.FindAll(player => !player.SpaceShip.IsDestroyed);
        }
        
        private bool HasPlayersWon()
        {
            return AlivePlayersSpaceShips().Count != 0;
        }
        
        /// <summary>
        /// Display all players and their ships
        /// </summary>
        private void DisplayPlayers()
        {
            Console.WriteLine("Alive Players:");
            foreach (Player player in AlivePlayers())
            {
                Console.WriteLine(player);
                Console.WriteLine("Ship:");
                Console.WriteLine(player.SpaceShip);
            }
        }

        /// <summary>
        /// Display all enemies ships
        /// </summary>
        private void DisplayEnemies()
        {
            Console.WriteLine("Alive Enemies:");
            foreach (SpaceShip spaceShip in AliveEnemiesSpaceShips())
            {
                Console.WriteLine(spaceShip);
            }
        }
        
        /// <summary>
        /// Display all weapon in the Armory
        /// </summary>
        private void DisplayArmory()
        {
            Console.WriteLine("Player Armory");
            Console.WriteLine(PlayerArmory);
            Console.WriteLine("Enemies Armory");
            Console.WriteLine(EnemiesArmory);
        }

        private void ManagePlayers()
        {
            Console.WriteLine("=== Players ===");
            Console.WriteLine("Not yet implemented");
        }
    }
}