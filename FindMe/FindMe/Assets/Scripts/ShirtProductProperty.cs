using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ShirtProductProperty")]
public class ShirtProductProperty : ScriptableObject, IShirtProduct
{
    public int id => _id;
    public Sprite sprite => _sprite;
    public int price => _price;

    [SerializeField] private int _id;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
}

interface IShirtProduct
{
    int id { get; }
    Sprite sprite { get; }
    int price { get; }
}
