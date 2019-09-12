using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelector : MonoBehaviour
{
    public static ObjectSelector Instance;

    private MapUnit selectedObject;

    [SerializeField]
    private Text nametext;
    [SerializeField]
    private Text desctext;
    [SerializeField]
    private Image iconimage;
    [SerializeField]
    private Slider rotationSlider;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectObject(MapUnitData data, MapUnit mapunit, float rotation)
    {
        selectedObject = mapunit;
        nametext.text = data.name;
        desctext.text = data.desc;
        iconimage.sprite = data.icon;
        rotationSlider.value = rotation;
    }

    public void RotateObject(float angle)
    {
        selectedObject.Rotate(angle);
    }
}
