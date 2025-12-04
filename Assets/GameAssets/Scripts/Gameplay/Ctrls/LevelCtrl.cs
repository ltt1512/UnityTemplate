using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Gameplay
{
    public class LevelCtrl : BaseCtrl
    {
        public LevelDataSO defaultLevel;
        public LevelDataSO CurLevel;
        public int levelCount;
        public bool isTest;
        [ShowIf("isTest")]
        public int levelTest;
        #region unity func
        #endregion
        #region override func
        public override void Init()
        {
            var curLevel = GameplaySaveData.GetLevelId();
            if (curLevel == 0)
                curLevel = 1;
            // TinySauce.OnGameStarted(curLevel);
#if UNITY_EDITOR
            if (isTest)
                curLevel = levelTest;
#endif


            if (curLevel > levelCount)
                curLevel = Random.Range(0, levelCount);


            CurLevel = Resources.Load<LevelDataSO>($"Levels/Level_{curLevel:D3}");
            if (CurLevel == null)
                CurLevel = defaultLevel;
        }

        public override void Reset()
        {
        }

        public override void StartGame()
        {
        }
        #endregion
        [Button]
        void SetLevelCount()
        {
            levelCount = Resources.LoadAll("Levels").Length;
        }

        [Button]
        void SetData()
        {
#if UNITY_EDITOR
            var allLevels = Resources.LoadAll<LevelDataSO>("Levels");
           
            foreach (var level in allLevels)
            {
                EditorUtility.SetDirty(level);
            }
            AssetDatabase.SaveAssets();
#endif
        }

  
    }
}