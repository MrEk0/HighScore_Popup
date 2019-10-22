using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeagueTable : MonoBehaviour
{
    [SerializeField] int numberOfPlayersToShow = 5;
    [SerializeField] int placeForPlayer = 2;
    [SerializeField] float defaultOffset = 70f;
    [SerializeField] GameObject playerResultPrefab = null;
    [SerializeField] GameObject emphasizedPlayerResult = null;
    [SerializeField] Transform defaultPosition = null;
    [SerializeField] RectTransform playerResStartPos = null;

    private const string DATAFILEPATH = "data.json";

    private List<PlayerData> playerDataList=new List<PlayerData>();
    private List<GameObject> listOfResults=new List<GameObject>();

    private ScrollContent scrollContent;

    private void Awake()
    {
        AddPlayerResult(8965, "Nick", "Martin");
        AddPlayerResult(4265, "Bob", "Lowerns");
        AddPlayerResult(5986, "Michael", "Okit");
        AddPlayerResult(2384, "Robert", "Yin");
        AddPlayerResult(7896, "Lionel", "Vinchester");
        AddPlayerResult(2354, "Tina", "Rovinson");
        AddPlayerResult(1258, "Ron", "Li");
        AddPlayerResult(3647, "James", "Duglas");
        AddPlayerResult(9861, "Alex", "Woley");
        AddPlayerResult(4035, "Ninotini", "Bizarre");
        AddPlayerResult(7030, "Zerto", "Genuine");
        AddPlayerResult(8065, "Walen", "Gold");
        AddPlayerResult(9614, "Vin", "Diesel");
        AddPlayerResult(10354, "Frank", "Gogo");

        scrollContent = playerResStartPos.GetComponent<ScrollContent>();
    }

    public void LoadData()
    {
        string filePath = Path.Combine( Application.dataPath, DATAFILEPATH);

        if(File.Exists(filePath))
        {
            string playerDataAsJson = File.ReadAllText(filePath);
            Highscores highscore = JsonUtility.FromJson<Highscores>(playerDataAsJson);
            playerDataList = highscore.playerData;
            placeForPlayer = highscore.playerPlace;
            numberOfPlayersToShow = highscore.playersToShow;
        }
        else
        {
            throw new FileNotFoundException();
        }

        SortData();
        CreateLeagueTable(playerDataList);

        scrollContent.SetContentHigh(playerResultPrefab, defaultOffset);
    }

    public void SaveData()
    {
        Highscores highScore = new Highscores() { playerData = playerDataList,
            playerPlace = placeForPlayer,
            playersToShow =numberOfPlayersToShow };

        string playersDataToJson = JsonUtility.ToJson(highScore);
        string savePath = Path.Combine(Application.dataPath, DATAFILEPATH);

        File.WriteAllText(savePath, playersDataToJson);
    }

    private void SortData()
    {
        for (int i = 0; i < playerDataList.Count; i++)
        {
            for (int j = i + 1; j < playerDataList.Count; j++)
            {
                if (playerDataList[i].points < playerDataList[j].points)
                {
                    PlayerData temp = playerDataList[i];
                    playerDataList[i] = playerDataList[j];
                    playerDataList[j] = temp;
                }
            }
        }
    }

    private void CreateLeagueTable(List<PlayerData> playerData)
    {
        if (numberOfPlayersToShow > playerData.Count)
        {
            numberOfPlayersToShow = playerData.Count;
        }

        InstantiateResult(emphasizedPlayerResult, defaultPosition, playerData, placeForPlayer - 1, 0);   

        for (int i = 0; i < numberOfPlayersToShow; i++)
        {
            if (i == placeForPlayer - 1)
            {
                InstantiateResult(emphasizedPlayerResult, playerResStartPos, playerData, i, i);
            }
            else
            {
                InstantiateResult(playerResultPrefab, playerResStartPos, playerData, i, i);
            }
        }

    }

    private void InstantiateResult(GameObject prefabToInst, Transform startPoint, List<PlayerData> playerData, int place,
         int placeInChain)
    {
        Vector3 startPosition = new Vector3(startPoint.position.x, startPoint.position.y-defaultOffset * placeInChain);

        GameObject playerResult = Instantiate(prefabToInst, startPosition, Quaternion.identity, startPoint);

        listOfResults.Add(playerResult);

        playerResult.GetComponent<PlayerResult>().SetupResult(place,
            playerData[place].name,
            playerData[place].lastName,
            playerData[place].points);
    }


    private void AddPlayerResult(int points, string name, string lastName)
    {
        PlayerData playerData = new PlayerData() { points = points, name = name, lastName = lastName };

        playerDataList.Add(playerData);

        SaveData();

    }

    public void CleanLeagueTable()
    {
        foreach(GameObject result in listOfResults)
        {
            Destroy(result);
        }
    }

    private class Highscores
    {
        public List<PlayerData> playerData;
        public int playerPlace;
        public int playersToShow;
    }

    [Serializable]
    private class PlayerData
    {
        public int points;
        public string name;
        public string lastName;
    }
}


