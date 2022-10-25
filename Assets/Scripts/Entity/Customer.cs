using UnityEngine;
using DG.Tweening;
using System;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public GameObject buble;
    public SpriteRenderer liquid;

    [SerializeField]
    public RuntimeAnimatorController idle;
    public RuntimeAnimatorController walk;

    private Sequence rotationSequence;

    private void Awake()
    {
        OnReset();
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }


    private void Subscribe()
    {
        Events.OnGameReset += OnReset;
        Events.OnCoctailFinished += OnCoctailFinished;
    }

    private void Unsubscribe()
    {
        Events.OnGameReset += OnReset;
        Events.OnCoctailFinished -= OnCoctailFinished;
    }

    private void OnReset()
    {
        buble.SetActive(false);
    }

    private void OnCoctailFinished()
    {

    }

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
        liquid.color = LevelManger.GetLevelData().finalColor;
        buble.SetActive(true);
    }
}
