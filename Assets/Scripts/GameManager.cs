using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public InputField nameField;
    public string PlayerName;

    public string BestPlayerName;
    public int BestScores;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadPlayerData();
    }

    public void SubmitPlayerName()
    {
        PlayerName = nameField.text;
    }
    public void CompareScores(int scores)
    {
        if (scores > BestScores)
        {
            BestScores = scores;
            SaveBestPlayer();
        }
    }

    class SaveData
    {
        public int BestScore;
        public string PlayerName;
    }

    public void SaveBestPlayer()
    {
        SaveData data = new SaveData();
        data.BestScore = BestScores;
        data.PlayerName = PlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScores = data.BestScore;
            BestPlayerName = data.PlayerName;
        }
    }
}
