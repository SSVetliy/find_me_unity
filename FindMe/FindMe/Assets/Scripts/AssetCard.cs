using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(menuName = "Card")]
public class AssetCard : ScriptableObject, ICard
{
    public int id => _id;
    public Sprite sprite => _sprite;
    public int rang => _rang;

    [SerializeField] private int _id;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _rang;

    public static void Render(GameObject prefub, GameObject parent, AssetCard card)
    {
        var inst = Instantiate(prefub);
        inst.SetActive(true);
        inst.transform.SetParent(parent.transform, false);
        inst.name = prefub.name;
        inst.GetComponent<Image>().sprite = card.sprite;

        switch (card.rang)
        {
            case 0:
                inst.GetComponentsInChildren<Image>()[1].color = new Color(1f, 0.8431372549019608f, 0);
                break;
            case 1:
                inst.GetComponentsInChildren<Image>()[1].color = new Color(0.4117647058823529f, 0, 0.7764705882352941f);
                break;
            case 2:
                inst.GetComponentsInChildren<Image>()[1].color = new Color(0.6f, 1f, 0.6f);
                break;
            case 3:
                inst.GetComponentsInChildren<Image>()[1].color = new Color(0, 0, 1f);
                break;
            case 4:
                inst.GetComponentsInChildren<Image>()[1].color = new Color(1f, 1f, 1f);
                break;
        }
    }
}

interface ICard
{
    int id { get; }
    Sprite sprite { get; }
    int rang { get; }
}