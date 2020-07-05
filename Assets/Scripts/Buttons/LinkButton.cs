using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LinkButton : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp (PointerEventData evenData)
	{
        OpenLink();
	}

    public void OpenLink()
    {
        Application.OpenURL("http://www.hexar.org/");
    }
}
