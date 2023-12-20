using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Ваши сохранения
        public string myList;

        public int curShirtId;
        public int curBackgroundId;

        public int countFlipCard;
        public int countAddStep;
        public int countAddTime;
        public int countViewAll;

        public int countCoin;
        public int countKey;

        public int openLevels;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            curShirtId = -1;
            curBackgroundId = -1;
        }
    }
}
