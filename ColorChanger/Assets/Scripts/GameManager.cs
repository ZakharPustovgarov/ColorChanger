using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Text _textR, _textG, _textB;
    float _R, _G, _B;
    float _r
    {
        set
        {
            if (value > 255) _R = 255;
            else if (value < 0) _R = 0;
            else _R = value;
        }

        get 
        {
            return _R;
        }
    }
    float _g
    {
        set
        {
            if (value > 255) _G = 255;
            else if (value < 0) _G = 0;
            else _G = value;
        }

        get
        {
            return _G;
        }
    }
    float _b
    {
        set
        {
            if (value > 255) _B = 255;
            else if (value < 0) _B = 0;
            else _B = value;
        }

        get
        {
            return _B;
        }
    }

    [SerializeField]
    Slider _slider;

    [SerializeField]
    PlayerController _player;

    bool _isCoroutineRunning;

    void Start()
    {
        _isCoroutineRunning = false;
    }

    void UpdateUI(bool isNewValues  = false)
    {
        _textR.text = Convert.ToString(_r);
        _textG.text = Convert.ToString(_g);
        _textB.text = Convert.ToString(_b);
        if (isNewValues == true) _slider.value = _g;
    }

    public void SetInitialColor(Color color)
    {
        _r = Convert.ToInt32(255 * color.r);
        _g = Convert.ToInt32(255 * color.g);
        _b = Convert.ToInt32(255 * color.b);
        UpdateUI(true);
    }

    void SetNewColor()
    {
        if (_player.is2D == false && _player.mat3D != null) _player.mat3D.color = new Color(_r / 255, _g / 255, _b / 255, 1f);
        else _player.image2D.color = new Color(_r / 255, _g / 255, _b / 255, 1f);
    }

    public void IncrementR()
    {
        _r++;
        UpdateUI();
        SetNewColor();
    }

    public void DecrementR()
    {
        _r--;
        UpdateUI();
        SetNewColor();
    }

    public void GetSliderValueG()
    {
        _g = _slider.value;
        UpdateUI();
        SetNewColor();
    }

    public void SetRandomB()
    {
        _b = Convert.ToInt32(UnityEngine.Random.Range(0f, 255f));
        UpdateUI();
        SetNewColor();
    }

    IEnumerator HoldR(bool isIncrement)
    {
        yield return new WaitForSeconds(0.7f);

        float interval = 0.2f;
        int iterations = 0;

        while(_isCoroutineRunning == true)
        {
            if (iterations > 50) interval = 0.07f;
            else if (iterations > 30) interval = 0.1f;
            else if (iterations > 10) interval = 0.15f;
            

            if (isIncrement == true) IncrementR();
            else DecrementR();

            iterations++;

            yield return new WaitForSeconds(interval);
        }
    }

    public void StartChangingR(bool isIncrement)
    {
        _isCoroutineRunning = true;
        StartCoroutine(HoldR(isIncrement));
    }

    public void StopChangingR()
    {
        _isCoroutineRunning = false;           
    }
}
