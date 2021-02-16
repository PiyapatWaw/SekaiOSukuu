using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ActionData/Heal")]
public class Heal : ActionData
{
    public override void DoAction(Character User, Character Target = null)
    {
        Target.Hp += Value;
        if (Target.Hp > Target.MaxHP)
            Target.MaxHP = Target.MaxHP;
    }
}
