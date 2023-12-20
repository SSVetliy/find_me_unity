using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] private YandexGame yandexGame;

    public static YandexGame yandex;

    public void Load()
    {
        yandex = yandexGame;

        //YandexGame.ResetSaveProgress();
        //YandexGame.SaveProgress();

        SceneManager.LoadScene("start");
    }
}
