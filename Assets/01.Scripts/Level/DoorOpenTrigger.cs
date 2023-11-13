using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("��ǥ ��")]
    private Color targetColor;

    [SerializeField]
    private ColorDoor linkedDoor; // ������ ��

    private ArrowUI arrowUI; //ó���� ������ ũ����Ż ���� ȭ��ǥ. ������ ȭ��ǥ�� �����ֱ� ���� ����

    private List<ParticleSystem> _dustParticles = new List<ParticleSystem>();
    private List<Rigidbody> _rigids = new List<Rigidbody>();

    bool _isOpend = false;

    private void Awake()
    {
        arrowUI = FindObjectOfType<ArrowUI>();

        _dustParticles.AddRange(GetComponentsInChildren<ParticleSystem>());
        _rigids.AddRange(GetComponentsInChildren<Rigidbody>());
    }

    public void ColorMatch(Color inputColor) // �ٸ� �Լ����� �����Ͽ� �� ��
    {
        if (ColorSystem.CompareColor(inputColor, targetColor) && !_isOpend)
        {
            linkedDoor.OpenDoor(); // ���� ���̶�� �� ��
            arrowUI?.FadeToDisable(); //ȭ��ǥ UI Fade�� Destroy
            SoundManager.Instance.PlaySFXSound("StoneFall");

            _dustParticles.ForEach(p => p.Play()); //���� ��ƼŬ �������ְ�

            _rigids.ForEach(v => //ü�ε� ����߸���
            {
                v.useGravity = true;
                Destroy(v.gameObject, 2f);
            });

            _isOpend = true;
        }
    }
}
