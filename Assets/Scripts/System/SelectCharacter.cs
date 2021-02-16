using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class SelectCharacter : MonoBehaviour
{
    public static SelectCharacter Instanst;
    public List<int> SelectID = new List<int>();
    public List<CharacterCaed> AllcharCard = new List<CharacterCaed>();
    public Button GoBTN;

    private void Awake()
    {
        Instanst = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(()=> AllcharCard.Where(w => w.SetComplete).Count() == AllcharCard.Count);
        if(AllcharCard.Where(w => !w.CharDead).Count()<=3||AllcharCard[0].CharDead)
        {
            PlayerPrefs.SetInt("ShowEndCredit", 1);
            SceneManager.LoadScene("Credit");
        }
    }

    public void AfterCardClick(int id)
    {
        if(SelectID.Contains(id))
        {
            SelectID.Remove(id);
        }
        else
        {
            SelectID.Add(id);
        }

        GoBTN.interactable = SelectID.Count == 3;
    }

    public void Go()
    {
        for (int i = 0; i < SelectID.Count; i++)
        {
            string key = string.Format("Party{0}", i + 1);
            PlayerPrefs.SetInt(key, SelectID[i]);
        }
        SceneManager.LoadScene("BattleSystem");
    }
}
