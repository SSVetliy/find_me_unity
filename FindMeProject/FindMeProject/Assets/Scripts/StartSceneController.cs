using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartSceneController : MonoBehaviour
{
    [SerializeField] private PlayerProperty player;
    [SerializeField] private AllCards allCards;
    [SerializeField] private AllShirt allShirts;
    [SerializeField] private AllBackground allBackground;
    [SerializeField] private GameObject mainBackground;

    [SerializeField] private List<GameObject> childsCanv;
    [SerializeField] private GameObject galleryContent;
    [SerializeField] private GameObject shopContent;
    [SerializeField] private GameObject levelsContent;

    [SerializeField] private GameObject levelsInfo;
    [SerializeField] private GameObject levelsView;

    [SerializeField] private GameObject chestInfo;
    [SerializeField] private GameObject chestView;

    [SerializeField] private GameObject chooseShirtView;
    [SerializeField] private GameObject galleryView;

    [SerializeField] private GameObject levelsSnapScroll;

    [SerializeField] private GameObject levelsPrefab;
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private GameObject shirtProductPrefab;
    [SerializeField] private GameObject backgroundProductPrefab;
    [SerializeField] private GameObject chooseItemtPrefab;

    [SerializeField] private GameObject shirtShopContent;
    [SerializeField] private GameObject backgroundShopContent;

    [SerializeField] private GameObject chestResult;
    [SerializeField] private GameObject chestResultParent;
    [SerializeField] private List<GameObject> chestUIHide;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject game;

    [SerializeField] private TextMeshProUGUI countCoinText;
    [SerializeField] private TextMeshProUGUI countKeyText;

    [SerializeField] private GameObject shopShirtPanel;
    [SerializeField] private GameObject shopBackgroundPanel;

    [SerializeField] private GameObject chooseShirtScrollContent;
    [SerializeField] private GameObject chooseBackgroundScrollContent;

    [SerializeField] private SoundController sound;

    [SerializeField] private SaveManager _savesData;

    private void OnEnable()
    {
        onPlayLevel += PlayLevel;
    }
    private void OnDisable()
    {
        onPlayLevel -= PlayLevel;
    }

    public Action<int> onPlayLevel;

    public void AddMoney()
    {
        player.countCoin = 123456789;
        _savesData.Save();
        ChangeTextMoneysKeys();
    }

    public void AddKeys()
    {
        player.countKey = 123;
        _savesData.Save();
        ChangeTextMoneysKeys();
    }

    public void ResetSaveProg()
    {
        _savesData.ResetSaveProgress();
        SceneManager.LoadScene("start");
    }

    public void OpenAllLevels()
    {
        player.openLevels = 44;
        _savesData.Save();
        ChangeTextMoneysKeys();
    }


    void Start()
    {
        player.init();
        ChangeTextMoneysKeys();

        mainBackground.GetComponent<Image>().sprite = player.curBackground.sprite;
    }

    public void BuyAssisttants(int id)
    {
        if (player.countCoin >= 200 && id != 4)
        {
            sound.BuyItem();

            switch (id)
            {
                case 0:
                    player.countFlipCard++;
                    break;
                case 1:
                    player.countAddStep++;
                    break;
                case 2:
                    player.countAddTime++;
                    break;
                case 3:
                    player.countViewAll++;
                    break;
            }
            player.countCoin -= 200;
        }
        else if (id == 4 && player.countKey > 0)
        {
            sound.BuyItem();
            player.countCoin += 200;
            player.countKey--;
        }
        else
        {
            sound.Error();
        }
        ChangeTextMoneysKeys();
    }

    public void CloseGame()
    {
        game.SetActive(false);
        menu.SetActive(true);
        _savesData.Save();
    }

    private void PlayLevel(int id)
    {
        var lvlProperty = AllLevels.lelels[id];

        if (lvlProperty.countCard / 2 <= player.myList.Cards.Count)
        {
            BackToStart();
            menu.SetActive(false);
            game.SetActive(true);

            var area = game.GetComponentInChildren<GameController>();

            area.SizeCard = lvlProperty.sizeCard;
            area.CountCard = lvlProperty.countCard;
            area.CountColumn = lvlProperty.countColumn;
            area.idLevel = id;
            area.init();
        }
        else
        {
            StartCoroutine(ViewLevelInfo());
        }
    }

    IEnumerator ViewLevelInfo()
    {
        levelsView.SetActive(false);
        levelsInfo.SetActive(true);
        yield return new WaitForSeconds(3f);
        levelsInfo.SetActive(false);
        levelsView.SetActive(true);
    }

    IEnumerator ViewChestInfo()
    {
        chestView.SetActive(false);
        chestInfo.SetActive(true);
        yield return new WaitForSeconds(3f);
        chestInfo.SetActive(false);
        chestView.SetActive(true);
    }

    public void OpenLevels()
    {
        childsCanv.Find(x => x.name.Equals("startCanv")).SetActive(false);
        childsCanv.Find(x => x.name.Equals("levelsCanv")).SetActive(true);

        if (levelsSnapScroll.transform.childCount <= player.openLevels)
        {
            for (int i = 0; i < levelsSnapScroll.transform.childCount; i++)
                Destroy(levelsSnapScroll.transform.GetChild(i).gameObject);

            for (int i = 0; i <= player.openLevels; i++)
            {
                var inst = Instantiate(levelsPrefab);
                inst.SetActive(true);
                inst.transform.SetParent(levelsSnapScroll.transform);
                inst.transform.SetSiblingIndex(i);
                var lvlProp = inst.GetComponent<LevelProperty>();
                lvlProp.id = AllLevels.lelels[i].id;
                lvlProp.countCard = AllLevels.lelels[i].countCard;
                lvlProp.countColumn = AllLevels.lelels[i].countColumn;
                lvlProp.sizeCard = AllLevels.lelels[i].sizeCard;
          
                lvlProp.init();
            }
        }
    }

    public void OpenShop()
    {
        player.ChangeAllShirts();
        player.ChangeAllBackground();

        childsCanv.Find(x => x.name.Equals("startCanv")).SetActive(false);
        childsCanv.Find(x => x.name.Equals("shopCanv")).SetActive(true);

        shopContent.GetComponent<RectTransform>().localPosition = Vector3.zero;

        if(allShirts.Shirts.Count == 0)
        {
            shopShirtPanel.SetActive(false);
        }
        else if (shirtShopContent.transform.childCount < allShirts.Shirts.Count)
        {
            shopShirtPanel.SetActive(true);
            foreach (var shirt in allShirts.Shirts)
            {
                var inst = Instantiate(shirtProductPrefab);
                inst.SetActive(true);
                inst.transform.SetParent(shirtShopContent.transform);
                inst.GetComponent<RectTransform>().localScale = Vector3.one;
                inst.GetComponent<RectTransform>().position = Vector3.zero;
                inst.GetComponent<Image>().sprite = shirt.sprite;
                inst.GetComponent<ProductShopController>()._id = shirt.id;
                inst.GetComponent<ProductShopController>()._price = shirt.price;
                inst.GetComponent<ProductShopController>().init();
            }
        }

        if (allBackground.Background.Count == 0)
        {
            shopBackgroundPanel.SetActive(false);
        }
        else if (backgroundShopContent.transform.childCount < allBackground.Background.Count)
        {
            shopBackgroundPanel.SetActive(true);
            foreach (var background in allBackground.Background)
            {
                var inst = Instantiate(backgroundProductPrefab);
                inst.SetActive(true);
                inst.transform.SetParent(backgroundShopContent.transform);
                inst.GetComponent<RectTransform>().localScale = Vector3.one;
                inst.GetComponent<RectTransform>().position = Vector3.zero;
                inst.GetComponent<Image>().sprite = background.ui_sprite;
                inst.GetComponent<ProductShopController>()._id = background.id;
                inst.GetComponent<ProductShopController>()._price = background.price;
                inst.GetComponent<ProductShopController>().init();
            }
        }
    }

    public void OpenSettings()
    {
        childsCanv.Find(x => x.name.Equals("startCanv")).SetActive(false);
        childsCanv.Find(x => x.name.Equals("settingsCanv")).SetActive(true);
    }

    public void OpenGallery()
    {
        player.myList.Cards.Sort((x, y) => x.rang.CompareTo(y.rang));
        player.myList.Shirts.Sort((x, y) => x.id.CompareTo(y.id));
        player.myList.Background.Sort((x, y) => x.id.CompareTo(y.id));

        galleryView.SetActive(true);
        chooseShirtView.SetActive(false);

        childsCanv.Find(x => x.name.Equals("startCanv")).SetActive(false);
        childsCanv.Find(x => x.name.Equals("galleryCanv")).SetActive(true);

        galleryContent.GetComponent<RectTransform>().localPosition = Vector3.zero;

        foreach (var child in galleryContent.GetComponentsInChildren<Transform>().Where(x => x.name.Equals(cardUIPrefab.name)))
        {
            if (child == galleryContent) continue;
            Destroy(child.gameObject);
        }

        foreach (var card in player.myList.Cards)
            AssetCard.Render(cardUIPrefab, galleryContent, card);
    }

    public void OpenChest()
    {
        childsCanv.Find(x => x.name.Equals("startCanv")).SetActive(false);
        childsCanv.Find(x => x.name.Equals("chestCanv")).SetActive(true);
    }

    public void ChestGetOneCard()
    {
        player.ChangeAllCards();

        if (allCards.Cards.Count < 1)
            StartCoroutine(ViewChestInfo());

        else if (player.countKey >= 1)
        {
            player.countKey--;
            StartCoroutine(GetCard(1));
        }
        else
            sound.Error();
    }

    public void ChestGetTenCard()
    {
        player.ChangeAllCards();

        if (allCards.Cards.Count < 10)
            StartCoroutine(ViewChestInfo());

        else if (player.countKey >= 10)
        {
            player.countKey -= 10;
            StartCoroutine(GetCard(10));
        }
        else
            sound.Error();
    }

    public void ChooseShirt()
    {
        galleryView.SetActive(false);
        chooseShirtView.SetActive(true);
            for(int i = 0; i < chooseShirtScrollContent.transform.childCount; i++)
                Destroy(chooseShirtScrollContent.transform.GetChild(i).gameObject);

        for (int i = 0; i < player.myList.Shirts.Count; i++)
        {
            var inst = Instantiate(chooseItemtPrefab);
            inst.GetComponent<Image>().sprite = player.myList.Shirts[i].sprite;
            inst.GetComponent<ChooseItemtController>().id = player.myList.Shirts[i].id;
            inst.GetComponent<ChooseItemtController>().isShirt = true;
            inst.SetActive(true);
            inst.transform.SetParent(chooseShirtScrollContent.transform);
        }

        for (int i = 0; i < chooseBackgroundScrollContent.transform.childCount; i++)
            Destroy(chooseBackgroundScrollContent.transform.GetChild(i).gameObject);

        for (int i = chooseBackgroundScrollContent.transform.childCount; i < player.myList.Background.Count; i++)
        {
            var inst = Instantiate(chooseItemtPrefab);
            inst.GetComponent<Image>().sprite = player.myList.Background[i].ui_sprite;
            inst.GetComponent<ChooseItemtController>().id = player.myList.Background[i].id;
            inst.GetComponent<ChooseItemtController>().isShirt = false;
            inst.SetActive(true);
            inst.transform.SetParent(chooseBackgroundScrollContent.transform);
        }
    }

    public void ChangeTextMoneysKeys()
    {
        countCoinText.text = player.countCoin.ToString();
        countKeyText.text = player.countKey.ToString();
    }


    IEnumerator GetCard(int countCard)
    {
        ChangeTextMoneysKeys();

        sound.OpenChest();

        Animator chestAnimation = childsCanv.Find(x => x.name.Equals("chestCanv"))
            .GetComponentInChildren<Animator>();
        chestAnimation.Play("OpenChest");

        yield return new WaitForSeconds(1f);

        chestResultParent.SetActive(true);

        foreach (var el in chestUIHide)
            el.SetActive(false);

        foreach (var child in chestResult.GetComponentsInChildren<Transform>().ToList())
        {
            if (child.name.Equals(chestResult.name)) continue;
            Destroy(child.gameObject);
        }

        List<AssetCard> randCards = new List<AssetCard>();

        for (int i = 0; i < countCard; i++)
        {
            player.ChangeAllCards();
            var randCard = allCards.GetRandCard();
            randCards.Add(randCard);
            player.myList.Cards.Add(randCard);
        }

        foreach (var randCard in randCards)
            AssetCard.Render(cardUIPrefab, chestResult, randCard);

        player.ChangeAllCards();
    }

    public void CloseChestResult()
    {
        foreach (var el in chestUIHide)
            el.SetActive(true);

        chestResultParent.SetActive(false);
        Animator chestAnimation = childsCanv.Find(x => x.name.Equals("chestCanv"))
            .GetComponentInChildren<Animator>();
        chestAnimation.Play("IdleChest");
    }

    public void BackToStart()
    {
        ChangeTextMoneysKeys();

        mainBackground.GetComponent<Image>().sprite = player.curBackground.sprite;

        foreach (GameObject child in childsCanv)
        {
            child.SetActive(false);
        }

        childsCanv.Find(x => x.name.Equals("startCanv")).SetActive(true);

        _savesData.Save();
    }
}
