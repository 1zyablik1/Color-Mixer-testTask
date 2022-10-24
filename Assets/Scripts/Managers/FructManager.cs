using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FructManager : MonoBehaviour
{
    private Dictionary<Fruct, Pool> fructPools;
    public Transform fructsContainer;
    
    [SerializeField] 
    private List<Transform> spawnFruitPoints;

    private void Awake()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        Events.OnGameReset += OnReset;
        Events.OnGlobalGameStateEnter += OnGlobalGameStateEnter;
        Events.OnStartMixing += OnStartMixing;
    }

    private void OnStartMixing()
    {
        //HideFruits();
    }

    private void OnGlobalGameStateEnter()
    {
        //ShowFruits();
    }

    private void OnReset()
    {
        
    }

    private void Start()
    {
        CreateFruitPool();
    }

    private void CreateFruitPool()
    {
        fructPools = new Dictionary<Fruct, Pool>();

        foreach(var fruct in SettingsManager.settings.fruitVariants)
        {
            var fructContainer = new GameObject($"{fruct.name}.container");
            fructContainer.transform.SetParent(fructsContainer);

            fructPools.Add(fruct, new Pool(fruct.gameObject, fructContainer));
        }
    }


}
