using System.Collections.Generic;
using UnityEngine;

public class AllCards : MonoBehaviour //содержит список со всеми картами
{
    [SerializeField] List<AssetCard> cards = new List<AssetCard>(); 
    public List<AssetCard> Cards { get { return cards; } }

    public AssetCard GetRandCard()
    {
        return cards[new System.Random().Next(cards.Count)];
    }

    void Start()
    {
        cards.Sort((x, y) => x.rang.CompareTo(y.rang));
    }
}
