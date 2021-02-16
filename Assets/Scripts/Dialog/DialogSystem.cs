using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public List<DialogObject> dialogObjects = new List<DialogObject>();
    public Image Portrait,BG;
    public TextMeshProUGUI DialogText;
    public string NextSceneName;
    public int ID;
    bool Gonext;
    DialogObject SelecDialog;


    void Awake()
    {
        Gonext = false;
        ID = PlayerPrefs.GetInt("DialogID",0);
        NextSceneName = PlayerPrefs.GetString("DialogNextScene","Select");
        SelecDialog = dialogObjects.Where(w=> ID == w.ID).FirstOrDefault();
        StartCoroutine(RunDialog());
    }

    IEnumerator RunDialog()
    {
        foreach (var item in SelecDialog.AllDialog)
        {
            Portrait.sprite = item.Portrait ? item.Portrait : Portrait.sprite;
            Portrait.SetNativeSize();
            BG.sprite = item.BG ? item.BG : BG.sprite;
            //BG.SetNativeSize();
            DialogText.text = item.Text;
            yield return new WaitUntil(()=> Gonext);
            Gonext = false;
        }
        ID++;
        PlayerPrefs.SetInt("DialogID", ID);
        SceneManager.LoadScene(NextSceneName);
    }

    public void NextText()
    {
        Gonext = true;
    }


}
