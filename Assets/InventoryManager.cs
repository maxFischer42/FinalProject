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
    }

    public void AddItem(itemType type, int indexID)
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

    public void Update()
    {
        
    }

    public void setHeadOptions()
    {
        headDropDown.ClearOptions();
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        for(int i = 0; i < headItems.Length; i++)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(headItems[i].m_name, headItems[i].icon);
            optionDatas.Add(optionData);            
        }
        headDropDown.AddOptions(optionDatas);
    }

    public void setCloakOptions()
    {
        cloakDropDown.ClearOptions();
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        for (int i = 0; i < cloakItems.Length; i++)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(cloakItems[i].m_name, cloakItems[i].icon);
            optionDatas.Add(optionData);
        }
        cloakDropDown.AddOptions(optionDatas);
    }

    public void setTunicOptions()
    {
        tunicDropDown.ClearOptions();
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        for (int i = 0; i < tunicItems.Length; i++)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(tunicItems[i].m_name, tunicItems[i].icon);
            optionDatas.Add(optionData);
        }
        tunicDropDown.AddOptions(optionDatas);
    }

    public void setAccessoryOptions()
    {
        accessoryDropDown.ClearOptions();
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        for (int i = 0; i < accessoryItems.Length; i++)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(accessoryItems[i].m_name, accessoryItems[i].icon);
            optionDatas.Add(optionData);
        }
        accessoryDropDown.AddOptions(optionDatas);
    }

    public void setWeaponOptions()
    {
        weaponDropDown.ClearOptions();
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        for (int i = 0; i < weaponItems.Length; i++)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(weaponItems[i].m_name, weaponItems[i].icon);
            optionDatas.Add(optionData);
        }
        weaponDropDown.AddOptions(optionDatas);
    }

    public void AddHead(int ID)
    {
        Items item;
        index.itemIndex.TryGetValue(ID, out item);
        Debug.Log("Adding item " + item.name + " to inventory");
        Items[] backup = headItems;
        headItems = new Items[headItems.Length + 1];
        if(backup.Length != 0)
        {
            Debug.Log("backup length is not 0");
            for(int i = 0; i < backup.Length; i++)
            {
                headItems[i] = backup[i];
                Debug.Log("Re-adding item " + headItems[i] + " into new array");
            }
            headItems[headItems.Length - 1] = item;
        }
        else
        {
            Debug.Log("backup length is 0");
            headItems[0] = item;
        }        
        setHeadOptions();
    }

    public void AddCloak(int ID)
    {
        Items item;
        index.itemIndex.TryGetValue(ID, out item);
        Items[] backup = cloakItems;
        cloakItems = new Items[cloakItems.Length + 1];
        if (backup.Length != 0)
        {
            Debug.Log("backup length is not 0");
            for (int i = 0; i < backup.Length; i++)
            {
                cloakItems[i] = backup[i];
                Debug.Log("Re-adding item " + cloakItems[i] + " into new array");
            }
            cloakItems[cloakItems.Length - 1] = item;
        }
        else
        {
            Debug.Log("backup length is 0");
            cloakItems[0] = item;
        }
        setCloakOptions();
    }

    public void AddTunic(int ID)
    {
        Items item;
        index.itemIndex.TryGetValue(ID, out item);
        Items[] backup = tunicItems;
        tunicItems = new Items[tunicItems.Length + 1];
        if (backup.Length != 0)
        {
            Debug.Log("backup length is not 0");
            for (int i = 0; i < backup.Length; i++)
            {
                tunicItems[i] = backup[i];
                Debug.Log("Re-adding item " + tunicItems[i] + " into new array");
            }
            tunicItems[tunicItems.Length - 1] = item;
        }
        else
        {
            Debug.Log("backup length is 0");
            tunicItems[0] = item;
        }
        setTunicOptions();
    }

    public void AddAccessory(int ID)
    {
        Items item;
        index.itemIndex.TryGetValue(ID, out item);
        Items[] backup = accessoryItems;
        accessoryItems = new Items[accessoryItems.Length + 1];
        if (backup.Length != 0)
        {
            Debug.Log("backup length is not 0");
            for (int i = 0; i < backup.Length; i++)
            {
                headItems[i] = backup[i];
                Debug.Log("Re-adding item " + accessoryItems[i] + " into new array");
            }
            accessoryItems[accessoryItems.Length - 1] = item;
        }
        else
        {
            Debug.Log("backup length is 0");
            accessoryItems[0] = item;
        }
        setAccessoryOptions();
    }

    public void AddWeapon(int ID)
    {
        Items item;
        index.itemIndex.TryGetValue(ID, out item);
        Items[] backup = weaponItems;
        weaponItems = new Items[weaponItems.Length + 1];
        if (backup.Length != 0)
        {
            Debug.Log("backup length is not 0");
            for (int i = 0; i < backup.Length; i++)
            {
                weaponItems[i] = backup[i];
                Debug.Log("Re-adding item " + weaponItems[i] + " into new array");
            }
            weaponItems[weaponItems.Length - 1] = item;
        }
        else
        {
            Debug.Log("backup length is 0");
            weaponItems[0] = item;
        }
        setWeaponOptions();
    }
}
