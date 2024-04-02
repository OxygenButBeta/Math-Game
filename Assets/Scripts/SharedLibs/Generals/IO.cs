using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;

namespace SumStudio
{
    public static class IO
    {
        public static string ReadJson(string FilePath, bool CombineWithStreamingAssets = true)
        {
            if (CombineWithStreamingAssets)
                FilePath = Path.Combine(Application.streamingAssetsPath, FilePath);

            if (Application.platform == RuntimePlatform.Android)
            {
                UnityWebRequest www = UnityWebRequest.Get(FilePath);
                www.SendWebRequest();
                while (!www.isDone) ;
                return www.downloadHandler.text;
            }
            return File.ReadAllText(FilePath);
        }
    }
}
