using System.Collections.Generic;
using UnityEngine;

public class PanelController : SingleBehaviour
{
    #region Single Behaviour
    [SerializeField] private bool IsActiveAtStart = true;
    public static List<GameObject> Panels = new List<GameObject>();
    private void Start()
    {
        gameObject.SetActive(IsActiveAtStart);
        Panels.Add(gameObject);
    }
    private void Reset()
    {
        this.CheckComponentDuplicated<PanelBehaviour>();
        this.CheckComponentDuplicated<PanelController>();


    }
    public override void PreRestart() => Panels.Clear();
    #endregion

    #region Static Functions
    public static PanelBehaviour SetActive(string name, bool Active) => InternalSetActive(name, Active, false);
    public static PanelBehaviour SwitchState(string name) => InternalSetActive(name, true, true);
    public static void SwitchState(params string[] names) => names.For(x => SwitchState(x));
    public static bool GetState(string name) => Panels.Find(x => x.name == name).activeSelf;
    public static void SwitchState(params GameObject[] Objects) => Objects.For(x => SwitchState(x.name));
    private static PanelBehaviour InternalSetActive(string name, bool Active, bool Switch)
    {

        PanelBehaviour PBs = null;
        var GO = Panels.Find(x => x.name == name);
        if (GO is not null)
        {
            if (Switch)
                Active = !GO.activeSelf;

            bool PanelBehaviourExist = GO.TryGetComponent<PanelBehaviour>(out PBs);

            #region Panel Behaviour Before CallBacks
            if (PanelBehaviourExist)
            {
                if (Active)
                {
                    PBs.BeforeOpening();
                    PBs.OpenedOnce = true;
                }
                else
                {
                    PBs?.BeforeClosing();
                    GO.GetComponentsInChildren<PanelBehaviour>()?.For(x => x.ParentClosing());
                }
            }
            #endregion

            GO.SetActive(Active);

            #region Panel Behaviour After CallBacks
            if (Active)
            {
                if (PBs != null)
                    if (PBs.ApplyAnimation)
                    {
                        PBs.AfterOpening();
                        var animator = GO.GetOrAddComponent<Animator>();
                        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Logo");
                        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
                    }

            }
            else
                PBs?.AfterClosing();
            #endregion

            if (GetState(name) != Active)
                GO.SetActive(Active);

        }
        else
        {
            // Debug.LogWarning("Missing Panel Trying to Find " + name);
            var obj = GameObject.Find(name);
            if (obj != null)
            {
                if (obj.TryGetComponent(out PanelController v))
                {
                    Debug.Log("Missin Object Found");
                    obj.SetActive(Active);
                }
            }
            //else
            //Debug.LogError("Panel not found with name " + name);
        }
        return PBs;

    }
    #endregion
}
