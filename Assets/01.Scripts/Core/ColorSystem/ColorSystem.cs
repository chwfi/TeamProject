using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ColorSystem
{
    private static ColorSystem _instance;
    public static ColorSystem Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ColorSystem();
            return _instance;
        }
    }
    public Color GetColorCombination(Color value1, Color value2)
    {
        return value1 + value2;
    }

}
