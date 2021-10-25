using System;

namespace TP1
{
    public class Player
    {
        public string Name { get; }
        public string NickName { get; }
        public SpaceShip SpaceShip { get; set; }

        public Player(string name, string firstName, string nickName, SpaceShip spaceShip)
        {
            Name = $"{Format(firstName)} {Format(name)}";
            NickName = nickName;
            SpaceShip = spaceShip;
        }

        /// <summary>
        /// Format a string to have the first letter in Uppercase and the others in lowercase
        /// </summary>
        /// <param name="toFormat">The string to format</param>
        /// <returns>The formatted string</returns>
        private static string Format(string toFormat)
        {
            if (toFormat == null || toFormat.Length == 0)
            {
                return toFormat;
            }
            if (toFormat.Length == 1)
            {
                return toFormat.ToUpper();
            }
            return $"{char.ToUpper(toFormat[0])}{toFormat.Substring(1).ToLower()}";
        }

        /// <summary>
        /// Return the nickname and the name of the player
        /// </summary>
        /// <returns>The nickname and the name of the player</returns>
        public override string ToString()
        {
            return $"{NickName} ({Name})";
        }

        /// <summary>
        /// Check if the passed object is equal to this player
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>If the passed object is equal to this player</returns>
        public override bool Equals(object obj)
        {
            return obj is Player otherPlayer && NickName == otherPlayer.NickName;
        }
        
        /// <summary>
        /// Auto-generated method
        /// Return the hash code of the object
        /// </summary>
        /// <returns>The hash code of the object</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, NickName);
        }
    }
}