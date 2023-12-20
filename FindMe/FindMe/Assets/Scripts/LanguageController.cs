using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageController : MonoBehaviour
{
    [Multiline]
    [SerializeField] private List<string> texts;
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = texts[PlayerProperty.language];
    }
}
