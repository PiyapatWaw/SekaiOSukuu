using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterInputArea : MonoBehaviour
{
    public UIExtendComponent backButton;
    public UIExtendComponent ui;
    public UIExtendComponent characterImage;
    public UIExtendComponent AreaContainer;
    public Player Pdata;
    public List<CharacterButtonData> actionButton = new List<CharacterButtonData>();
    public UIExtendComponent actionDescription;

    #region UpdateUI
    public void SetData(int index)
    {
        Pdata = GameSystem.Instanst._playerParty[index];
        for (int i = 0; i < actionButton.Count; i++)
        {
            actionButton[i].ui.txt.text = Pdata.actionDatas[i].Name;
        }
    }

    public void UpdateOrderText(int playerIndex)
    {
        actionButton[0].graphicUpdate.UpdateFollowCamera(playerIndex);
        switch (playerIndex)
        {
            case 0:
                characterImage.txt.text = playerIndex+1 + "st";
                break;
            case 1:
                characterImage.txt.text = playerIndex+1 + "nd";
                break;
            case 2:
                characterImage.txt.text = playerIndex+1 + "rd";
                break;
            default:
                characterImage.txt.text = playerIndex+1 + "st";
                break;
        }

        TweenPanelIn(playerIndex);
     
    }



    #endregion

    #region Tween

    private void TweenPanelIn(int playerIndex)
    {
        float fadeDuration = 0.2f;

        backButton.button.interactable = false;
        actionButton[0].ui.button.interactable = false;
        actionButton[1].ui.button.interactable = false;

        backButton.canvasGroup.alpha = 0;
        AreaContainer.canvasGroup.alpha = 0;



        var backButtonOrigin = backButton.rect.localPosition;
        var AreaContainerOrigin = AreaContainer.rect.localPosition;
        var characterImageOrigin = characterImage.rect.localPosition;

        Vector2 vecToPunchButton = new Vector2(0, backButton.rect.localPosition.y);
        Vector2 vecToPunchAreaContainerPanel = new Vector2(0, AreaContainer.rect.localPosition.y);

        float charImgPosTween = 0;
        switch (playerIndex)
        {
            case 0:
                charImgPosTween = -250;
                break;
            case 1:
                charImgPosTween = -150;
                break;
            case 2:
                charImgPosTween = 0;
                break;
            default:
                charImgPosTween = -250;
                break;
        }  
        Vector2 vecToPunchcharacterImage = new Vector2(charImgPosTween, characterImage.rect.localPosition.y);

        characterImage.rect.DOLocalMove(vecToPunchcharacterImage, fadeDuration).From().OnComplete(() =>
        {
            characterImage.rect.localPosition = characterImageOrigin;

            backButton.canvasGroup.DOFade(1, fadeDuration);
            AreaContainer.canvasGroup.DOFade(1, fadeDuration);

            backButton.rect.DOLocalMove(vecToPunchButton, fadeDuration).From().OnComplete(() =>
            {
                backButton.button.interactable = true;
                backButton.rect.localPosition = backButtonOrigin;
            });

            AreaContainer.rect.DOLocalMove(vecToPunchAreaContainerPanel, fadeDuration).From().OnComplete(() =>
            {
                actionButton[0].ui.button.interactable = true;
                actionButton[1].ui.button.interactable = true;
                AreaContainer.rect.localPosition = AreaContainerOrigin;
            });
        });


        
    }

    #endregion

}
