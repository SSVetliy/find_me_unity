using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private StartSceneController _startSceneController;
    [SerializeField] private Transform _panelCard;
    [SerializeField] private List<CardController> _cards;
    [SerializeField] private GameObject _gameingCard;
    [SerializeField] private GameObject _winGamePanel;
    [SerializeField] private PlayerProperty _player;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _stepText;

    [SerializeField] private TextMeshProUGUI _flipCardText;
    [SerializeField] private TextMeshProUGUI _addStepText; 
    [SerializeField] private TextMeshProUGUI _addTimeText;
    [SerializeField] private TextMeshProUGUI _viewAllText;
    [SerializeField] private TextMeshProUGUI _winPanelText;

    [SerializeField] private List<GameObject> _priceImage;

    [SerializeField] int countCard;
    [SerializeField] int sizeCard;
    [SerializeField] int countColumn;
    [SerializeField] int maxTime;
    [SerializeField] int maxSteps;

    [SerializeField] private SoundController sound;

    [SerializeField] private GameObject settingsCanv;
    [SerializeField] private GameObject settingsCanvBackButton;
    [SerializeField] private GameObject settingsCanvOpenButton;

    [SerializeField] private GameObject infoCanv;
    [SerializeField] private GameObject infoCanvBackButton;
    [SerializeField] private GameObject infoCanvOpenButton;

    private float timer;
    private int steps;
    private bool isPauseGame;
    private bool isStopGame;
    public int idLevel;

    public int CountCard
    {
        get { return countCard; }
        set { countCard = value; }
    }

    public int SizeCard
    {
        get { return sizeCard; }
        set { sizeCard = value; }
    }
    public int CountColumn
    {
        get { return countColumn; }
        set { countColumn = value; }
    }

    private int _curIdCard;
    private bool _invertCard;

    public bool InvertCard
    {
        get { return _invertCard; }
        set { _invertCard = value; }
    }

    Vector2 resolution;

    void Start()
    {
        resolution = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        if (!isStopGame && !isPauseGame)
        {
            timer -= Time.deltaTime;

            string minute = (Mathf.Floor(timer / 60)).ToString();
            string second = (Mathf.Floor(timer % 60)).ToString();
            second = second.Length == 1 ? "0" + second : second;
            _timeText.text = minute + ":" + second;

            if (timer <= 0)
            {
                timer = 0;
                isStopGame = true;
                StopGame(false);
            }
        }

        if (resolution.x != Screen.width || resolution.y != Screen.height)
        {
            ScreenResize();

            resolution.x = Screen.width;
            resolution.y = Screen.height;
        }
    }

    void ScreenResize()
    {
        float constW = 1447;
        float constH = 980;

        var size = GetComponent<RectTransform>().localScale *
            GetComponent<RectTransform>().rect.size;

        var grid = GetComponent<Beardy.GridLayoutGroup>();

        grid.cellSize = new Vector2(size.x * sizeCard / constW,
            size.y * sizeCard / constH);
    }

    private void StopGame(bool isWin)
    {
        transform.parent.gameObject.SetActive(false);
        _winGamePanel.SetActive(true);
        settingsCanvOpenButton.SetActive(false);
        infoCanvOpenButton.SetActive(false);


        if (isWin)
        {
            _winPanelText.text = "Поздравляю";

            sound.WinLevel();

            _priceImage[0].transform.parent.GetComponent<Beardy.GridLayoutGroup>().constraintCount = 3;

            for(int i = 0; i < _priceImage.Count; i++)
            {
                _priceImage[i].gameObject.SetActive(true);

                if (i == 0 || i == 1)
                    continue;
                
                _priceImage[i].GetComponentInChildren<TextMeshProUGUI>().text = "3";
            }

            _player.countKey++;
            _player.countCoin += 200;

            _player.countFlipCard += 3;
            _player.countAddStep += 3;
            _player.countAddTime += 3;
            _player.countViewAll += 3;

            _player.openLevels = _player.openLevels == idLevel && idLevel < 44
                ? idLevel + 1 : _player.openLevels;
        }
        else
        {
            _winPanelText.text = "Проигрыш";

            sound.LossLevel();

            _priceImage[0].transform.parent.GetComponent<Beardy.GridLayoutGroup>().constraintCount = 2;
            _priceImage[0].gameObject.SetActive(false);
            _priceImage[1].gameObject.SetActive(false);


            for (int i = 2; i < _priceImage.Count; i++)
            {
                _priceImage[i].gameObject.SetActive(true);
                _priceImage[i].GetComponentInChildren<TextMeshProUGUI>().text = "1";
            }

            _player.countFlipCard += 1;
            _player.countAddStep += 1;
            _player.countAddTime += 1;
            _player.countViewAll += 1;
        }

        _startSceneController.ChangeTextMoneysKeys();
    }

    public void CloseGame()
    {
        transform.parent.gameObject.SetActive(true);
        _winGamePanel.SetActive(false);
    }

    public void ChangeTime(int value)
    {
        if (_player.countAddTime > 0)
        {
            timer += value;
            _player.countAddTime--;
            _addTimeText.text = _player.countAddTime.ToString();
        }
    }

    public void ChangeStep(bool clickBut)
    {
        if (clickBut && _player.countAddStep > 0)
        {
            steps += 10;
            _player.countAddStep--;
            _addStepText.text = _player.countAddStep.ToString();
        }

        _stepText.text = steps.ToString();
    }


    private void clear()
    {
        var childsGO = GetComponentsInChildren<Transform>();

        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);

        _cards.Clear();
    }

    public void init()
    {
        clear();

        System.Random RND = new System.Random();

        for (int i = 0; i < _player.myList.Cards.Count; i++)
        {
            var tmp = _player.myList.Cards[0];
            _player.myList.Cards.RemoveAt(0);
            _player.myList.Cards.Insert(RND.Next(_player.myList.Cards.Count), tmp);
        }

        maxSteps = (countCard * (countCard - 1)) / 2;
        maxTime = maxSteps + 5;

        isStopGame = false;
        isPauseGame = false;
        _winGamePanel.SetActive(false);
        transform.parent.gameObject.SetActive(true);
        settingsCanvOpenButton.SetActive(true);
        infoCanvOpenButton.SetActive(true);

        _flipCardText.text = _player.countFlipCard.ToString();
        _addStepText.text = _player.countAddStep.ToString();
        _addTimeText.text = _player.countAddTime.ToString();
        _viewAllText.text = _player.countViewAll.ToString();

        timer = maxTime;
        string minute = (Mathf.Floor(timer / 60)).ToString();
        string second = (Mathf.Floor(timer % 60)).ToString();
        second = second.Length == 1 ? "0" + second : second;
        _timeText.text = minute + ":" + second;

        steps = maxSteps;
        _stepText.text = steps.ToString();

        _gameingCard.GetComponent<BoxCollider2D>().size = new Vector2 (sizeCard, sizeCard);

        var grid = GetComponent<Beardy.GridLayoutGroup>();
        grid.cellSize = new Vector2(sizeCard, sizeCard);
        grid.spacing = new Vector2(sizeCard / 10, sizeCard / 10);
        grid.constraintCount = countColumn;

        for (int i = 0; i < countCard / 2; i++)
        {
            var inst1 = Instantiate(_gameingCard);
            var inst2 = Instantiate(_gameingCard);

            inst1.SetActive(true);
            inst2.SetActive(true);

            inst1.transform.SetParent(_panelCard.transform, false);
            inst2.transform.SetParent(_panelCard.transform, false);

            inst1.name = _gameingCard.name;
            inst2.name = _gameingCard.name;

            inst1.GetComponent<CardController>().FrontSprite = _player.myList.Cards[i].sprite;
            inst2.GetComponent<CardController>().FrontSprite = _player.myList.Cards[i].sprite;

            inst1.GetComponent<CardController>().Id = i;
            inst2.GetComponent<CardController>().Id = i;

            inst1.GetComponent<CardController>().BackSprite = _player.curShirt.sprite;
            inst2.GetComponent<CardController>().BackSprite = _player.curShirt.sprite;

            inst1.GetComponent<CardController>().init();
            inst2.GetComponent<CardController>().init();

            inst1.transform.SetSiblingIndex(Random.Range(0, countCard));
            inst2.transform.SetSiblingIndex(Random.Range(0, countCard));

            _cards.Add(inst1.GetComponent<CardController>());
            _cards.Add(inst2.GetComponent<CardController>());
        }
    }

    public void FlipCardButton()
    {
        int openCard = 0;
        CardController curCard = null;

        foreach (CardController card in _cards)
            if (card.Invert)
            {
                openCard++;
                curCard = card;
            }

        if (openCard == 1 && curCard != null && _player.countFlipCard > 0)
        {
            curCard.NoFindCard();
            _player.countFlipCard--;
            _flipCardText.text = _player.countFlipCard.ToString();
        }
        
    }

    public void ViewAllCardsButoon()
    {
        if (_player.countViewAll > 0)
        {
            foreach (CardController card in _cards)
                if (!card.Invert && !card.IsWin)
                    StartCoroutine(card.CoroutineViewCard());
            _player.countViewAll--;
            _viewAllText.text = _player.countViewAll.ToString();
        }
    }

    public void OpenCards()
    {
        int openCard = 0;

        foreach (CardController card in _cards)
            if (card.Invert)
            {
                if (openCard == 0)
                    _curIdCard = card.Id;
                openCard++;
            }

        if (openCard == 2)
            CheackCard();
    }

    void CheackCard()
    {
        steps--;
        ChangeStep(false);

        int a = 0;

        foreach(CardController card in _cards)
            if (card.Invert && card.Id == _curIdCard)
                a++;

        if (a == 2)
            FindCards();
        else
            NoFindCards();
    }

    void FindCards()
    {
        foreach (CardController card in _cards)
            if (card.Invert)
                StartCoroutine(Win(card));
    }

    void NoFindCards()
    {
        foreach (CardController card in _cards)
            if (card.Invert)
                StartCoroutine(NoFind(card));
    }

    IEnumerator NoFind(CardController card)
    {
        _invertCard = true;
        yield return new WaitForSeconds(1f);
        card.NoFindCard();
        _invertCard = false;

        if (steps <= 0 && !isStopGame)
        {
            isStopGame = true;
            StopGame(false);
        }
    }

    IEnumerator Win(CardController card)
    {
        _invertCard = true;
        yield return new WaitForSeconds(1f);
        card.FindCard();
        CheackWin();
        _invertCard = false;
    }

    void CheackWin()
    {
        int winCard = 0;
        foreach (CardController card in _cards)
            if (card.IsWin)
                winCard++;

        if (winCard == _cards.Count)
        {
            isStopGame = true;
            StopGame(true);
        }
    }

    public void OpenSettings()
    {
        transform.parent.gameObject.SetActive(false);
        settingsCanv.SetActive(true);
        settingsCanvOpenButton.SetActive(false);
        infoCanvOpenButton.SetActive(false);
        isPauseGame = true;
    }

    public void CloseSettings()
    {
        settingsCanv.SetActive(false);
        transform.parent.gameObject.SetActive(true);
        settingsCanvOpenButton.SetActive(true);
        infoCanvOpenButton.SetActive(true);
        isPauseGame = false;
    }


    public void OpenInfo()
    {
        transform.parent.gameObject.SetActive(false);
        infoCanv.SetActive(true);
        settingsCanvOpenButton.SetActive(false);
        infoCanvOpenButton.SetActive(false);
        isPauseGame = true;
    }

    public void CloseInfo()
    {
        infoCanv.SetActive(false);
        transform.parent.gameObject.SetActive(true);
        settingsCanvOpenButton.SetActive(true);
        infoCanvOpenButton.SetActive(true);
        isPauseGame = false;
    }
}
