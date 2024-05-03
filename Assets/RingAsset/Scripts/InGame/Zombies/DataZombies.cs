using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDataZombies", menuName = "CustomData/DataZombies", order = 1)]
public class DataZombies : ScriptableObject
{
    public int Health;
    public int Speed;
}
