using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManger : MonoBehaviour
{
    private const string levelValueName = "LevelValue";
    private const string levelFolderName = "Levels/levelSet.";
    
    private int levelValue = 1;
    private string levelName;

    private static Level levelData;

    private static bool levelWin = false;

    private void Awake()
    {
        levelValue = PlayerPrefs.GetInt(levelValueName, 1);
        Subsribe();
    }

    private void Start()
    {
        OnReset();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    public static Level GetLevelData()
    {
        return levelData;
    }

    private void Subsribe()
    {
        Events.OnGameReset += OnReset;
        Events.GameEnd += LevelFinished;
        Events.OnCoctailFinished += OnCoctailFinished;
    }
    private void Unsubscribe()
    {
        Events.OnGameReset -= OnReset;
        Events.GameEnd -= LevelFinished;
        Events.OnCoctailFinished -= OnCoctailFinished;
    }

    private void OnReset()
    {
        LoadLevel();
        levelWin = false;
    }

    private void IncreaseLevelValue()
    {
        levelValue++;
        PlayerPrefs.SetInt(levelValueName, levelValue);
    }

    private string GetLevelName(int levelNum)
    {
        levelName = levelFolderName + levelNum;
        return levelName;
    }

    private void LoadLevel()
    {
        int levelNum = levelValue;

        if(levelValue >= SettingsManager.settings.levelCountInSequence)
        {
            levelNum = levelValue - ((levelValue / SettingsManager.settings.levelCountInSequence) * SettingsManager.settings.levelCountInSequence);
        }
        if (levelNum == 0)
        {
            levelNum = SettingsManager.settings.levelCountInSequence;
        }

        levelData = Resources.Load<Level>(GetLevelName(levelNum));
    }

    public static bool IsLevelWin()
    {
        return levelWin;
    }

    private void LevelFinished(bool isLevelWin)
    {
        levelWin = isLevelWin;
        if(levelWin)
        {
            IncreaseLevelValue();
        }
    }

    private void OnCoctailFinished()
    {
        GlobalStateMachine.SetState<Finish>();
    }
}
