using System.Collections.Generic;
using System;
using System.Linq;

namespace UnityEngine
{


    public static class Extension
    {

        #region Delegates
        public delegate T ArrayAction<T>(T Value);

        #endregion

        #region GameTime Extensions
        /// <summary>
        /// This function calculates the LastActivation between the first LastActivation and the second LastActivation. 
        /// (note: 1st LastActivation must be greater than 2nd LastActivation, otherwise the result might be negative)
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <returns>Returns the diffence as seconds</returns>
        public static int ElapsedTime(this GameTime First, GameTime Second)
        {
            if (First is null || Second is null)
                return 0;
            int Greatest, time;
            if (First > Second)
            {
                Greatest = First.TimeAsSeconds; time = Second.TimeAsSeconds;
            }
            else
            {
                Greatest = Second.TimeAsSeconds; time = First.TimeAsSeconds;
            }
            return Greatest - time;
        }

        /// <summary>
        /// A Function to get LastActivation information in a formatted string. (Output Example: "12:52")
        /// </summary>
        /// <returns></returns>
        public static string TimeToString(int Minute, int Second)
        {
            string min, sec;

            if (Minute < 10)
                min = "0" + Minute.ToString();
            else
                min = Minute.ToString();
            if (Second < 10)
                sec = "0" + Second.ToString();
            else
                sec = Second.ToString();
            return min + ":" + sec;
        }
        #endregion

        #region Unity Extensions
        [HideInCallstack]
        public static void Log(this object obj) => Debug.Log(obj.ToString());
        public static void Log(this object obj, string str) => Debug.Log(str + " " + obj);
        public static bool CheckComponentDuplicated<T>(this Component component)
        {

            if (component.GetComponents<T>().Length > 1)
            {
                Debug.LogError("There cant be more than one PanelBehaviour in any gameobject", component);
                GameObject.DestroyImmediate(component);
            }

            return true;
        }
        #endregion

        #region Generic Object Extensions
        public static void For<T>(this T[] Array, Action<T> Operation) where T : class => Array.For((x, i) =>
            Operation?.Invoke(x)
        );
        public static void For<T>(this T[] Array, Action<T, int> Operation) where T : class
        {
            if (Array is null || Array.Length <= 0)
                return;

            for (int i = 0; i < Array.Length; i++)
                Operation?.Invoke(Array[i], i);
        }
        public static int ToInt32<T>(this T Obj) => Convert.ToInt32(Obj);
        public static void FastEach<T>(this List<T> Collection, Action<T> IEnumerableOperation)
        {
            for (int i = 0; i < Collection.Count(); i++)
                IEnumerableOperation?.Invoke(Collection[i]);

        }
        public static void FastEach<T>(this IEnumerable<T> Collection, Action<T> IEnumerableOperation)
        {
            foreach (var item in Collection)
                IEnumerableOperation?.Invoke(item);

        }
        public static void FastEach<T>(this T[] Collection, Action<T> IEnumerableOperation)
        {
            for (int i = 0; i < Collection.Count(); i++)
                IEnumerableOperation?.Invoke(Collection[i]);

        }

        public static bool IsNullOrEmpty<T>(this T[] Array) => Array is null || Array.Length <= 0;
        public static T[] ConvertType<T>(this object[] Array)
        {
            if (Array.IsNullOrEmpty())
                throw new Exception("Array is null or Empty");

            T[] LA = new T[Array.Length];

            for (int i = 0; i < Array.Length; i++)
                LA[i] = (T)Convert.ChangeType(Array[i], typeof(T));

            return LA;

        }
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            T Component;
            if (!obj.TryGetComponent<T>(out Component))
                Component = obj.AddComponent<T>();

            return Component;
        }
        public static int IsOutOfRange<T>(this T[] Array, int Index)
        {
            if (Index < 0)
                return 0;
            if (Index >= Array.Length)
                return Array.Length - 1;
            else return Index;
        }

        public static void IfNotNull<T>(this T obj, Action<T> action) where T : class
        {
            if (obj != null)
                action?.Invoke(obj);
        }


        #endregion

        #region Base Struct Extensions

        #endregion
    }
}