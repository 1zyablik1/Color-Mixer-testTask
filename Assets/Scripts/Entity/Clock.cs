using UnityEngine;
using DG.Tweening;

public class Clock : MonoBehaviour
{
    [SerializeField] private Transform hourArrow;
    [SerializeField] private Transform minuteArrow;
    private Sequence clockSequence;

    void Start()
    {
        clockSequence = DOTween.Sequence();

        clockSequence.Append(hourArrow.transform.DORotate(new Vector3(0, 0, 360), 20, RotateMode.FastBeyond360)).SetLoops(-1).SetEase(Ease.Linear);
        clockSequence.Insert(0, minuteArrow.transform.DORotate(new Vector3(0, 0, 360), 100, RotateMode.FastBeyond360)).SetLoops(-1).SetEase(Ease.Linear);
    }
}
