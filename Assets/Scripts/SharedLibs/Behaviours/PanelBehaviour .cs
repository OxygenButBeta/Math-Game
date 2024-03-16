using System.Collections.Generic;
using static PanelController;

namespace UnityEngine
{

    /// <summary>
    /// This class is used to control the panels for UI elements
    /// This class is a extension of the MonoBehaviour class for handle
    /// the opening and closing of the panels properly
    /// </summary>
    public abstract class PanelBehaviour : MonoBehaviour
    {
        #region Static Functions and Properties



        /// <summary>
        /// If true, the inactive classes will be included in the list
        /// </summary>
        public static bool SelfIncludeInactive => true;

        /// <summary>
        /// This property returns all the classes which inherits from PanelBehaviour from the active scene
        /// </summary>
        public static IEnumerable<PanelBehaviour> GetAllPanelBehaviors => GameObject.FindObjectsOfType<PanelBehaviour>(SelfIncludeInactive);

        /// <summary>
        /// Start Invoking all the BeforeOpening functions of all classes which inherits from PanelBehaviour
        /// </summary>
        public static void ExecuteAllBeforeOpenings() => GetAllPanelBehaviors.FastEach(x => x.BeforeOpening());
        /// <summary>
        /// Start Invoking all the AfterOpening functions of all classes which inherits from PanelBehaviour
        /// </summary>
        public static void ExecuteAllAfterOpenings() => GetAllPanelBehaviors.FastEach(x => x.AfterOpening());

        /// <summary>
        /// Start Invoking all the AfterClosing functions of all classes which inherits from PanelBehaviour
        /// </summary>
        public static void ExecuteAllBeforeClosings() => GetAllPanelBehaviors.FastEach(x => x.BeforeClosing());
        /// <summary>
        /// Start Invoking all the AfterClosing functions of all classes which inherits from PanelBehaviour
        /// </summary>
        public static void ExecuteAllAfterClosings() => GetAllPanelBehaviors.FastEach(x => x.AfterClosing());
        #endregion
        /// <summary>
        /// If True the panel will apply the animation while opening.
        /// </summary>
        [SerializeField] public bool ApplyAnimation = true;


        #region Inheritable Virtual Functions/Callbacks

        [HideInInspector] public virtual void CheckArgument(object args) { } //=> Do something with the argument
        /// <summary>
        /// This Callback will be called while the parent is closing
        /// </summary>
        [HideInInspector] public virtual void ParentClosing() { } //=> Do something before closing the panel

        /// <summary>
        /// This Callback will be called before opening the panel
        /// </summary>
        [HideInInspector] public virtual void BeforeOpening() { } // => Do something before Openig

        /// <summary>
        /// This Callback will be called after opening the panel
        /// </summary>
        [HideInInspector] public virtual void AfterOpening() { }//=> Do something after Openig

        /// <summary>
        /// This Callback will be called before closing the panel
        /// </summary>
        [HideInInspector] public virtual void BeforeClosing() { } //=> Do something before closing the panel

        /// <summary>
        /// This Callback will be called after closing the panel
        /// </summary>
        [HideInInspector] public virtual void AfterClosing() { } //=> Do something after closing the panel

        [HideInInspector] public virtual void ReFreshPanel() { } //=> Do something to refresh the panel
        #endregion

        #region  Properties

        /// <summary>
        /// This Propert returns the name of the Panel Behaviour that opened this panel
        /// </summary>
        private string Caller = string.Empty;

        /// <summary>
        /// Short hand of gameobject.name
        /// </summary>
        protected string Self => gameObject.name;

        /// <summary>
        /// this property returns true if the panel has been opened once
        /// </summary>
        public bool OpenedOnce { get; set; } = false;
        #endregion

        #region Inheritable Functions

        /// Opens the panel with the given name and sets the caller to this panel
        /// </summary>
        /// <param name="TargetPanel"></param>
        public void OpenPanel(string TargetPanel, object args = null) => PanelController.SetActive(TargetPanel, true).IfNotNull(x =>
        {
            x.Caller = Self;
            if (args != null)
                x.CheckArgument(args);
        });
        public void OpenPanel(string TargetPanel) => OpenPanel(TargetPanel, null);


        /// <summary>
        /// Opens the panel with the given name and sets the caller to this panel 
        /// And closes this panel
        /// </summary>
        /// <param name="TargetPanel"></param>
        /// 
        public void OpenPanelSingle(string TargetPanel, object args = null)
        {
            SetActive(Self, false);
            OpenPanel(TargetPanel, args);
        }
        public void OpenPanelSingle(string TargetPanel) => OpenPanelSingle(TargetPanel, null);
        /// <summary>
        /// Opens the panel with the given name and sets the caller to this panel
        /// </summary>
        /// <param name="TargetPanel"></param>
        /// <param name="Caller"></param>
        public void OpenSingleOverride(string TargetPanel, string Caller)
        {
            SetActive(Caller, false);
            SetActive(TargetPanel, true).Caller = Caller;
        }

        /// <summary>
        /// This method closes the current panel
        /// </summary>
        /// 
        public void CloseSelf() => PanelController.SetActive(Self, false).Caller = string.Empty;

        /// <summary>
        /// This method closes the current panel and opens the caller panel
        /// </summary>
        public void BackToTheCaller()
        {
            if (Caller != string.Empty)
                PanelController.SetActive(Caller, true);
            CloseSelf();
        }

        /// <summary>
        /// This Method call every refresh callback in the children of this panel
        /// </summary>
        public void ReFreshChilderen() => gameObject.GetComponentsInChildren<PanelBehaviour>(true)?.For(x => x.ReFreshPanel());

        #endregion
    }
}
