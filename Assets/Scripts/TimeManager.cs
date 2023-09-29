using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    private float startTime;
    private float gameTime;
    private bool isGameOver => PlayerControll.isDie;
    private string filePath;
    public bool IsSave = false;
    private ScoreData scoreData = new ScoreData();

    public static TimeManager instance { get; private set; }
    public ScoreData ScoreData { get => scoreData; set => scoreData = value; }

    private void Awake()
    {
        Debug.Log("ok");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        LoadData();
    }
    private void Start()
    {
        filePath = Application.persistentDataPath + "/TimePlay.txt";
        startTime = Time.time;

    }

    private void Update()
    {   Debug.Log(isGameOver && !IsSave);
        if (isGameOver && !IsSave)
        {
            Debug.Log("tinh time");
             gameTime = Time.time - startTime;
            if (gameTime > ScoreData.highestScore)
            {
               
                ScoreData.highestScore = gameTime;
            }
            ScoreData.currentScore = gameTime;
            SaveData();
            IsSave = true;
        }
    }
    public void LoadData()
    {
        
        if (File.Exists(filePath))
        {
            StreamReader reader = new StreamReader(filePath);
            string[] data = File.ReadAllLines(filePath);
            if (data.Length >= 2)
            {
                if (float.TryParse(data[0], out float currentScore))
                {
                    ScoreData.currentScore = currentScore;
                }
                if (float.TryParse(data[1], out float highestScore))
                {
                    ScoreData.highestScore = highestScore;
                }
            }
            
            reader.Close();
        }

    }

    public void SaveData()
    {
        string[] lines = new string[]
            {
        scoreData.currentScore.ToString("F2"),
        scoreData.highestScore.ToString("F2")
            };

        File.WriteAllLines(filePath, lines);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        startTime = Time.time;
        StartCoroutine(DelayedFunction());

    }

    public bool IsHome()
    {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }

    public void LoadScene(int _index)
    {
        SceneManager.LoadScene(_index);
        IsSave = false;
        startTime = Time.time;

    }

    private IEnumerator DelayedFunction()
    {
        // ??i 1 frame
        yield return null;
        IsSave = false;
        LoadData();
    }

}


[System.Serializable]
public class ScoreData
{
    public float currentScore;
    public float highestScore;
}
