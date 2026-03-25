using UnityEngine;

public enum BoosterType
{
    Bomb,
    ExtraMoves,
    DoubleReward
}

public enum CurrencyType
{
    Soft,
    Hard
}

[CreateAssetMenu(menuName = "Game/Booster Data", fileName = "BoosterData")]
public class BoosterData : ScriptableObject
{
    [Header("Identity")]
    public BoosterType type;
    public string displayName;
    public Sprite icon;

    [Header("Unlock")]
    public int unlockLevel = 1;

    [Header("Cost")]
    public CurrencyType currencyType;
    public int cost;
}
