using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Items : ScriptableObject
{
    public string m_name;
    public Sprite icon;
    public string flair;
    public enum type {Head, Weapon, Accessory, Tunic, Cloak};
    public type itemType;
    public int indexID;
}