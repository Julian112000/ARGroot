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

    private float rotation;
    private bool selected;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
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
    }
    private void OnMouseExit()
    {
        if (selected) return;
        meshRenderer.material.color = normalColor;
    }
}
