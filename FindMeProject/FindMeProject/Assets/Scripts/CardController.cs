using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;

    [SerializeField] private Sprite _backSprite;
    [SerializeField] private Sprite _frontSprite;
    [SerializeField] private Color _winSprite;
    [SerializeField] private int id;

    [SerializeField] private SoundController sound;

    private Animator _animator;

    private bool invert;
    private bool isWin;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public Sprite FrontSprite
    {
        get { return _frontSprite; }
        set { _frontSprite = value; }
    }

    public Sprite BackSprite
    {
        get { return _backSprite; }
        set { _backSprite = value; }
    }

    public void PlayAnimation(string str)
    {
        if(str.Equals("FlipGameingCard"))
            sound.FlipCard();
        _animator.Play(str);
    }

    public bool Invert
    {
        get { return invert; }
        set { invert = value; }
    }

    public bool IsWin
    {
        get { return isWin; }
        set { isWin = value; }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void init()
    {
        invert = false;
        isWin = false;
        _animator = GetComponent<Animator>();
        _gameController = transform.parent.GetComponent<GameController>();
        GetComponent<Image>().sprite = _backSprite;
    }

    public void Click()
    {
        if (!_gameController.InvertCard && !invert && !isWin)
        {
            invert = true;
            StartCoroutine(CoroutinePlayAnimation(_frontSprite));
            _gameController.OpenCards();
        }
    }

    public IEnumerator CoroutineViewCard()
    {
        PlayAnimation("FlipGameingCard");
        yield return new WaitForSeconds(0.25f);
        this.GetComponent<Image>().sprite = _frontSprite;

        yield return new WaitForSeconds(1f);

        PlayAnimation("FlipGameingCard");
        yield return new WaitForSeconds(0.25f);
        this.GetComponent<Image>().sprite = _backSprite;
    }

    IEnumerator CoroutinePlayAnimation(Sprite sprite)
    {
        PlayAnimation("FlipGameingCard");
        yield return new WaitForSeconds(0.25f);
        this.GetComponent<Image>().sprite = sprite;
    }

    IEnumerator CoroutinePlayAnimationWin()
    {
        PlayAnimation("WinGameingCard");
        yield return new WaitForSeconds(0.5f);

        GetComponent<Image>().enabled = false;
        transform.GetChild(0).GetComponent<Image>().enabled = false;
    }

    public void NoFindCard()
    {
        StartCoroutine(CoroutinePlayAnimation(_backSprite));
        invert = false;
    }

    public void FindCard()
    {
        StartCoroutine(CoroutinePlayAnimationWin());
        invert = false;
        isWin = true;
    }
}
