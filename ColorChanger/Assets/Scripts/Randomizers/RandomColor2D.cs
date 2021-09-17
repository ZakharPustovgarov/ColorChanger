using UnityEngine;
using UnityEngine.UI;

public class RandomColor2D : RandomColor
{
    Image _image;
    
    protected override void Start()
    {
        base.Start();

        _image = GetComponent<Image>();
        _image.color = _color;
    }
}
