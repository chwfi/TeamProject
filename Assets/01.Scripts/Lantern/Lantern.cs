using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using static Define.Define;

[RequireComponent(typeof(LineRenderer))]
public class Lantern : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    public Color defaultColor;

    private LineRenderer lb;

    private ReflectData myReflectData;

    private Reflective reflectObject = null;

    private ReflectState _currentState = ReflectState.NULL;

    public ReflectState CurrentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState == value) return;

            _currentState = value;
        }
    }

    public float lgihtFadeInoutDuration = .3f;

    private Vector3 _startPos = Vector3.zero;
    private Vector3 _endPos = Vector3.zero;

    private Coroutine coroutine;

    private float startTime = 0;
    private void Awake()
    {
        myReflectData.color = defaultColor;

        lb = (LineRenderer)GetComponent("LineRenderer");
        lb.positionCount = 2;

        lb.material.color = defaultColor;

        _inputReader.OnStartFireEvent += OnStartShootLight;
        _inputReader.OnShootingFireEvent += OnShootLight;
        _inputReader.OnStopFireEvent += OnEndShootLight;
    }
    private void OnStartShootLight()
    {
        lb.enabled = true;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
    private void OnEndShootLight()
    {

        elapsedTime = 0;
        raycastDistance = 0;
        startTime = 0;

        coroutine = StartCoroutine(DrawAndFadeLineCoroutine());
    }
    private IEnumerator DrawAndFadeLineCoroutine() //서서히 빛이 사라지는 코드
    {
        lb.SetPosition(0, _startPos);
        lb.SetPosition(1, _endPos);

        while (startTime < lgihtFadeInoutDuration)
        {
            startTime += Time.deltaTime;
            Vector3 lerpedPosition = Vector3.Lerp(_startPos, _endPos, startTime / lgihtFadeInoutDuration);

            lb.SetPosition(0, lerpedPosition);

            yield return null;
        }

        lb.enabled = false;

        if (reflectObject != null)
        {
            reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
            reflectObject = null;
        }
    }

    private void OnShootLight()
    {
        StartShootLight(transform.position, transform.forward);
    }

    public void StartShootLight(Vector3 origin, Vector3 direction)
    {
        _startPos = origin;

        myReflectData.hitPos = origin;
        myReflectData.direction = direction;

        DataModify(myReflectData);
    }

    private float elapsedTime = 0f;

    private float raycastDistance = 0;

    public void DataModify(ReflectData reflectData)
    {
        lb.SetPosition(0, reflectData.hitPos);
        RaycastHit hit;

        elapsedTime += Time.deltaTime;

        if (elapsedTime < lgihtFadeInoutDuration) //레이저 발사 중일때
        {
            float t = elapsedTime / lgihtFadeInoutDuration;
            raycastDistance = t * 10;

            Vector3 endPosition = reflectData.hitPos + reflectData.direction * raycastDistance;
            lb.SetPosition(1, endPosition);

            _endPos = endPosition;

            if (Physics.Raycast(reflectData.hitPos, reflectData.direction, out hit, raycastDistance, ReflectionLayer))
            //레이저 발사 중일때 오브젝트가 맞았을때
            {
                if (hit.collider.TryGetComponent<Reflective>(out var reflectable))
                {
                    myReflectData.hitPos = hit.point;
                    myReflectData.direction = reflectData.direction;
                    myReflectData.normal = hit.normal;

                    lb.SetPosition(1, hit.point);

                    ChangedReflectObject(reflectable);

                    reflectable?.OnReflectTypeChanged(ReflectState.OnReflect);
                    reflectable?.GetReflectedObjectDataModify(myReflectData);

                    _endPos = hit.point;
                }
                
                if (hit.collider.TryGetComponent<DoorOpenTrigger>(out var door))
                {
                    myReflectData.hitPos = hit.point;
                    myReflectData.direction = reflectData.direction;
                    myReflectData.normal = hit.normal;

                    door.ColorMatch(myReflectData.color);
                    lb.SetPosition(1, hit.point);
                    _endPos = hit.point;
                }
            }
            else ////레이저 발사 중일때 오브젝트가 안맞았을때
            {
                if (reflectObject != null)
                {
                    reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
                    reflectObject = null;
                }
            }
        }
        else //레이저 발사가 끝났을때
        {
            if (Physics.Raycast(reflectData.hitPos, reflectData.direction, out hit, 1000, ReflectionLayer))
            {
                if (hit.collider.TryGetComponent<Reflective>(out var reflectable))
                {
                    myReflectData.hitPos = hit.point;
                    myReflectData.direction = reflectData.direction;
                    myReflectData.normal = hit.normal;

                    lb.SetPosition(1, hit.point);

                    ChangedReflectObject(reflectable);

                    reflectable?.OnReflectTypeChanged(ReflectState.OnReflect);
                    reflectable?.GetReflectedObjectDataModify(myReflectData);

                    _endPos = hit.point;
                }

                if (hit.collider.TryGetComponent<DoorOpenTrigger>(out var door))
                {
                    myReflectData.hitPos = hit.point;
                    myReflectData.direction = reflectData.direction;
                    myReflectData.normal = hit.normal;

                    door.ColorMatch(myReflectData.color);
                    lb.SetPosition(1, hit.point);
                    _endPos = hit.point;
                }
            }
            else //레이저 발사가 끝났을때
            {
                if (reflectObject != null)
                {
                    reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
                    reflectObject = null;
                }

                lb.SetPosition(1, hit.point + reflectData.direction * 1000); //포지션이라 쏘는 위치를 더해줘야함
                _endPos = hit.point + reflectData.direction * 1000;
            }
        }
    }

    private void ChangedReflectObject(Reflective reflectable)
    {
        if (reflectObject == reflectable) return;
        reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);

        reflectObject = reflectable;
    }

    private void OnDestroy()
    {
        _inputReader.OnStartFireEvent -= OnShootLight;
        _inputReader.OnShootingFireEvent -= OnShootLight;
        _inputReader.OnStopFireEvent -= OnShootLight;
    }
}
