using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fruct : MonoBehaviour, IMouseInteractable
{
    public Vector3 startScale;

    private Rigidbody rb;
    private Sequence fallOffSequence;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        OnReset();
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
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
        this.transform.localScale = startScale;
        rb.isKinematic = true;   
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void FallOff()
    {
        fallOffSequence = DOTween.Sequence();

        fallOffSequence.Append(this.transform.DOScale(Vector3.zero, SettingsManager.settings.decrisigSizeTime));
    }
}
