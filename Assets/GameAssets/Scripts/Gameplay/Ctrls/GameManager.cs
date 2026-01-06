using Cysharp.Threading.Tasks;
using Doozy.Runtime.Nody;
using Doozy.Runtime.Signals;
using Sirenix.OdinInspector;
using StansAssets.Foundation.Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public enum GameState
    {
        Init,
        Pause,
        Playing,
        Win,
        Lose,
        Tut,
        LockInteractive
    }
    public class GameManager : MonoBehaviour
    {
        public FlowController flow;
        public static GameManager Instance { get; private set; }
        [ShowInInspector] public List<BaseCtrl> baseCtrls { get; set; } = new();
        public Dictionary<Type, BaseCtrl> ctrlDic { get; set; } = new();
        [ShowInInspector] GameState gameState;
        private void Awake()
        {
            AddListener();
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
            Instance = this;
            baseCtrls = new List<BaseCtrl>(GetComponentsInChildren<BaseCtrl>());
            foreach (BaseCtrl ctrl in baseCtrls)
                ctrlDic.Add(ctrl.GetType(), ctrl);
        }

        private void Start()
        {
            Init();
            ShowUI();
        }

        private void OnDestroy()
        {
            RemoveListener();
            Instance = null;
        }

        private void AddListener()
        {
            StaticBus<EventGameInit>.Subscribe(OnEventGameInit);
            StaticBus<EventGameReset>.Subscribe(OnEventGameReset);
        }


        private void RemoveListener()
        {
            StaticBus<EventGameInit>.Unsubscribe(OnEventGameInit);
            StaticBus<EventGameReset>.Unsubscribe(OnEventGameReset);
        }

        private void OnEventGameReset(EventGameReset reset)
        {
            Reset();
        }

        private void OnEventGameInit(EventGameInit init)
        {
            Init();
        }

        [Button]
        public void Init()
        {
            gameState = GameState.Playing;
            foreach (var ctrl in baseCtrls)
                ctrl.Init();
        }

        private async void ShowUI()
        {
            await UniTask.WaitUntil(()=> flow.isValid);
            await UniTask.DelayFrame(20);
            Signal.Send(StreamId.FlowMainmenu.MainMenu);
        }
        void Update()
        {
            bool isPlaying = GameManager.Instance.gameState == GameState.Playing;
            if (!isPlaying) return;
            foreach (var ctrl in baseCtrls)
                ctrl.OnUpdate();
        }

        public void StartGame()
        {
            gameState = GameState.Playing;
            foreach (var ctrl in baseCtrls)
                ctrl.StartGame();
        }

        public void Reset()
        {
            foreach (var ctrl in baseCtrls)
                ctrl.Reset();
        }



        public static T GetCtrl<T>() where T : BaseCtrl
        {
            Type t= typeof(T);
            if (Instance.ctrlDic.ContainsKey(t))
                return (T)Instance.ctrlDic[t];
            return null;
        }

        public static GameState GameState
        {
            set { Instance.gameState = value; }
            get { return Instance.gameState; }
        }

    }
}