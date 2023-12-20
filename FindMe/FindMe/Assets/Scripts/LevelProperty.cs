using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProperty : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI attempts;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private Button play;

    [SerializeField] private GameObject StartSceneMeneger;

    public int id;
    public int countCard;
    public int sizeCard;
    public int countColumn;

    public LevelProperty(int id, int countCard, int sizeCard, int countColumn)
    {
        this.id = id;
        this.countCard = countCard;
        this.sizeCard = sizeCard;
        this.countColumn = countColumn;
    }

    public void init()
    {
        int maxSteps = (countCard * (countCard - 1)) / 2;
        int maxTime = maxSteps + 5;

        string minute = (Mathf.Floor(maxTime / 60)).ToString();
        string second = (Mathf.Floor(maxTime % 60)).ToString();
        second = second.Length == 1 ? "0" + second : second;

        switch (PlayerProperty.language)
        {
            case 0:
                title.text = "Уровень: " + (id + 1);
                attempts.text = "Попыток: " + maxSteps;
                time.text = "Время: " + minute + ":" + second;
                break;

            case 1:
                title.text = "Level: " + (id + 1);
                attempts.text = "Attempts: " + maxSteps;
                time.text = "Time: " + minute + ":" + second;
                break;
        }
    }

    public void PlayLevel() {
        StartSceneMeneger.GetComponent<StartSceneController>().onPlayLevel?.Invoke(id);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
