using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHandler
{
    private const string saveDataKey = "saveData";
    private const string isNewPlayerKey = "isNewPlayer";
    public static void SaveGameData(int[] data)
    {
        PlayerPrefsX.SetIntArray(saveDataKey, data);
    }

    public static int[] LoadGameData()
    {
        return PlayerPrefsX.GetIntArray(saveDataKey, 0, 0);
    }

    public static bool IsNewPlayer
    {
        get
        {
            return PlayerPrefsX.GetBool(isNewPlayerKey, true);
        }
        set
        {
            PlayerPrefsX.SetBool(isNewPlayerKey, false);
        }
    }
}
