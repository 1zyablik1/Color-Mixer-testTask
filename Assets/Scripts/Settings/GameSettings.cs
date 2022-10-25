using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "settings", menuName = "Settings/Game settings", order = 1)]
public class GameSettings : ScriptableObject
{
    public int levelCountInSequence;

    public float timeCustomerMove;
    public float timeCustomerrRotation;

    public Vector3 customerStartRotation;
    public Vector3 customerEndRotation;

    public List<Fruct> fruitVariants;
    public float decrisigSizeTime;

    public int maxItemInBlender;

    public float timeScalingFruct;
    public float timeMoveFruct;

    public Vector3 vectorForceFruct;
}