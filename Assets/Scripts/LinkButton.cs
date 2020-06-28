using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkButton : MonoBehaviour
{
    public void OpenLink()
    {
        print("OpenLink()");
        Application.OpenURL("http://www.hexar.org/");
    }
}
