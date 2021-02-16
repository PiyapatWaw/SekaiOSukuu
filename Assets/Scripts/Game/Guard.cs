using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ActionData/Guard")]
public class Guard : ActionData
{
    public override void DoAction(Character User, Character Target = null)
    {
        Buffdata guardbuff = new Buffdata("Guard",1,0,1);
        Target.ApplyBuff(guardbuff);
    }
}
