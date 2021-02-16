using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuffAndGuardPanelUI : MonoBehaviour
{
    public List<Player> playerInstance = new List<Player>();
    public List<CharacterButtonData> button = new List<CharacterButtonData>();
    public UIExtendComponent btnBack;
    public UIExtendComponent panelUI;

    public Action executeAction;

    private void Awake()
    {
        btnBack.button.onClick.AddListener(Back);
        playerInstance = GameSystem.Instanst._playerParty;
        int i = 0;
        foreach (var item in button)
        {
            item.ui.img.sprite = playerInstance[i].Portrait;
            item.ui.img.preserveAspect = true;
            item.ui.button.onClick.AddListener(() =>
            {
                GuardBuffButtonExecute(item.playerIndex);
            });
            i++;
        }
        gameObject.SetActive(false);
    }


    private void Back()
    {
        this.gameObject.SetActive(false);
    }

    public void TweenIn()
    {
        this.gameObject.SetActive(true);
        float tweenDuration = 0.15f;
        Vector2 vecPunch = new Vector2(100, 0);
        foreach (var item in button)
        {
            item.ui.button.interactable = false;
        }
        btnBack.button.interactable = false;

        panelUI.canvasGroup.alpha = 0;
        panelUI.canvasGroup.DOFade(1, tweenDuration);
        panelUI.rect.DOPunchPosition(vecPunch, tweenDuration).OnComplete(()=> 
        {
            foreach (var item in button)
            {
                item.ui.button.interactable = true;
            }
            btnBack.button.interactable = true;
        });
    }

    public void GuardBuffButtonExecute(int i)
    {
        GameSystem.Instanst.SelectTarget = i;
        this.gameObject.SetActive(false);
        executeAction?.Invoke();
    }

    public void RegiserAction(Action playerAction,bool exeuteNow = false)
    {
        executeAction = playerAction;
        if(exeuteNow)
            executeAction?.Invoke();
    }
}
