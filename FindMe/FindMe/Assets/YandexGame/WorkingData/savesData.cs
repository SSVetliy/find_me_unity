using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class savesData : MonoBehaviour
{
    [SerializeField] private PlayerProperty player;

    public void Save()
    {
        YandexGame.savesData.myList = JsonUtility.ToJson(player.myList);

        YandexGame.savesData.curShirtId = player.curShirtId;
        YandexGame.savesData.curBackgroundId = player.curBackgroundId;

        YandexGame.savesData.countFlipCard = player.countFlipCard;
        YandexGame.savesData.countAddStep = player.countAddStep;
        YandexGame.savesData.countAddTime = player.countAddTime;
        YandexGame.savesData.countViewAll = player.countViewAll;

        YandexGame.savesData.countCoin = player.countCoin;
        YandexGame.savesData.countKey = player.countKey;

        YandexGame.savesData.openLevels = player.openLevels;

        YandexGame.SaveProgress();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
