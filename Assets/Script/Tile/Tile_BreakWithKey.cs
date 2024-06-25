using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_BreakWithKey : MonoBehaviour
{
    static Dictionary<int, List<Tile_BreakWithKey>> keyValuePairs = new Dictionary<int, List<Tile_BreakWithKey>>();

    public static void GetKey(int key)
    {
        Tile_BreakWithKey[] tile_BreakWithKeys = keyValuePairs[key].ToArray();

        for(int i = 0; i < tile_BreakWithKeys.Length; i++)
        {
            Destroy(tile_BreakWithKeys[i]);
        }
    }


    [SerializeField]
    int keyValue;
    public void Start()
    {
        if(!keyValuePairs.ContainsKey(keyValue)) keyValuePairs.Add(keyValue, new List<Tile_BreakWithKey>());

        keyValuePairs[keyValue].Add(this);
    }
}
