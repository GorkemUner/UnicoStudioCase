using UnityEngine;

public class PlayerPrefsManager : Singleton<PlayerPrefsManager>
{
    private PlayerData playerData;
    public PlayerData PlayerData
    {
        get
        {
            if (playerData == null)
                playerData = new PlayerData();
            return playerData;
        }
    }
}

public class PlayerData
{
    private int level = 0;

    public PlayerData()
    {
        Load();
    }

    private void Load()
    {
        level = PlayerPrefs.GetInt("level", level);
    }

    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            PlayerPrefs.SetInt("level", level);
        }
    }
}