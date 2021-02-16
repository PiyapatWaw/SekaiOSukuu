using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "ActionData/Attack")]
public class Attack : ActionData
{
    public override void DoAction(Character User, Character Target = null)
    {
        Debug.Log(User.name + "Attack");
        if (Target.AllbuffData.Where(w => w.name == "Guard").Count() > 0)
        {
            Buffdata targetbuff = Target.AllbuffData.Where(w => w.name == "Guard").FirstOrDefault();
            Target.AllbuffData.Remove(targetbuff);
        }
        else
        {
            Target.TakeDamae(Value);
        }
    }
}
