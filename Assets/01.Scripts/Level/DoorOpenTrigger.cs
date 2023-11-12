using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("목표 색")]
    private Color targetColor;

    [SerializeField]
    private ColorDoor linkedDoor; // 열어줄 문

    private List<ParticleSystem> _dustParticles = new List<ParticleSystem>();
    private List<Rigidbody> _rigids = new List<Rigidbody>();

    bool _isOpend = false;

    private void Awake()
    {
        _dustParticles.AddRange(GetComponentsInChildren<ParticleSystem>());
        _rigids.AddRange(GetComponentsInChildren<Rigidbody>());
    }

    public void ColorMatch(Color inputColor) // 다른 함수에서 실행하여 비교 함
    {
        if (ColorSystem.CompareColor(inputColor, targetColor) && !_isOpend)
        {
            linkedDoor.OpenDoor(); // 같은 색이라면 문 염
            SoundManager.Instance.PlaySFXSound("StoneFall");

            _dustParticles.ForEach(p => p.Play()); //먼지 파티클 실행해주고

            _rigids.ForEach(v => //체인들 떨어뜨리기
            {
                v.useGravity = true;
                Destroy(v.gameObject, 2f);
            });

            _isOpend = true;
        }
    }
}
