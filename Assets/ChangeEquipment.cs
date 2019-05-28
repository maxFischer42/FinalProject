using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEquipment : MonoBehaviour
{
    private InventoryManager manager;
    private ItemIndex index;
    public Image image;
    Dropdown m_dropdown;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
        index = GameObject.Find("GameManager").GetComponent<ItemIndex>();
        m_dropdown = GetComponent<Dropdown>();
        m_dropdown.onValueChanged.AddListener(delegate { OnValueChanged(m_dropdown); });
    }
    public void OnValueChanged(Dropdown value)
    {
        image.sprite = value.options[value.value].image;
        Items equipedItem;
        index.imageIndex.TryGetValue(value.options[value.value].image, out equipedItem);
    }

}
