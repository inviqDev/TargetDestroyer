using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public Dictionary<string, int> ScoreboardDictionary { get; private set; } = new(5);
    private int _scoreboardCount = 5;
    private string _saveFilePath;

    private TextMeshProUGUI[] _scoreboardSlotTexts;

    private int _startValue = 0;

    public int CurrentScore { get; private set; }
    public int scoreIncrement = 1;



    private void Awake()
    {
        _saveFilePath = Application.persistentDataPath + "/scoreboardSaveFile";

        if (!File.Exists(_saveFilePath))
        {
            FillScoreboardWithDefaultValues();
            SaveDataToLocalFile();
        }
        else
        {
            LoadSavedDataFromLocalFile();
        }

        SortScoreboardDictionary();
        SetUpScoreboardValues();

        ResetPlayerCurrentScoreValue();
    }


    private void FillScoreboardWithDefaultValues()
    {
        int startIndex = 1;
        for (int i = 0; i < _scoreboardCount; i++)
        {
            string defaultText = $"{startIndex}. Empty slot";
            ScoreboardDictionary.Add(defaultText, 0);

            startIndex++;
        }
    }

    private void SetUpScoreboardValues()
    {
        StartMenuUIHandler startMenuUIHandler = FindObjectOfType<StartMenuUIHandler>();
        if (startMenuUIHandler != null)
        {
            Transform scoreboardSlotsParent = startMenuUIHandler.GetScoreboardSlotsParentReference();

            int textFieldAmount = scoreboardSlotsParent.childCount;
            _scoreboardSlotTexts = new TextMeshProUGUI[textFieldAmount];

            for (int i = 0; i < textFieldAmount; i++)
            {
                Transform nextChild = scoreboardSlotsParent.GetChild(i);
                _scoreboardSlotTexts[i] = nextChild.GetComponent<TextMeshProUGUI>();
            }

            int index = 0;
            foreach (var score in ScoreboardDictionary)
            {
                bool emptySlot = score.Value == 0;
                string scoreboardText;

                scoreboardText = emptySlot ? $"{score.Key}" : $"{index + 1}. {score.Key} : {score.Value} pts.";
                _scoreboardSlotTexts[index].text = scoreboardText;

                index++;
            }
        }
        else
        {
            Debug.Log("StartMenuUIHandler component is not found in the game !");
        }
    }



    private void SortScoreboardDictionary()
    {
        IEnumerable<KeyValuePair<string, int>> sortedEnumerable = ScoreboardDictionary.OrderByDescending(x => x.Value);
        ScoreboardDictionary = sortedEnumerable.ToDictionary(x => x.Key, x => x.Value);
    }



    public void ResetPlayerCurrentScoreValue()
    {
        CurrentScore = _startValue;
    }

    public void IncreaseCurrentScoreValue()
    {
        CurrentScore += scoreIncrement;
    }

    public void SaveScore(string playerName)
    {
        KeyValuePair<string, int> minScorePair = 
            ScoreboardDictionary.Where(x => x.Value == ScoreboardDictionary.Values.Min()).First();

        ScoreboardDictionary.Remove(minScorePair.Key);
        ScoreboardDictionary.Add(playerName, CurrentScore);

        SortScoreboardDictionary();
        SaveDataToLocalFile();
    }





    [Serializable]
    public class SaveData
    {
        public Dictionary<string, int> scoreboardDictionary;
    }

    public void SaveDataToLocalFile()
    {
        SaveData data = new SaveData();
        data.scoreboardDictionary = ScoreboardDictionary;

        var saveThisData = JsonConvert.SerializeObject(data);
        File.WriteAllText(_saveFilePath, saveThisData);
    }

    public void LoadSavedDataFromLocalFile()
    {
        var dataFromJson = File.ReadAllText(_saveFilePath);
        SaveData data = JsonConvert.DeserializeObject<SaveData>(dataFromJson);

        ScoreboardDictionary = data.scoreboardDictionary;
    }
}