using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private Image bar;
    private float currentFill;
    public float maxValue;
    public float currentValue;

    public float MaxValue 
    { 
        get
        {
            return maxValue;
        }
        set 
        {
            maxValue = value;
        }
    }
    
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            if (value <= maxValue && value >= 0)
            {
                currentValue = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentFill = currentValue / maxValue;
        bar.fillAmount = currentFill;
    }
}
