using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace Gamepplay.UI
{
    public class ViewMainmenu : MonoBehaviour
    {

        public UIButton btnSetting;

        private void Awake()
        {
            btnSetting.onClickEvent.AddListener(OnBtnSettingClick);
        }
        private void OnDestroy()
        {
            btnSetting.onClickEvent.RemoveListener(OnBtnSettingClick);
        }

        private void OnBtnSettingClick()
        {
            //doozy stream
            Signal.Send(StreamId.FlowMainmenu.Setting);
        }
    }
}