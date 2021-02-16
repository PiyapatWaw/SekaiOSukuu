using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string Name;
    public int Hp,MaxHP,ID;
    public List<ActionData> actionDatas = new List<ActionData>();
    public List<Buffdata> AllbuffData = new List<Buffdata>();
    public CharacterState CharacterState;
    public CharacterGraphic CharacterGraphic;
    public virtual void TakeDamae(int value)
    {
        Debug.Log(name + " takedamage " + value);
        Hp -= value;
        Debug.Log(name + " hp is " + Hp);
    }

    public virtual void DoAction(int index,Character Target = null)
    {
        Debug.Log("DoAction "+ actionDatas[index].name);
        actionDatas[index].DoAction(this,Target);
    }


    public virtual void ApplyBuff(Buffdata buffdata)
    {
        Debug.Log("ApplyBuff");
        AllbuffData.Add(buffdata);
        Debug.Log(gameObject.name+" have buff " + AllbuffData.Count );
        if (buffdata.TargetVariable == BuffTargetVariable.atk)
        {
            foreach (var item in actionDatas)
            {
                if(item is Attack)
                {
                    item.Value += buffdata.value;
                }
            }
        }
    }

    public virtual void DecressBuffTurn()
    {
        foreach (var item in AllbuffData)
        {
            item.turn--;
            if(item.turn==0)
            {
                int buffcount = RemoveBuff(item);
                if (buffcount <= 0)
                    return;
            }
        }
    }

    public virtual int RemoveBuff(Buffdata buff)
    {
        if (buff.TargetVariable == BuffTargetVariable.atk)
        {
            foreach (var item in actionDatas)
            {
                if (item is Attack)
                {
                    item.Value -= buff.value;
                    AllbuffData.Remove(buff);
                }
            }
        }
        return AllbuffData.Count;
    }
}
