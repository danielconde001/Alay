using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "ScriptableObjects/Buff", order = 1)]
public class BuffData : ScriptableObject
{
    public int ID;
    public string Name;
    public Sprite Image;
    public string Description;
}
