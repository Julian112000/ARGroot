using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUnitDataToText : MonoBehaviour
{
    public MapUnitData model;

    public Text idUI;
    public Text nameUI;
    public Image iconUI;

    public void Update()
    {
        idUI.text = "ID: " + model.id;
        nameUI.text = "" + model.name;
        iconUI.sprite = model.icon;
    }
}
