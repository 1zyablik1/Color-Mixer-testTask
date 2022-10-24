using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blender : MonoBehaviour
{
    private int fruitCount;
    private Sequence shakingSequence;

    [SerializeField]
    private GameObject liquid;
    [SerializeField]
    private GameObject Cap;

    private List<Fruct> fructsInBlender;

    private void Awake()
    {
        fructsInBlender = new List<Fruct>();
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        Events.OnGameReset += OnReset;
        Events.OnFruitAdd += FruitAdd;
    }

    private void Unsubscribe()  
    {
        Events.OnGameReset -= OnReset;
        Events.OnFruitAdd -= FruitAdd;
    }

    private void FruitAdd(Fruct fruct)
    {
        if(fruitCount > SettingsManager.settings.maxItemInBlender)
        {

            return;
        }
        shakingSequence = DOTween.Sequence();
        shakingSequence.Append(this.transform.DOShakeRotation(0.1f));

        fruitCount++;
        fructsInBlender.Add(fruct);

        fruct.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnReset()
    {
        fruitCount = 0;
        fructsInBlender.Clear();
    }    

    public void Mix()
    {
        foreach(var fruct in fructsInBlender)
        {
            fruct.FallOff();
        }
    }
}
