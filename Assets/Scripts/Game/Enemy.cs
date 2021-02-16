using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public List<int> ActionQue = new List<int>();

    private void Start()
    {
        MaxHP = Hp;
    }

    public override void DoAction(int index, Character Target = null)
    {
        Debug.Log(gameObject.name+ "Doaction" +Target);
        if(Hp>0)
        {
            base.DoAction(index, Target);
        }
        else
        {
            Hp = MaxHP;
        }
        
    }
}
