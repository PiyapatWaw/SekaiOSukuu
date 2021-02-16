using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public Image Tips;
    public GameObject PauseGroup;

    public void PauseUnpause(bool pause)
    {
        PauseGroup.SetActive(pause);
        Time.timeScale = pause ? 0 : 1;
    }

    public void ShowHideTips(bool show)
    {
        Tips.gameObject.SetActive(show);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
