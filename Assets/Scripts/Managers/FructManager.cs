using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FructManager : MonoBehaviour
{
    private Dictionary<Fruct, Pool> fructPools;
    public Transform fructsContainer;

    [SerializeField]
    private List<FruitSpawner> spawnFruitPoints;

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
        Events.OnGameReset += OnReset;
        Events.OnGlobalGameStateEnter += OnGlobalGameStateEnter;
        Events.OnStartMixing += OnStartMixing;
    }
    private void Unsubscribe()
    {
        Events.OnGameReset -= OnReset;
        Events.OnGlobalGameStateEnter -= OnGlobalGameStateEnter;
        Events.OnStartMixing -= OnStartMixing;
    }

    private void OnStartMixing()
    {
        HideFruits();
    }
     private void OnGlobalGameStateEnter()
    {
        ShowFruits();
    }

    private void OnReset()
    {

    }

    private void HideFruits()
    {
        foreach(var spawner in spawnFruitPoints)
        {
            spawner.HideFruit();
        }
    }

    private void ShowFruits()
    {
        for (int i = 0; i < spawnFruitPoints.Count; i++)
        {
            if (LevelManger.GetLevelData().neededFruits.Count <= i)
            {
                SpawnedRandom(i);
                continue;
            }

            spawnFruitPoints[i].FruitSpawn(fructPools[LevelManger.GetLevelData().neededFruits[i].fruct]);            
        }
    }

    private void SpawnedRandom(int i)
    {
        int randIndex = UnityEngine.Random.Range(0, SettingsManager.settings.fruitVariants.Count - 1);
        spawnFruitPoints[i].FruitSpawn(fructPools[SettingsManager.settings.fruitVariants[randIndex]]);
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
