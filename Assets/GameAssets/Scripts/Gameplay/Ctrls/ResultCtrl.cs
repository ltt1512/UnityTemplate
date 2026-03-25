using Doozy.Runtime.Signals;
using Foundation.Patterns;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Gameplay
{
    public class ResultCtrl : BaseCtrl
    {
        public float timePlayer => 300;
        public float cTIme = 0;
        int curTime = 0;
        public override void Init()
        {
            curTime = (int)timePlayer;
            cTIme = timePlayer;
            StaticBus<EventChangeTime>.Post(new EventChangeTime()
            {
                time = curTime,
            });
        }

        public override void OnUpdate()
        {
          
            cTIme -= Time.deltaTime;
            int time = (int)(cTIme);
            if (time != curTime)
            {
                curTime = time;
                StaticBus<EventChangeTime>.Post(new EventChangeTime()
                {
                    time = curTime,
                });
            }
            if (cTIme <= 1)
            {
                LoseGame();
            }
        }

        public override void Reset()
        {
        }

        public override void StartGame()
        {
        }

        [Button]
        public void LoseGame()
        {
            if (GameManager.GameState != GameState.Playing) return;
            Signal.Send(StreamId.FlowMainmenu.Lose);
            GameManager.GameState = GameState.Lose;
        }
        [Button]
        public void WinGame()
        {
            if (GameManager.GameState != GameState.Playing) return;
            GameManager.GameState = GameState.Win;
            GameplaySaveData.IncreaseLevel(1);
            GameplaySaveData.AddCoin(30);
            Signal.Send(StreamId.FlowMainmenu.Win);
        }
    }
}
