using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentItemController : MonoBehaviour
{
    [SerializeField] private PlayerProperty player;
    public int id;

    public void getPresent()
    {
        switch (id)
        {
            case 0:
                player.countFlipCard += 10;
                break;
            case 1:
                player.countAddStep += 10;
                break;
            case 2:
                player.countAddTime += 10;
                break;
            case 3:
                player.countViewAll += 10;
                break;
            case 4:
                player.countCoin += 2000;
                break;
            case 5:
                player.countKey += 10;
                break;
        }
    }
}
