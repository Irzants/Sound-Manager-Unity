using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GameManager : Singleton<GameManager>
    {
        void Start()
        {
            UIManager.Instance.loadViewFunc = LoadViewFunc;
            Application.logMessageReceived += LogCallback;
            SoundManager.Instance.Play("bgm_menu", true);
        }

        static ViewBase LoadViewFunc(string panelName)
        {
            return ResourceUtil.Load<ViewBase>(PathUtil.Panel(panelName));
        }

        static void LogCallback(string condition, string stackTrace, LogType type)
        {

        }

        [RuntimeInitializeOnLoadMethod]
        private static void RTInit()
        {
            Instance.Dummy();
        }
    }
