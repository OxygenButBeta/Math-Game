using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SharedLibs.Registry
{
    public class RegFloat
    {
        private string Key;
        private float Default;
        public RegFloat(string key, float Default = 0)
        {
            Key = key;
            this.Default = Default;
        }

        public static implicit operator float(RegFloat value)
        {
            return Convert.ToSingle(PlayerPrefs.GetString(value.Key, value.Default.ToString()));
        }


        public void Set(float value)
        {
            PlayerPrefs.SetString(Key, value.ToString());
        }
    }
}
