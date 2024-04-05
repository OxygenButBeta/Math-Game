using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueAnim : MonoBehaviour
{
    [SerializeField] Image logo;
    [SerializeField] Image brand;
    void Start()
    {
        InvokeRepeating("DisplayLogo", 0, 1);
    }
    void DisplayLogo()
    {
        logo.enabled = logo.enabled!;
        brand.enabled = brand.enabled!;
        Debug.Log("Logo displayed");

    }
    void DisplayBrand()
    {

    }
}
