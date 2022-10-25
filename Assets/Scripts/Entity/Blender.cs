using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blender : MonoBehaviour
{
    private float percent = 0;
    private int correctFructs;
    private int fruitCount;
    private Sequence shakingSequence;
    private Sequence capSequence;
    private Sequence liqSequence;

    [SerializeField]
    private GameObject liquid;
    [SerializeField]
    private GameObject cap;
    [SerializeField]
    private Renderer rendererMP;

    public GameObject endFructPos;
    public bool isInteractable = false;

    private MaterialPropertyBlock propertyBlock;

    private Vector3 defPosCap;
    public Vector3 capOffset;

    private static Vector3 staticPos;
    private List<Fruct> fructsInBlender;

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();

        staticPos = endFructPos.transform.position;
        fructsInBlender = new List<Fruct>();

        defPosCap = cap.transform.position;

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
        Events.OnFruitAdd += FruitAdd;
        Events.OnFruitClicked += OnFructClicked;
        Events.OnGlobalGameStateEnter += OnGlobalGameStateEnter;
    }

    private void Unsubscribe()  
    {
        Events.OnGameReset -= OnReset;
        Events.OnFruitAdd -= FruitAdd;
        Events.OnFruitClicked -= OnFructClicked;
        Events.OnGlobalGameStateEnter += OnGlobalGameStateEnter;
    }

    private void OnGlobalGameStateEnter()
    {
        isInteractable = true;
    }

    private void OnFructClicked(Fruct obj)
    {
        DOTween.Kill(cap.transform);

        capSequence = DOTween.Sequence();

        capSequence.Append(cap.transform.DOMove(defPosCap + capOffset, 0.1f));
        capSequence.AppendInterval(0.2f);
        capSequence.Append(cap.transform.DOMove(defPosCap, 0.1f));
    }

    public static Vector3 GetEndFructPos()
    {
        return staticPos;
    }

    private void FruitAdd(Fruct fruct)
    {
        if(fruitCount > SettingsManager.settings.maxItemInBlender)
        {
            fruct.ThrowAway();
            return;
        }
        shakingSequence = DOTween.Sequence();
        shakingSequence.Append(this.transform.DOShakeRotation(0.1f, 5));

        fruitCount++;
        fructsInBlender.Add(fruct);

        fruct.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnReset()
    {
        percent = 0;
        correctFructs = 0;
        isInteractable = false;
        fruitCount = 0;
        fructsInBlender.Clear();

        rendererMP.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_Fill", 0);
        rendererMP.SetPropertyBlock(propertyBlock);
    }     

    public void Mix()
    {
        isInteractable = false;

        rendererMP.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor("_LiquidColor", LevelManger.GetLevelData().finalColor);
        propertyBlock.SetColor("_SurfaceColor", LevelManger.GetLevelData().darkerColor);
        rendererMP.SetPropertyBlock(propertyBlock);

        CheckCoctail();


        if (percent > 85)
        {
            Events.GameEnd?.Invoke(true);
        }
        else
        {
            Events.GameEnd?.Invoke(false); rendererMP.GetPropertyBlock(propertyBlock);

            Color32 color = new Color(
                 UnityEngine.Random.Range(0f, 1f),
                 UnityEngine.Random.Range(0f, 1f),
                 UnityEngine.Random.Range(0f, 1f)
              );

            propertyBlock.SetColor("_LiquidColor", color);
            propertyBlock.SetColor("_SurfaceColor", color);
            rendererMP.SetPropertyBlock(propertyBlock);
        }

        shakingSequence = DOTween.Sequence();
        shakingSequence.Append(this.transform.DOShakeRotation(3f, 6));
        shakingSequence.AppendCallback(CoctailDone);

        float progress = 0;

        liqSequence = DOTween.Sequence();
        liqSequence.Append(
            DOTween.To(() => progress, x => progress = x, 1, 3)
                .OnUpdate(() => {
                    rendererMP.GetPropertyBlock(propertyBlock);
                    propertyBlock.SetFloat("_Fill", progress);
                    rendererMP.SetPropertyBlock(propertyBlock);
                }));

        foreach (var fruct in fructsInBlender)
        {
            fruct.FallOff();
        }
    }

    private void CheckCoctail()
    {
        List<Fruct> fructs = new List<Fruct>();
        
        foreach (var fruct in LevelManger.GetLevelData().neededFruits)
        {
            for(int i = 0; i < fruct.num; i++)
            {
                fructs.Add(fruct.fruct);
            }
        }

        if (fructsInBlender.Count == 0)
        {
            percent = 0;
            return;
        }

        foreach (var fruct in fructsInBlender)
        {
            bool finded = false;

            for (int i = 0; i < fructs.Count; i++)
            {

                if(fruct.GetType() == fructs[i].GetType())
                {
                    finded = true;
                    correctFructs++;
                    fructs.RemoveAt(i);
                    break;
                }
            }

            if(!finded)
            {
                correctFructs--;
            }
        }

        percent = 100.0f / fructsInBlender.Count * correctFructs;
    }

    public void CoctailDone()
    {
        Events.OnCoctailFinished?.Invoke();
    }
}
