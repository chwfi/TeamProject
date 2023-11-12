using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("��ǥ ��")]
    private Color targetColor;

    [SerializeField]
    private ColorDoor linkedDoor; // ������ ��

    private List<ParticleSystem> _dustParticles = new List<ParticleSystem>();
    private List<Rigidbody> _rigids = new List<Rigidbody>();

    bool _isOpend = false;

    private void Awake()
    {
        _dustParticles.AddRange(GetComponentsInChildren<ParticleSystem>());
        _rigids.AddRange(GetComponentsInChildren<Rigidbody>());
    }

    public void ColorMatch(Color inputColor) // �ٸ� �Լ����� �����Ͽ� �� ��
    {
        if (ColorSystem.CompareColor(inputColor, targetColor) && !_isOpend)
        {
            linkedDoor.OpenDoor(); // ���� ���̶�� �� ��
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
