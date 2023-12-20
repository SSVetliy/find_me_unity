using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

[System.Serializable]
public class MyList
{
    public List<AssetCard> Cards; //
    public List<ShirtProductProperty> Shirts; //
    public List<BackgroundProductProperty> Background; //
}

public class PlayerProperty : MonoBehaviour
{
    public List<AssetCard> startCard;
    public List<ShirtProductProperty> startShirt;
    public List<BackgroundProductProperty> startBackgroung;

    public MyList myList;

    public static int language = YandexGame.EnvironmentData.language.Equals("ru") ? 0 : 1;

    [SerializeField] private StartSceneController startSceneController;

    public ShirtProductProperty curShirt;
    public int curShirtId; //

    public BackgroundProductProperty curBackground;
    public int curBackgroundId; //

    [SerializeField] private AllCards _allCards;
    [SerializeField] private AllShirt _allShirts;
    [SerializeField] private AllBackground _allBackground;

    public int countFlipCard; //
    public int countAddStep; //
    public int countAddTime; //
    public int countViewAll; //

    public int countCoin; //
    public int countKey; //

    public int openLevels; //

    private void OnEnable() => YandexGame.GetDataEvent += init;
    private void OnDisable() => YandexGame.GetDataEvent -= init;

    public void init()
    {
        myList = JsonUtility.FromJson<MyList>(YandexGame.savesData.myList);
        if (myList == null)
            myList = new MyList();

        if (myList.Cards == null || myList.Cards.Count == 0)
            myList.Cards = new List<AssetCard>(startCard);

        if (myList.Shirts == null || myList.Shirts.Count == 0)
            myList.Shirts = new List<ShirtProductProperty>(startShirt);

        if (myList.Background == null || myList.Background.Count == 0)
            myList.Background = new List<BackgroundProductProperty>(startBackgroung);

        curShirtId = YandexGame.savesData.curShirtId;
        if (curShirtId == -1)
            curShirtId = 15;
        curShirt = myList.Shirts.Find(x => x.id == curShirtId);

        curBackgroundId = YandexGame.savesData.curBackgroundId;
        if(curBackgroundId == -1)
            curBackgroundId = 6;
        curBackground = myList.Background.Find(x => x.id == curBackgroundId);

        countFlipCard = YandexGame.savesData.countFlipCard;
        countAddStep = YandexGame.savesData.countAddStep;
        countAddTime = YandexGame.savesData.countAddTime;
        countViewAll = YandexGame.savesData.countViewAll;

        countCoin = YandexGame.savesData.countCoin;
        countKey = YandexGame.savesData.countKey;

        openLevels = YandexGame.savesData.openLevels;

        startSceneController.ChangeTextMoneysKeys();
    }

    public void AddShirt(int id)
    {
        myList.Shirts.Add(_allShirts.Shirts.Find(x => x.id == id));
    }

    public void AddBackground(int id)
    {
        myList.Background.Add(_allBackground.Background.Find(x => x.id == id));
    }

    public void ChangeAllShirts()
    {
        _allShirts.Shirts.RemoveAll(item1 => myList.Shirts.Any(item2 => item1.id == item2.id));
    }

    public void ChangeAllCards()
    {
        _allCards.Cards.RemoveAll(item1 => myList.Cards.Any(item2 => item1.id == item2.id));
    }

    public void ChangeAllBackground()
    {
        _allBackground.Background.RemoveAll(item1 => myList.Background.Any(item2 => item1.id == item2.id));
    }
}
