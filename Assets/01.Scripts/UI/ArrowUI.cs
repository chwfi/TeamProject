using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUI : LookCamUIModule //Ʃ�丮�󿡼� ũ����Ż�� ����Ű�� ȭ��ǥ UI �ڵ�
{
    [SerializeField] private float _moveDis;
    [SerializeField] private float _moveTime;

    private Image _arrowImage;

    protected override void Awake()
    {
        _arrowImage = GetComponent<Image>();

        MoveUpAndDown();
    }

    private void MoveUpAndDown()
    {
        transform.DOMoveY(transform.position.y + _moveDis, _moveTime).OnComplete(() =>
        {
            transform.DOMoveY(transform.position.y - _moveDis, _moveTime).OnComplete(() => MoveUpAndDown()); //���
        });
    }

    public void FadeToDisable()
    {
        _arrowImage.DOFade(0, 0.75f).OnComplete(() => Destroy(this.gameObject));
    }
}
