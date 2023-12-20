using System.Collections.Generic;
using UnityEngine;

public class AllShirt : MonoBehaviour
{
    [SerializeField] private List<ShirtProductProperty> shirts;
    public List<ShirtProductProperty> Shirts { get { return shirts; } }
}