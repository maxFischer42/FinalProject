using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public enum itemType {Head,Cloak,Tunic,Accessory,Weapon};

    [Header("CurrentEquipment")]
    public Items headItem;
    public Items cloakItem;
    public Items tunicItem;
    public Items accessoryItem;
    public Items weaponItem;

    [Header("Sub-Inventories")]
    public Items[] headItems;
    public Items[] cloakItems;
    public Items[] tunicItems;
    public Items[] accessoryItems;
    public Items[] weaponItems;

    [Header("UI Elements")]
    public Dropdown headDropDown;
    public Dropdown cloakDropDown;
    public Dropdown tunicDropDown;
    public Dropdown accessoryDropDown;
    public Dropdown weaponDropDown;

    private ItemIndex index;

    public void Start()
    {
        index = GetComponent<ItemIndex>();
        AddItem(itemType.Head, 4);
        AddItem(itemType.Head, 5);
        AddItem(itemType.Tunic, 2);
        AddItem(itemType.Tunic, 2);
    }



    public void changeEquipment(Items i)
    {
        Debug.Log("swapping equipment");
        switch (i.itemType)
        {
            case Items.type.Head:
                headItem = i;
                break;
            case Items.type.Cloak:
                cloakItem = i;
                break;
            case Items.type.Tunic:
                tunicItem = i;
                break;
            case Items.type.Accessory:
                accessoryItem = i;
                break;
            case Items.type.Weapon:
                weaponItem = i;
                break;
        }
    }

    public Items GetEquipment(Items i)
    {

        return null;
    }

    Dropdown GetDropdowns(itemType i)
    {
        switch(i)
        {
            case itemType.Head:
                return headDropDown;
            case itemType.Cloak:
                return cloakDropDown;
            case itemType.Tunic:
                return tunicDropDown;
            case itemType.Accessory:
                return accessoryDropDown;
            case itemType.Weapon:
                return weaponDropDown;
        }
        return null;
    }
    Items[] GetItems(itemType i)
    {
        switch (i)
        {
            case itemType.Head:
                return headItems;
            case itemType.Cloak:
                return cloakItems;
            case itemType.Tunic:
                return tunicItems;
            case itemType.Accessory:
                return accessoryItems;
            case itemType.Weapon:
                return weaponItems;
        }
        return null;
    }

    public void AddItem(itemType type, int indexID)
    {
        Add(indexID, type);
    }
    public void setOptions(itemType itemType)
    {
        Dropdown dropdown = GetDropdowns(itemType);
        dropdown.ClearOptions();
        Items[] itemArray = GetItems(itemType);
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        for (int i = 0; i < itemArray.Length; i++)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(itemArray[i].m_name, itemArray[i].icon);
            optionDatas.Add(optionData);
            Debug.Log(i + "th loop");
        }
        dropdown.AddOptions(optionDatas);
    }
    public void Add(int ID, itemType type)
    {
        Items item;
        index.itemIndex.TryGetValue(ID, out item);
        Debug.Log("Adding item " + item.name + " to inventory");
        Items[] itemArray = GetItems(type);
        Items[] backup = itemArray;
        itemArray = new Items[itemArray.Length + 1];
        if (backup.Length != 0)
        {
            Debug.Log("backup length is not 0");
            for (int i = 0; i < backup.Length; i++)
            {
                itemArray[i] = backup[i];
                Debug.Log("Re-adding item " + itemArray[i] + " into new array");
            }
            itemArray[itemArray.Length - 1] = item;
        }
        else
        {
            Debug.Log("backup length is 0");
            itemArray[0] = item;
        }
        ChangeArray(type, itemArray);
        setOptions(type);
    }

    void ChangeArray(itemType i, Items[] items)
    {
        switch (i)
        {
            case itemType.Head:
                headItems = items;
                break;
            case itemType.Cloak:
                cloakItems = items;
                break;
            case itemType.Tunic:
                tunicItems = items;
                break;
            case itemType.Accessory:
                accessoryItems = items;
                break;
            case itemType.Weapon:
                weaponItems = items;
                break;
        }
    }
}
