using Doozy.Runtime.UIManager.Containers;
using Doozy.Runtime.UIManager.ScriptableObjects;
using Gameplay;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Android;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.UI
{
    public class PopupCtrl : BaseCtrl
    {
        public Dictionary<PopupEnum, UIPopup> popups = new();
        public override void Init()
        {
        }

        public override void Reset()
        {
        }

        public override void StartGame()
        {
        }

        public UIPopup ShowPopup(PopupEnum popupEnum)
        {
            if (popups.ContainsKey(popupEnum))
            {
                var p = popups[popupEnum];
                p.gameObject.SetActive(true);
                p.Show();
                return p;
            }
            var popup = AddPopup(popupEnum);
            if (popup != null)
            {
                popup.Show();
                popup.gameObject.SetActive(true);
                return popup;
            }
            Debug.Log($"Popup {popupEnum.ToString()} no available.");
            return null;
        }

        private UIPopup AddPopup(PopupEnum popupEnum)
        {
            var popup = Get(popupEnum.ToString());
            if (popup != null)
                popups.Add(popupEnum, popup);
            return popup;
        }

        private static UIPopup Get(string popupName)
        {
            if (string.IsNullOrEmpty(popupName)) return null;
            GameObject prefab = UIPopupDatabase.instance.GetPrefab(popupName);
            if (prefab == null)
            {
                Debug.LogWarning($"UIPopup.Get({popupName}) - prefab not found in the database");
                return null;
            }
            UIPopup popup =
                Instantiate(prefab)
                    .GetComponent<UIPopup>()
                    .Reset();
            popup.Validate();
            popup.SetParent(popup.GetParent());
            popup.InstantHide(false);
            popup.DisableGameObjectWhenHidden = true;
            
            return popup;
        }


#if UNITY_EDITOR
        [Button]
        public void GeneratePopupEnum()
        {
            var popups = UIPopupDatabase.instance.database;
            var rs = TextToEnum(popups);
            string path = "Assets/GameAssets/Scripts/UI/Popups/PopupEnum.cs";
            File.WriteAllText(path, rs);
            AssetDatabase.Refresh();
        }

        private string TextToEnum(List<UIPopupLink> popups)
        {
            var rs = new StringBuilder();
            rs.Append("public enum PopupEnum\r\n{");
            rs.Append("\n");
            foreach (var popup in popups)
            {
                rs.Append($"        {popup.prefabName}");
                rs.Append(",");
                rs.Append("\n");
            }
            rs.Append("}");
            return rs.ToString();
        }
#endif
    }


}
