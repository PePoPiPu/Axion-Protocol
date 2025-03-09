using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public GameObject blueStraightBullet;
    public GameObject greenRoundBullet;
    public uint size;
    public bool shouldExpand = false;
    private List<GameObject> pool;
    // Start is called before the first frame update
    private void Start()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < size; i++)
        {
            AddGameObjectToPool();
        }
    }

    public GameObject GetAvailableGameObject()
    {
        foreach (GameObject o in pool)
        {
            if (!o.activeInHierarchy)
            {
                return o;
            }
        }

        if (shouldExpand)
        {
            return AddGameObjectToPool();
        }

        return null;
    }

    private GameObject AddGameObjectToPool()
    {
        GameObject o = Instantiate(blueStraightBullet);
        GameObject o2 = Instantiate(greenRoundBullet);
        pool.Add(o);
        pool.Add(o2);
        o.SetActive(false);
        o2.SetActive(false);
        return o;
    }
}
