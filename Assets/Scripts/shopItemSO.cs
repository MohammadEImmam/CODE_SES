using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shop Item", menuName = "Scriptable Objects/New Item", order = 1)]
public class shopItemSO : ScriptableObject {
    public string name;
    public string desc;
    public int price;
    public Sprite itemImage;
}