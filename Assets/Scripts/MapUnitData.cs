using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Mapunits/unit", order = 1)]
public class MapUnitData : ScriptableObject
{
    public int id;
    public string name;
    public string desc;
    public Sprite icon;
}
