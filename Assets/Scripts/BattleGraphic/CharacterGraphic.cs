using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGraphic : MonoBehaviour
{
    public GameObject graphicNode;
    public GameObject feedbackCurrentChosenObj;
    public Transform cameraFocusPoint;
    public Animator anim;

    public Vector3 originPos;

    private void Awake()
    {
        originPos = this.transform.position;

    }

    public void ActiveCurrentChosen(bool data)
    {
        feedbackCurrentChosenObj.SetActive(data);
    }
}
