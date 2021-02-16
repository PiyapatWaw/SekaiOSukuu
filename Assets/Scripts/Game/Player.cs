using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Character
{
    [Header ("Maincharcter")]
    #region Maincharcter
    public bool IsMainCharacter;
    public RelationshipData MyRelationship;
    private string RelationshipKey = "Relationship";
    #endregion
    public bool IsDead;
    public string Desc;
    public Sprite Portrait;
    //[Header("Character Setting")]
    
        

    private void Start()
    {
        MaxHP = Hp;
        if(IsMainCharacter)
        {
            if (PlayerPrefs.HasKey(RelationshipKey))
            { 
                string json = PlayerPrefs.GetString(RelationshipKey);
                MyRelationship = JsonUtility.FromJson<RelationshipData>(json);
            }
            else
            {
                MyRelationship.Data = new int[7];
                for (int i = 0; i < MyRelationship.Data.Length; i++)
                {
                    MyRelationship.Data[i] = 0;
                }
                string json = JsonUtility.ToJson(MyRelationship);
                PlayerPrefs.SetString(RelationshipKey,json);
            }

            //DoAction(0,this);
        }
    }


    public string GetRelationshipKey ()=> RelationshipKey;

    public override void TakeDamae(int value)
    {
        base.TakeDamae(value);
        if(Hp<=0)
        {
            StepDie();
        }
    }

    void StepDie()
    {
        IsDead = true;
        string DeadKey = string.Format("Char{0}Status",ID);
        PlayerPrefs.SetInt(DeadKey,0);
    }

}
[System.Serializable]
public class RelationshipData
{
    public int[] Data;
}
