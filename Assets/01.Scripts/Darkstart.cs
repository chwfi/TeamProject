using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class Darkstart : Reflective
{
    [SerializeField] private DarkStar d;
    [SerializeField] private float time = 3f;

    public Color targetColor;

    private float timer = 0f;
    private bool isis = false;
    private bool colorM = false;
    public override void OnHandleReflected()
    {
        base.OnHandleReflected();

        isis = true;
    }
    public override void UnHandleReflected()
    {
        isis = false;

        timer = 0;
    }
    public override void GetReflectedObjectDataModify(ReflectData reflectedData)
    {

    }
    private bool asdf = false;
    private void Update()
    {
        if (isis && colorM)
        {
            timer += Time.deltaTime;
            if (timer > time)
            {
                if (asdf == false)
                {
                    d.Ending();
                    asdf = true;
                }
            }
        }

    }
    public void ColorMatch(Color inputColor) // �ٸ� �Լ����� �����Ͽ� �� ��
    {
        if (ColorSystem.CompareColor(inputColor, targetColor))
        {
            colorM = true;
        }
        else colorM = false;
    }
}
