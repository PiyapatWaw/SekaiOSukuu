using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitControler : MonoBehaviour
{
    public static ExitControler Instanst;
    public Button exit;

    private void Awake()
    {
        if (Instanst != null)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
        Instanst = this;
    }

    public void Clck()
    {
        Application.Quit();
    }
}
