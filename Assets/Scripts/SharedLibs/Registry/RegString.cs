using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SharedLibs.Registry
{
    public class RegString
    {
        private string Key;
        public RegString(string key) => Key = key;

        public static implicit operator string(RegString value)
        {
            return PlayerPrefs.GetString(value.Key);
        }
        public static RegString operator +(RegString value, string str)
        {
            PlayerPrefs.SetString(value.Key, PlayerPrefs.GetString(value.Key) + str);
            return value;
        }
        public void Set(string value)
        {
            PlayerPrefs.SetString(Key, value);
        }
    }
}
