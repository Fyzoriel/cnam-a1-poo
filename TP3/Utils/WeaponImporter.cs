using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TP3.Enum;
using TP3.Extension;


namespace TP3.Utils
{
    public class WeaponImporter
    {
        private int MinChar {get;}
        private List<string> BlackList { get; }
        public List<Weapon> Weapons { get; } = new();

        public WeaponImporter(string file) : this(file, 0, new List<string>()) {}
        public WeaponImporter(string file, int minChar) : this(file, minChar, new List<string>()) {}
        public WeaponImporter(string file, List<string> blackList) : this(file, 0, blackList) {}
        
        public WeaponImporter(string file, int minChar, List<string> blackList)
        {
            BlackList = blackList;
            MinChar = minChar;

            LoadWeapons(file);
        }

        private Dictionary<string, int> ReadFile(string file)
        {

            Dictionary<string, int> dictionary = new();

            string text = File.ReadAllText(file).Replace("\n", " ");

            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrEmpty(text))
            {
                return dictionary;
            }
            
            foreach (string str in text.Split(" ")) 
            {
                string formatted = FormatWord(str);
                if (formatted.Length < MinChar || BlackList.Contains(formatted))
                {
                    continue;
                }
                if (!dictionary.ContainsKey(formatted))
                {
                    dictionary[formatted] = 0;
                }
                dictionary[formatted]++;
            }
            return dictionary;
        }
        
        private string FormatWord(string word)
        {
            return Regex.Replace(word, @"[^\d\D]", String.Empty).ToLower();
        }

        private void LoadWeapons(string file)
        {
            Dictionary<string, int> dictionary = ReadFile(file);
            
            foreach (string key in dictionary.Keys)
            {
                int min = dictionary[key], max = key.Length;
                if (max < min)
                {
                    max = min;
                    min = key.Length;
                }
                Random random = new Random();

                WeaponType weaponType = (WeaponType) typeof(WeaponType).GetRandomEnumValue();

                Weapon weapon = new Weapon(key, min, max, random.Next(7)+1, weaponType);
                Weapons.Add(weapon);
            }
        }
    }
}