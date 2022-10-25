using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fruct : MonoBehaviour, IMouseInteractable
{
    public Vector3 spawnedScale;

    private Rigidbody rb;
    private Sequence fallOffSequence;

    private Sequence scaleSequence;
    private Sequence moveSequence;
    private Sequence throwAwaySequence;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
    }

    private void Unsubscribe()
    {
        Events.OnGameReset -= OnReset;
    }

    private void OnReset()
    {
        this.transform.rotation = Quaternion.Euler(Vector3.zero);

        this.transform.localScale = Vector3.zero;
        rb.isKinematic = true;   
    }

    public void Spawned()
    {
        scaleSequence = DOTween.Sequence();

        scaleSequence.Append(this.transform.DOScale(spawnedScale, SettingsManager.settings.timeScalingFruct));
    }

    public void Interact()
    {
        MoveAnim();
        Events.OnFruitClicked?.Invoke(this);
    }

    private void MoveAnim()
    {
        moveSequence = DOTween.Sequence();

        moveSequence.Append(this.transform.DOMove(Blender.GetEndFructPos(), SettingsManager.settings.timeMoveFruct));
        moveSequence.AppendCallback(FructEndMove);
    }

    private void FructEndMove()
    {
        scaleSequence = DOTween.Sequence();
        scaleSequence.Append(this.transform.DOScale(new Vector3(spawnedScale.x - 0.1f, spawnedScale.y - 0.1f, spawnedScale.z - 0.1f), SettingsManager.settings.decrisigSizeTime));

        Events.OnFruitAdd(this);
    }

    public void FallOff()
    {
        fallOffSequence = DOTween.Sequence();

        fallOffSequence.Append(this.transform.DOScale(Vector3.zero, SettingsManager.settings.decrisigSizeTime));

        this.gameObject.SetActive(false);
    }

    public void ThrowAway()
    {
        throwAwaySequence = DOTween.Sequence();

        Vector3 throwAwayPoint = Blender.GetEndFructPos() + new Vector3(1, 1, 0);

        throwAwaySequence.Append(this.transform.DOMove(throwAwayPoint, 0.5f));
        throwAwaySequence.AppendCallback(() => this.gameObject.SetActive(false));
    }
}
