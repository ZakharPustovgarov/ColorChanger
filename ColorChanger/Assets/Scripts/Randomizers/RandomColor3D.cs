using UnityEngine;

public class RandomColor3D : RandomColor
{
    Material _material;

    protected override void Start()
    {
        base.Start();

        _material = GetComponent<MeshRenderer>().material;
        _material.color = _color;
    }
}
