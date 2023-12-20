using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProductShopController : MonoBehaviour
{
    public int _id;
    public int _price;

    [SerializeField] private PlayerProperty _player;
    [SerializeField] private StartSceneController _startSceneController;

    [SerializeField] private SoundController sound;

    public void init()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = _price.ToString();
    }

    public void BuyShirt()
    {
        if(_player.countCoin >= _price)
        {
            sound.BuyItem();
            _player.countCoin -= _price;
            _player.AddShirt(_id);
            _startSceneController.ChangeTextMoneysKeys();
            Destroy(this.gameObject);
        }
        else
        {
            sound.Error();
        }
    }

    public void BuyBackground()
    {
        if (_player.countCoin >= _price)
        {
            sound.BuyItem();
            _player.countCoin -= _price;
            _player.AddBackground(_id);
            _startSceneController.ChangeTextMoneysKeys();
            Destroy(this.gameObject);
        }
        else
        {
            sound.Error();
        }
    }
}
