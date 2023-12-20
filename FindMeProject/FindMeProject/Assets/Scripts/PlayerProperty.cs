using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MyList
{
    public List<AssetCard> Cards; 
    public List<ShirtProductProperty> Shirts; 
    public List<BackgroundProductProperty> Background; 
}

public class PlayerProperty : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;

    public List<AssetCard> startCard;
    public List<ShirtProductProperty> startShirt;
    public List<BackgroundProductProperty> startBackgroung;

    public MyList myList;

    [SerializeField] private StartSceneController startSceneController;
    public BackgroundProductProperty curBackground;
    public int curShirtId; 

    public ShirtProductProperty curShirt;
    public int curBackgroundId; 

    [SerializeField] private AllCards _allCards;
    [SerializeField] private AllShirt _allShirts;
    [SerializeField] private AllBackground _allBackground;

    public int countFlipCard; 
    public int countAddStep; 
    public int countAddTime; 
    public int countViewAll; 

    public int countCoin; 
    public int countKey; 

    public int openLevels; 

    public void init()
    {
        SaveData saveData = saveManager.Load();

        myList = JsonUtility.FromJson<MyList>(saveData.myList);
        if (myList == null)
            myList = new MyList();

        if (myList.Cards == null || myList.Cards.Count == 0)
            myList.Cards = new List<AssetCard>(startCard);

        if (myList.Shirts == null || myList.Shirts.Count == 0)
            myList.Shirts = new List<ShirtProductProperty>(startShirt);

        if (myList.Background == null || myList.Background.Count == 0)
            myList.Background = new List<BackgroundProductProperty>(startBackgroung);

        curShirtId = saveData.curShirtId;
        if (curShirtId == -1)
            curShirtId = 15;
        curShirt = myList.Shirts.Find(x => x.id == curShirtId);

        curBackgroundId = saveData.curBackgroundId;
        if (curBackgroundId == -1)
            curBackgroundId = 6;
        curBackground = myList.Background.Find(x => x.id == curBackgroundId);

        countFlipCard = saveData.countFlipCard;
        countAddStep = saveData.countAddStep;
        countAddTime = saveData.countAddTime;
        countViewAll = saveData.countViewAll;

        countCoin = saveData.countCoin;
        countKey = saveData.countKey;

        openLevels = saveData.openLevels;

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
