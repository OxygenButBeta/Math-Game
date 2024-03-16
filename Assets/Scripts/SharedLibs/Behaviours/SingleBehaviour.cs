using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{

    public abstract class SingleBehaviour : MonoBehaviour
    {
        #region Static Functions and Properties
        /// <summary>
        /// If true, the inactive classes will be included in the list
        /// </summary>
        public static bool IncludeInactive => true;

        /// <summary>
        /// This property returns all the classes which inherits from SingleBehaviour from the active scene
        /// </summary>
        public static IEnumerable<SingleBehaviour> GetAllSingleBehavioursShorted => GameObject.FindObjectsOfType<SingleBehaviour>(IncludeInactive).OrderBy(x => x.RestartPriority);

        /// <summary>
        /// Start Invoking all the PreRestart functions of all classes which inherits from SingleBehaviour
        /// </summary>
        public static void ExecuteAllPreRestarts() => GetAllSingleBehavioursShorted.FastEach(x => x.PreRestart());

        /// <summary>
        /// Start Invoking all the PostRestart functions of all classes which inherits from SingleBehaviour
        /// </summary>
        public static void ExecuteAllPostRestarts() => GetAllSingleBehavioursShorted.FastEach(x => x.PostRestart());
        #endregion

        #region Inheritable Virtual Functions and Variables

        /// <summary>
        /// Restart Priority of the class. Lower priority will be restarted first
        /// Default value is 0
        /// </summary>
        [HideInInspector] public int RestartPriority = -0; // Lower priority will be restarted first

        /// <summary>
        /// This Callback will be called before restarting the game
        /// </summary>
        public virtual void PreRestart() { } // => Do something before restart

        /// <summary>
        /// This Callback will be called after restarting the game
        /// </summary>
        public virtual void PostRestart() { } // => Do something after restart

        #endregion
    }
}
