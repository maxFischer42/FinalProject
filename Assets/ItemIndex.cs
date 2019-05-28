using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndex : MonoBehaviour
{
    public Dictionary<int, Items> itemIndex = new Dictionary<int, Items>();
    public ItemList[] list;

    void Start()
    {
        for(int i = 0; i < list.Length - 1; i++)
        {
            itemIndex.Add(list[i].ID, list[i].item);
        }
    }

}

[System.Serializable]
public class ItemList
{
    public int ID;
    public Items item;
}
