using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectSelector : MonoBehaviour
{
    public static ObjectSelector Instance;

    public MapUnit selectedObject;
    public bool hovering;

    [SerializeField]
    private Text nametext;
    [SerializeField]
    private Text idtext;
    [SerializeField]
    private Image iconimage;
    [SerializeField]
    private Slider rotationSlider;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
    public void SelectModel(MapUnitDataToText data)
    {
        CurrentSelectedModel.Instance.UpdateUI(data);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hovering && !EventSystem.current.IsPointerOverGameObject())
        {
            gameObject.SetActive(false);
        }
    }
    public void SelectObject(MapUnitData data, MapUnit mapunit, float rotation)
    {
        selectedObject = mapunit;
        if (!gameObject.activeSelf || mapunit.gameObject != selectedObject.gameObject)
        {
            gameObject.SetActive(true);
            idtext.text = "ID: [" + data.id.ToString() + "]";
            nametext.text = "[" + data.name + "]";
            iconimage.sprite = data.icon;
            rotationSlider.value = rotation;
        }
        else gameObject.SetActive(false);
    }
    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
    public void RotateObject(float angle)
    {
        selectedObject.Rotate(angle);
    }
}
