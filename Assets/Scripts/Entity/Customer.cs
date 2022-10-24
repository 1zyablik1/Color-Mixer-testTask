using UnityEngine;
using DG.Tweening;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    public RuntimeAnimatorController idle;
    public RuntimeAnimatorController walk;

    private Sequence rotationSequence;

    public void Move()
    {
        animator.runtimeAnimatorController = walk;
    }

    public void Rotate()
    {
        animator.runtimeAnimatorController = idle;

        rotationSequence = DOTween.Sequence();

        rotationSequence.Append(this.transform.DORotate(SettingsManager.settings.customerEndRotation, SettingsManager.settings.timeCustomerrRotation));
        rotationSequence.AppendCallback(MakeOrder);
    }

    private void MakeOrder()
    {

    }
}
