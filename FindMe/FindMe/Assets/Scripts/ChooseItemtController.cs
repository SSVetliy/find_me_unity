using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChooseItemtController : MonoBehaviour
{
    public int id;
    public bool isShirt;
    [SerializeField] PlayerProperty player;
    [SerializeField] private AllBackground allBackground;
    [SerializeField] private AllShirt allShirts;

    Action<int, bool> onBuyItem;

    private void OnEnable()
    {
        onBuyItem += BuyItem;
    }

    private void OnDisable()
    {
        onBuyItem -= BuyItem;
    }

    public void ChooseItem()
    {
        onBuyItem?.Invoke(id, isShirt);
    }

    private void Start()
    {
        if (isShirt)
        {
            if (id == player.curShirtId)
            {
                GetComponentsInChildren<Image>()[1].color = Color.green;
                player.curShirt = player.myList.Shirts.Find(x => x.id == id);
            }
        }
        else
        {
            if (id == player.curBackgroundId)
            {
                GetComponentsInChildren<Image>()[1].color = Color.green;
                player.curBackground = player.myList.Background.Find(x => x.id == id);
            }
        }
    }

    private void BuyItem(int _id, bool _isShirt)
    {
        if (_id == id)
        {
            if (_isShirt)
            {
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    var child = transform.parent.GetChild(i).gameObject;

                    if (child.GetComponent<ChooseItemtController>().id == player.curShirtId)
                    {
                        child.GetComponentsInChildren<Image>()[1].color = Color.white;
                        break;
                    }
                }

                player.curShirt = player.myList.Shirts.Find(x => x.id == _id);
                player.curShirtId = _id;

                GetComponentsInChildren<Image>()[1].color = Color.green;
            }
            else
            {
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    var child = transform.parent.GetChild(i).gameObject;

                    if (child.GetComponent<ChooseItemtController>().id == player.curBackgroundId)
                    {
                        child.GetComponentsInChildren<Image>()[1].color = Color.white;
                        break;
                    }
                }

                player.curBackground = player.myList.Background.Find(x => x.id == _id);
                player.curBackgroundId = _id;

                GetComponentsInChildren<Image>()[1].color = Color.green;
            }
        }
    }
}
