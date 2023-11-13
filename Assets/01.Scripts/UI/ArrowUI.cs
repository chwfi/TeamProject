using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUI : LookCamUIModule //튜토리얼에서 크리스탈을 가리키는 화살표 UI 코드
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
            transform.DOMoveY(transform.position.y - _moveDis, _moveTime).OnComplete(() => MoveUpAndDown()); //재귀
        });
    }

    public void FadeToDisable()
    {
        _arrowImage.DOFade(0, 0.75f).OnComplete(() => Destroy(this.gameObject));
    }
}
