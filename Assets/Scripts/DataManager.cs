using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public string PlayerName
    {
        get
        {
            return PlayerPrefs.GetString("PlayerName", "");
        }
        set
        {
            PlayerPrefs.SetString("PlayerName", value);
        }
    }
    public int Score
    {
        get
        {
            return PlayerPrefs.GetInt("Score", 0);   
        }
        set
        {
            PlayerPrefs.SetInt("Score", value);
        }
    }
}
