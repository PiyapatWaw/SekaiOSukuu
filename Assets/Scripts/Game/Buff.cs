using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ActionData/Buff")]
public class Buff : ActionData
{
    public int turn;
    public int ID;
    public BuffTargetVariable BuffTargetVariable;
    public BuffTarget TargetType;

    public override void DoAction(Character User, Character Target = null)
    {
        Debug.Log(User.Name + " buff to " + Target.Name);
        Buffdata buff = new Buffdata("Buff", ID, Value, turn, BuffTargetVariable, TargetType);
        Target.ApplyBuff(buff);
    }
}
