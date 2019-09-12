using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTab : MonoBehaviour
{

    [SerializeField]
    private GameObject modelTab;
    [SerializeField]
    private GameObject arrow, arrowDown;

    public void OpenBuildTab()
    {
        Debug.Log("Opened tab");
        if (modelTab.activeSelf)
        {
            modelTab.SetActive(false);
            arrow.SetActive(true);
            arrowDown.SetActive(false);
        }
        else
        {
            modelTab.SetActive(true);
            arrow.SetActive(false);
            arrowDown.SetActive(true);
        }

    }
}
