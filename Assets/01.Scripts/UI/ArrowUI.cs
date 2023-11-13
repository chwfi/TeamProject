using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static Define.Define;

public class ArrowUI : LookCamUIModule, ICheckDistance //튜토리얼에서 크리스탈을 가리키는 화살표 UI 코드
{
    [SerializeField] private float _moveDis;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _ableDistance;

    private Image _arrowImage;

    private DistanceState _currentState;
    public DistanceState State
    {
        get => _currentState;
        set
        {
            _currentState = value;

            if (_currentState == DistanceState.Inside)
            {
                Fade(1);
            }
            else if (_currentState == DistanceState.Outside)
            {
                Fade(0);
            }
        }
    }

    protected override void Awake()
    {
        _arrowImage = GetComponent<Image>();

        MoveUpAndDown();
    }

    protected override void Update()
    {
        base.Update();
        CheckDistance();
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

    public void Fade(float value)
    {
        _arrowImage.DOFade(value, 0.5f);
    }

    public DistanceState CheckDistance()
    {
        if (Vector3.Distance(PlayerTrm.position, transform.position) < _ableDistance)
        {
            return State = DistanceState.Inside;
        }
        else
        {
            return State = DistanceState.Outside;
        }
    }
}
