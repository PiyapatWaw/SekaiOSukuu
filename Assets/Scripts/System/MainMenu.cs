using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("DialogID", 0);
        PlayerPrefs.SetString("DialogNextScene", "BattleSystem");
        PlayerPrefs.SetInt("EnemyID", 0);
        PlayerPrefs.SetInt("ShowEndCredit", 0);
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetInt(string.Format("Party{0}", i + 1),i+1);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Dialog");
    }

    public void CreDit()
    {
        SceneManager.LoadScene("Credit");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
