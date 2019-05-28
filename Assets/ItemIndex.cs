using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndex : MonoBehaviour
{
    public Dictionary<int, Items> itemIndex = new Dictionary<int, Items>();
    public ItemList[] list;
    public SpriteList[] imageList;
    public Dictionary<Sprite, Items> imageIndex = new Dictionary<Sprite, Items>();
    void Start()
    {
        for(int i = 0; i < list.Length; i++)
        {
            itemIndex.Add(list[i].ID, list[i].item);
        }

        for (int i = 0; i < imageList.Length; i++)
        {
            imageIndex.Add(imageList[i].icon, imageList[i].item);
        }
    }

}

[System.Serializable]
public class ItemList
{
    public int ID;
    public Items item;
}

[System.Serializable]
public class SpriteList
{
    public Items item;
    public Sprite icon;
}
