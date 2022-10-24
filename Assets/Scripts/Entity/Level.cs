using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "levelSet", menuName = "level/level setting", order = 11)]
public class Level : ScriptableObject
{
    public Color finalColor;
    public List<DictForFruct> neededFruits;
}

[System.Serializable]
public class DictForFruct
{
    public Fruct fruct;
    public int num;
}
