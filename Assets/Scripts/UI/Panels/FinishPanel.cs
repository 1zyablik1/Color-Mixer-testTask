using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishPanel : BaseUI
{
    [SerializeField] private TMP_Text textLose;
    [SerializeField] private TMP_Text textButton;
    [SerializeField] private CanvasGroup canvasGroup;

    private float timeElement = 1.5f;

    private Sequence fadeSequence;

    private void Awake()
    {
        ResetPanel();
    }

    private void OnEnable()
    {
        if(LevelManger.IsLevelWin())
        {
            textLose.text = "WIN";
            textButton.text = "NEXT";
        }
        else
        {
            textLose.text = "LOSE";
            textButton.text = "RESTART";
        }

        AnimatePanel();
    }

    private void OnDisable()
    {
        ResetPanel();
    }

    private void AnimatePanel()
    {
        fadeSequence = DOTween.Sequence();

        fadeSequence.Append(canvasGroup.DOFade(1, timeElement));
    }

    private void ResetPanel()
    {
        canvasGroup.alpha = 0;
    }

    public void ButtonClicked()
    {
        GlobalStateMachine.SetState<Menu>();
    }
}