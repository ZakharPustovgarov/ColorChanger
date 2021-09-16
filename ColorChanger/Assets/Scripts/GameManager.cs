using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Text textR, textG, textB;
    float R, G, B;
    float r
    {
        set
        {
            if (value > 255) R = 255;
            else if (value < 0) R = 0;
            else R = value;
        }

        get 
        {
            return R;
        }
    }
    float g
    {
        set
        {
            if (value > 255) G = 255;
            else if (value < 0) G = 0;
            else G = value;
        }

        get
        {
            return G;
        }
    }
    float b
    {
        set
        {
            if (value > 255) B = 255;
            else if (value < 0) B = 0;
            else B = value;
        }

        get
        {
            return B;
        }
    }

    [SerializeField]
    Slider slider;

    [SerializeField]
    PlayerController player;

    bool isCoroutineRunning;

    void Start()
    {
        isCoroutineRunning = false;
    }

    void UpdateUI(bool isNewValues  = false)
    {
        textR.text = Convert.ToString(r);
        textG.text = Convert.ToString(g);
        textB.text = Convert.ToString(b);
        if (isNewValues == true) slider.value = g;
    }

    public void SetInitialColor(Color color)
    {
        r = Convert.ToInt32(255 * color.r);
        g = Convert.ToInt32(255 * color.g);
        b = Convert.ToInt32(255 * color.b);
        UpdateUI(true);
    }

    void SetNewColor()
    {
        if (player.is2D == false && player.mat3D != null) player.mat3D.color = new Color(r / 255, g / 255, b / 255, 1f);
        else player.image2D.color = new Color(r / 255, g / 255, b / 255, 1f);
    }

    public void IncrementR()
    {
        r++;
        UpdateUI();
        SetNewColor();
    }

    public void DecrementR()
    {
        r--;
        UpdateUI();
        SetNewColor();
    }

    public void GetSliderValueG()
    {
        g = slider.value;
        UpdateUI();
        SetNewColor();
    }

    public void SetRandomB()
    {
        b = Convert.ToInt32(UnityEngine.Random.Range(0f, 255f));
        UpdateUI();
        SetNewColor();
    }

    IEnumerator HoldR(bool isIncrement)
    {
        yield return new WaitForSeconds(0.7f);

        float interval = 0.2f;
        int iterations = 0;

        while(isCoroutineRunning == true)
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
        isCoroutineRunning = true;
        StartCoroutine(HoldR(isIncrement));
    }

    public void StopChangingR()
    {
        isCoroutineRunning = false;           
    }
}
