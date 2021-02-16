using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class BattlePlayerInputUI : MonoBehaviour
{
    enum state
    {
        PartyChooseState,
        CharacterActionChooseState

    }
    [Header("Party Choose Panel")]
    public UIExtendComponent panelTurnFeedback;
    public UIExtendComponent partyChoosePanel;
    public List<CharacterButtonData> btnPartyList;
    [Header("Character Choose Panel")]
    public CharacterInputArea CharacterChoosePanel;
    public BattleGraphicUpdate battleGraphic;
    public GameSystemUpdateAction systemAction;


    public Action tweenTurnFeedbackDone;
    private void Awake()
    {
        //Register
        for (int i = 0; i < btnPartyList.Count; i++)
        {
            btnPartyList[i].ui.img.sprite = GameSystem.Instanst._playerParty[i].Portrait;
            btnPartyList[i].ui.img.preserveAspect = true;
            btnPartyList[i].playerIndex = i;
        }

        CharacterChoosePanel.backButton.button.onClick.AddListener(()=> BackButtonExecute(false));
        foreach (var item in CharacterChoosePanel.actionButton)
        {
            item.ui.button.onClick.AddListener(()=> BackButtonExecute(true));
            item.ui.pointerEvent.onPointerEnter.AddListener(() => UpdateDescription(item));
        }
        partyChoosePanel.gameObject.SetActive(true);
        CharacterChoosePanel.gameObject.SetActive(false);
    }


    public void ChooseThisCharacterButtonExecute(CharacterButtonData data)
    {
        //Register in inspector
        partyChoosePanel.gameObject.SetActive(false);
        CharacterChoosePanel.SetData(data.playerIndex);
        CharacterChoosePanel.characterImage.img.sprite = GameSystem.Instanst._playerParty[data.playerIndex].Portrait;
        CharacterChoosePanel.characterImage.img.preserveAspect = true;
        CharacterChoosePanel.gameObject.SetActive(true);
        CharacterChoosePanel.UpdateOrderText(data.playerIndex);
        battleGraphic.UpdateCurrentHeaderIconFeedback(data.playerIndex);

        GameSystem.Instanst.playerExecuteIndex = data.playerIndex;
    }

    public void BackButtonExecute(bool playerActionExecute)
    {
        partyChoosePanel.gameObject.SetActive(true);
        CharacterChoosePanel.gameObject.SetActive(false);
        TweenPartyChoosePanelIn(playerActionExecute);
        battleGraphic.UpdateCameraDefaultView();
        battleGraphic.HideAlltHeaderIconFeedback();

        if (!playerActionExecute)
        {
            battleGraphic.TweenAllCharacterToDefault();
        }
        else
        {
            GameSystem.Instanst.ExecutePlayerAction(systemAction.DoAction);
        }

    }

    public void UpdateDescription(CharacterButtonData data)
    {
        int index = data.playerIndex;
        GameSystem.Instanst.ButtonExecuteIndex = index;
        string desc = CharacterChoosePanel.Pdata.actionDatas[index].Desc;
        CharacterChoosePanel.ui.txt.text = desc;
    }


    public void UpdateTurnFeedbackText(bool isPlayer)
    {
        if (isPlayer)
        {
            panelTurnFeedback.txt.text = "Your Turn";
        }

        else
        {
            panelTurnFeedback.txt.text = "Enemy Turn";
        }
    }

    #region Tween
    private void TweenPartyChoosePanelIn(bool playerActionExecute)
    {
        float fadeDuration = 0.2f;

        SetPartyButtonInteractable(false);

        partyChoosePanel.canvasGroup.alpha = 0;
        partyChoosePanel.canvasGroup.DOFade(1, fadeDuration).OnComplete(() =>
        {
            if (!playerActionExecute)
            {
                SetPartyButtonInteractable(true);
            }

            else
            {
                TweenTurnFeedback();
            }
        });

        var panelOrigin = partyChoosePanel.rect.localPosition;
        Vector2 vecToPunchPanel = new Vector2(-100f, partyChoosePanel.rect.localPosition.y);
        partyChoosePanel.rect.DOLocalMove(vecToPunchPanel, fadeDuration).From().OnComplete(() =>
        {
            partyChoosePanel.rect.localPosition = panelOrigin;
        });
    }

    private void TweenTurnFeedback()
    {
        panelTurnFeedback.gameObject.SetActive(true);
        float duration = 0.2f;
        var panelOrigin = panelTurnFeedback.rect.localPosition;
        Vector2 vecToPunchPanel = new Vector2(100f, panelTurnFeedback.rect.localPosition.y);
        Vector2 vecToPunchPanelMinus = new Vector2(-200f, panelTurnFeedback.rect.localPosition.y);
        panelTurnFeedback.canvasGroup.alpha = 0;
        panelTurnFeedback.canvasGroup.DOFade(1, duration);
        panelTurnFeedback.rect.DOLocalMove(vecToPunchPanel, duration).From().OnComplete(()=> 
        {
            panelTurnFeedback.rect.localPosition = panelOrigin;
        });

        panelTurnFeedback.canvasGroup.DOFade(0, 0.25f).SetDelay(0.5f).OnComplete(()=> panelTurnFeedback.gameObject.SetActive(false));
        panelTurnFeedback.rect.DOLocalMove(vecToPunchPanelMinus, 0.25f).SetDelay(0.5f).OnComplete(() =>
        {
            panelTurnFeedback.rect.localPosition = panelOrigin;
            tweenTurnFeedbackDone?.Invoke();
        });
    }

    public void SetPartyButtonInteractable(bool setData)
    {
        foreach (var item in btnPartyList)
        {
            item.ui.button.interactable = setData;
        }
    }
    #endregion

}
