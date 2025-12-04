

using Gameplay;
using StansAssets.Foundation.Patterns;


public class Config
{
    public bool isMusic;
    public bool isSound;
    public bool isVibrate;
    public Config()
    {
        isMusic = true;
        isSound = true;
        isVibrate = true;
    }
}

public class ConfigSave
{
    static string SaveKey = "config_data";

    public static Config GetData()
    {
        return ES3.Load(SaveKey, new Config());
    }
    public static bool GetSound()
    {
        return ES3.Load(SaveKey, new Config()).isSound;
    }

    public static void SetSound(bool value)
    {
        var data = GetData();
        data.isSound = value;
        ES3.Save(SaveKey, data);
    }

    public static bool GetMusic()
    {
        return ES3.Load(SaveKey, new Config()).isMusic;
    }

    public static void SetMusic(bool value)
    {
        var data = GetData();
        data.isMusic = value;
        ES3.Save(SaveKey, data);
    }
    public static bool GetVibrate()
    {
        return ES3.Load(SaveKey, new Config()).isVibrate;
    }

    public static void SetVibrate(bool value)
    {
        var data = GetData();
        data.isVibrate = value;
        ES3.Save(SaveKey, data);
    }
}

[System.Serializable]
public class DataSaver
{
    public int level;
    public int coin;
    public int expLevel = 1;
    public int exp = 0;
    public DataSaver()
    {
        level = 1;
        coin = 200;
        expLevel = 1;
        exp = 0;
    }
}
public class GameplaySaveData
{
    static string dataSaveKey = "gameplay_data";
    public static int startTube = 5;
    public static int startGun = 4;
    public static DataSaver GetData()
    {
        return ES3.Load(dataSaveKey, new DataSaver());
    }
    public static int GetLevelId()
    {
        return ES3.Load(dataSaveKey, new DataSaver()).level;
    }

    public static void SetLevelId(int id)
    {
        if (id < 0)
            id = 0;
        var data = GetData();
        data.level = id;
        ES3.Save(dataSaveKey, data);
    }

    public static void IncreaseLevel(int value)
    {
        var data = GetData();
        data.level += value;
        ES3.Save(dataSaveKey, data);
    }

    public static int GetCoin()
    {
        return ES3.Load(dataSaveKey, new DataSaver()).coin;
    }

    public static void AddCoin(int coin)
    {
        var data = GetData();
        data.coin += coin;
        if (data.coin <= 0)
            data.coin = 0;
        ES3.Save(dataSaveKey, data);
        StaticBus<EventCoinChange>.Post(new());
    }

    public static void SetCoin(int coin)
    {
        var data = GetData();
        data.coin = coin;
        ES3.Save(dataSaveKey, data);
    }

    public static bool IsEnough(int cost)
    {
        var coin = GetCoin();
        return coin >= cost;
    }
   
    public static string GetLevelString()
    {
        return (GetLevelId() + 1).ToString();
    }

    public static int GetExpLevel()
    {
        return ES3.Load(dataSaveKey, new DataSaver()).expLevel;
    }

    public static void SetExpLevel(int v)
    {
        var data = GetData();
        data.expLevel = v;
        ES3.Save(dataSaveKey, data);
    }

    public static void AddExpLevel(int v)
    {
        var data = GetData();
        data.expLevel += v;
        ES3.Save(dataSaveKey, data);
    }

    public static int GetExp()
    {
        return ES3.Load(dataSaveKey, new DataSaver()).exp;
    }

    public static void SetExp(int v)
    {
        var data = GetData();
        data.exp = v;
        ES3.Save(dataSaveKey, data);
    }
}
