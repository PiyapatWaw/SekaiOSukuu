using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuffTargetVariable
{
    none,
    atk
}
public enum BuffTarget
{
    me,
    Enemy,
    Allies
}
[System.Serializable]
public class Buffdata
{
    public string name;
    public int ID;
    public int value;
    public int turn;
    public BuffTargetVariable TargetVariable;
    public BuffTarget BuffTarget;

    public Buffdata(string name, int iD, int value, int turn, BuffTargetVariable variable = BuffTargetVariable.none , BuffTarget targettype = BuffTarget.me)
    {
        this.name = name;
        ID = iD;
        this.value = value;
        this.turn = turn;
        TargetVariable = variable;
        BuffTarget = targettype;
    }
}
