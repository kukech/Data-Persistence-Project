using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour //singleton
{
    public static GameManager Instance;
    public InputField playerNameInputField;
    public string playerName { get; private set; }
    public string secondPlayerName;
    public int bestScore { get; set; }
    private void Awake()
    {
        if(Instance!= null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadHighScore();
        DontDestroyOnLoad(gameObject);
    }
    public void StartGame()
    {
        if (string.IsNullOrEmpty(playerNameInputField.text))
        {
            secondPlayerName = "Player";
        }
        else secondPlayerName = playerNameInputField.text;
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int bestScore;
    }
    public void SaveHighScore()
    {
        playerName = secondPlayerName;
        SaveData saveData = new SaveData();
        saveData.playerName = secondPlayerName;
        saveData.bestScore = bestScore;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestScore = data.bestScore;
            playerName = data.playerName;
        }
        else bestScore = 0;
    }
}
