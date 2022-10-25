using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    Fruct currentFruct;
    Pool fructPool;

    private void Awake()
    {
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        Events.OnFruitClicked += OnFruitClicked;
    }

    private void Unsubscribe()
    {
        Events.OnFruitClicked -= OnFruitClicked;
    }

    private void OnFruitClicked(Fruct fruct)
    {
        if(fruct == currentFruct)
        {
            Spawn();
        }
    }

    public void HideFruit()
    {
        currentFruct.FallOff();
    }

    public void FruitSpawn(Pool fructPool)
    {
        this.fructPool = fructPool;

        Spawn();
    }

    private void Spawn()
    {
        currentFruct = this.fructPool.GetFreeElement().GetComponent<Fruct>();
        currentFruct.transform.position = this.transform.position;

        currentFruct.Spawned();
    }
}
