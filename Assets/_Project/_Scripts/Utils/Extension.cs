using UnityEngine;
using System.Collections.Generic;

public static class Extensions
{
    private static System.Random rng = new System.Random();

    public static void SetLayerRecursively(this GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (child == null)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public static void ClearAllChildren(this Transform transform)
    {
        while (transform.childCount != 0)
        {
            GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Mathf.FloorToInt(Random.Range(0, n));
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}