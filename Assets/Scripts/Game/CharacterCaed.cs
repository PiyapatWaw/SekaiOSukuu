using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCaed : MonoBehaviour
{
    public bool IsSelect,CharDead,SetComplete;
    public Image Portrait,BG;
    public TextMeshProUGUI Action1;
    public TextMeshProUGUI Action2;
    public TextMeshProUGUI Desc;
    public Player CharacterData;

    private void Start()
    {
        string DeadKey = string.Format("Char{0}Status", CharacterData.ID);
        CharDead = PlayerPrefs.GetInt(DeadKey,1)==0;
        if (CharacterData)
        {
            Action1.text = CharacterData.actionDatas[0].Name;
            Action2.text = CharacterData.actionDatas[1].Name;
            Desc.text = CharacterData.Desc;
            Portrait.sprite = CharacterData.Portrait;
            Portrait.preserveAspect = true;
            if (CharacterData.IsMainCharacter)
            {
                IsSelect = true;
                BG.color = Color.yellow;
                SelectCharacter.Instanst.SelectID.Add(CharacterData.ID);
            }
            if(CharDead)
            {
                gameObject.SetActive(false);
                IsSelect = false;
                BG.color = Color.gray;
            }
        }
        SetComplete = true;

    }


    public void Click()
    {
        if ((SelectCharacter.Instanst.SelectID.Count == 3 && !SelectCharacter.Instanst.SelectID.Contains(CharacterData.ID) && !CharacterData.IsMainCharacter) 
            || CharacterData.IsMainCharacter || CharDead)
            return;
        IsSelect = !IsSelect;
        BG.color = IsSelect ? Color.blue : Color.white;
        SelectCharacter.Instanst.AfterCardClick(CharacterData.ID);
    }
}
