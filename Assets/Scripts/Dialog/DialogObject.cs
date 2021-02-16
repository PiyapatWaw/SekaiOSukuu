using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Dialog")]
public class DialogObject : ScriptableObject
{
    public int ID;
    public List<Dialog> AllDialog = new List<Dialog>();
}
[System.Serializable]
public class Dialog
{
    public Sprite Portrait,BG;
    [TextArea] 
    public string Text;
}
