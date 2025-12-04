using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.UI;


public class PlayButtonSound : MonoBehaviour
{
    Button btn;
    UIButton uiBtn;
    private void Awake()
    {
        btn = GetComponent<Button>();
        uiBtn = GetComponent<UIButton>();
        if(btn != null)
            btn.onClick.AddListener(OnClick);

        if (uiBtn != null)
            uiBtn.onClickEvent.AddListener(OnClick);

    }

    private void OnClick()
    {
        SoundCtrl.Instance.PlaySFXAsync("ButtonClick").Forget();
    }

    private void OnDestroy()
    {
        if(btn != null)
            btn.onClick.RemoveAllListeners();
        if (uiBtn != null)
            uiBtn.onClickEvent.RemoveAllListeners();
    }
}
