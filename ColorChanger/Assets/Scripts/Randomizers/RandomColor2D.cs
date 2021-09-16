using UnityEngine;
using UnityEngine.UI;

public class RandomColor2D : RandomColor
{
    Image image;
    
    protected override void Start()
    {
        base.Start();

        image = GetComponent<Image>();
        image.color = color;
    }
}
