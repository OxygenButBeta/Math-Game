using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkButton : MonoBehaviour
{
    public void Toinstagram()
    {
        Application.OpenURL("https://www.instagram.com/umurdulger/");
    }

    public void Tofacebook()
    {
        Application.OpenURL("https://www.facebook.com/profile.php?id=61557551704775");
    }

    public void Togoogleplay()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=5779781339130385331");
    }

}
