using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjInpool : MonoBehaviour
{
    private Pooling Mypool;
    private Action Callback;
    public virtual void Show(Pooling pool,Action BeforeReturn = null)
    {
        gameObject.SetActive(true);
        Mypool = pool;
        if (BeforeReturn != null)
            Callback = BeforeReturn;
        StartCoroutine(WaitToReturn());
    }

    IEnumerator WaitToReturn()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(5,10));
        Callback?.Invoke();
        Mypool.ReturnPool(this);
    }
}
