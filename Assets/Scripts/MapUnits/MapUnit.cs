using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnit : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [SerializeField]
    private MapUnitData data;
    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color hoveredColor;
    //
    private bool selected;

    public float rotation;
    public double latitude;
    public double longitude;
    public float altitude;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public MapUnitData GetData()
    {
        return data;
    }
    //
    private void OnMouseDown()
    {
        ObjectSelector.Instance.SelectObject(data, this, rotation);
    }
    public void Rotate(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        rotation = angle;
    }
    private void OnMouseOver()
    {
        if (selected) return;
        meshRenderer.material.color = hoveredColor;
        ObjectSelector.Instance.hovering = true;
    }
    private void OnMouseExit()
    {
        if (selected) return;
        meshRenderer.material.color = normalColor;
        ObjectSelector.Instance.hovering = false;
    }
}
