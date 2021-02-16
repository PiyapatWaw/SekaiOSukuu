using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPoolingSystem : MonoBehaviour
{
    public Pooling Pooling;

    IEnumerator Start()
    {
        while (Pooling.Pool.Count>0)
        {
            ObjInpool Select = Pooling.GetPool();
            Select.Show(Pooling,DosomeThing);
            Select.transform.position = new Vector3(Random.Range(1,50), Random.Range(1, 50), Random.Range(1, 50));
            yield return new WaitForSeconds(Random.Range(1,5));
        }
    }

    void DosomeThing()
    {
        Debug.Log("DosomeThing");
    }
}
