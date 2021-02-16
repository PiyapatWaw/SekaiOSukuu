using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HpScript : MonoBehaviour
{
    public TextMeshProUGUI currHP;
    public TextMeshProUGUI maxHP;
    public TextMeshProUGUI Name;
    public Character Character;

    public void Setup(Character mychar)
    {
        maxHP.text = mychar.Hp.ToString();
        Character = mychar;
        Name.text = Character.Name.ToString();
    }

    private void Update()
    {
        currHP.text = Character.Hp.ToString();
    }

    public void UpdateHP(int input)
    {
        currHP.text = input.ToString();
    }
}
