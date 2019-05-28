using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public enum itemType {Head,Cloak,Tunic,Accessory,Weapon};

    [Header("Sub-Inventories")]
    public Items[] headItems;
    public Items[] cloakItems;
    public Items[] tunicItems;
    public Items[] accessoryItems;
    public Items[] weaponItems;

    private ItemIndex index;

    public void Start()
    {
        index = GetComponent<ItemIndex>();
    }

    public void AddItem(Items item, itemType type, int indexID)
    {
        switch(type)
        {
            case itemType.Head:
                AddHead(indexID);
                break;
            case itemType.Cloak:
                AddCloak(indexID);
                break;
            case itemType.Tunic:
                AddTunic(indexID);
                break;
            case itemType.Accessory:
                AddAccessory(indexID);
                break;
            case itemType.Weapon:
                AddWeapon(indexID);
                break;
        }
    }


    public void AddHead(int ID)
    {
        Items item;
        index.itemIndex.TryGetValue(ID, out item);
        Items[] backup = headItems;
        headItems = new Items[headItems.Length + 1];
        for(int i = 0; i < backup.Length - 1; i++)
        {
            headItems[i] = backup[i];
        }
        headItems[headItems.Length - 1] = item;
    }


}
