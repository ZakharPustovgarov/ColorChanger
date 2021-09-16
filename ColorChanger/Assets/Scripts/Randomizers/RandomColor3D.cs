using UnityEngine;

public class RandomColor3D : RandomColor
{
    Material material;

    protected override void Start()
    {
        base.Start();

        material = GetComponent<MeshRenderer>().material;
        material.color = color;
    }
}
