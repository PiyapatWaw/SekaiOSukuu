using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundStorage : MonoBehaviour
{
    public static SoundStorage Instanst;
    public List<AudioClip> AllSound = new List<AudioClip>();

    private void Awake()
    {
        if(Instanst!=null)
        {
            Destroy(gameObject);
        }

        Instanst = this;
    }

    public AudioClip GetSound(string name)
    {
        return AllSound.Where(w => w.name == name).FirstOrDefault();
    }
}
