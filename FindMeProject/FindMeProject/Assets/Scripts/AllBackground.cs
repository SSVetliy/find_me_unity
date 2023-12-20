using System.Collections.Generic;
using UnityEngine;

public class AllBackground : MonoBehaviour
{
    [SerializeField] private List<BackgroundProductProperty> background;
    public List<BackgroundProductProperty> Background { get { return background; } }
}
