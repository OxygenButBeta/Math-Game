using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.SharedLibs.Language
{
    public class LanguageSync : LanguageSyncable
    {
        [SerializeField] TMP_Text TextField;
        [SerializeField] string Key;
        private void Reset() => TextField = GetComponent<TMP_Text>();
        private void Start() => Sync();
        public override void Sync() => TextField.text = LanguageManager.GetText(Key);

    }
}
