using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "BackgroundProductProperty")]
public class BackgroundProductProperty : ScriptableObject, IBackgroundProduct
{
    public int id => _id;
    public Sprite sprite => _sprite;
    public Sprite ui_sprite => _ui_sprite;
    public int price => _price;

    [SerializeField] private int _id;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Sprite _ui_sprite;
    [SerializeField] private int _price;

}

interface IBackgroundProduct
{
    int id { get; }
    Sprite sprite { get; }
    Sprite ui_sprite { get; }
    int price { get; }
}
