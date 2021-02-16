using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    [SerializeField]
    private ObjInpool BaseObject;
    public Queue<ObjInpool> Pool = new Queue<ObjInpool>();
    [SerializeField]
    private int StartSize;

    private void Awake()
    {
        for (int i = 0; i < StartSize; i++)
        {
            ObjInpool clone = Instantiate(BaseObject);
            Pool.Enqueue(clone);
            clone.gameObject.SetActive(false);
        }
    }

    public ObjInpool GetPool()
    {
        ObjInpool Select;
        if (Pool.Count > 0)
        {
            Select = Pool.Dequeue();
        }
        else
        {
            Select = Instantiate(BaseObject);
        }
        return Select;
    }   

    public void ReturnPool(ObjInpool Target)
    {
        Pool.Enqueue(Target);
        Target.gameObject.SetActive(false);
    }
}
