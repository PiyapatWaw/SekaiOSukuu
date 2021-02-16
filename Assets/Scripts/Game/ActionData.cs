using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ActionType
{
    Attack,
    Heal,
    Guard,
    Buff,
    Debuff
}

[System.Serializable]
[CreateAssetMenu(menuName = "ActionData/Data")]
public class ActionData : ScriptableObject
{
    public string Name,Desc;
    public Sprite Icon;
    public int Value;
    public virtual void DoAction(Character User,Character Target = null)
    {

    }
}
