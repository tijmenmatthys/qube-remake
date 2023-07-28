using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static int Max(this List<int> list)
    {
        int max = list[0];
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i] > max) max = list[i];
        }
        return max;
    }
    public static int Min(this List<int> list)
    {
        int min = list[0];
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i] < min) min = list[i];
        }
        return min;
    }
}
