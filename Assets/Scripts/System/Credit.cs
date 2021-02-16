using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    public bool ShowEndCredit;
    public GameObject BtnBack;
    public CanvasGroup CreditGroup,Fade, TextGroup;
    IEnumerator Start()
    {
        ShowEndCredit = PlayerPrefs.GetInt("ShowEndCredit", 0) == 1;
        if(ShowEndCredit)
        {
            Time.timeScale = 1;
            BtnBack.SetActive(false);
            yield return new WaitForSeconds(10);
            float t = 0;
            while (t<1)
            {
                t += Time.deltaTime;
                Fade.alpha = t;
                CreditGroup.alpha = 1 - t;
                yield return null;
            }
            yield return new WaitForSeconds(2);
            t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                TextGroup.alpha = t;
                yield return null;
            }
            yield return new WaitForSeconds(2);
            BtnBack.SetActive(true);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

}
