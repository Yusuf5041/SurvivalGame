using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//*********************************************
//MonoBehavior used from UnityEngine library
//*********************************************

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public Animator anim;

    public float score, health, hunger, stress, currentHighest;
    public int gameCount;

    private void Awake()
    {
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
            StartCoroutine("PlayLimit");
        }


        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        string filename = Application.persistentDataPath + "/playInfo.dat";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filename, FileMode.OpenOrCreate);

        PlayerData pd = new PlayerData
        {
            healthAverage = health,
            hungerAverage = hunger,
            stressAverage = stress,
            gamesPlayed = gameCount,
            highScore = currentHighest
        };

        bf.Serialize(file, pd);
        file.Close();
        Debug.Log("game saved");
    }

    public void Load()
    {
        string filename = Application.persistentDataPath + "/playInfo.dat";
        if (File.Exists(filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filename, FileMode.Open);
            PlayerData pd = (PlayerData)bf.Deserialize(file);
            file.Close();

            health = pd.healthAverage;
            hunger = pd.hungerAverage;
            stress = pd.stressAverage;
            currentHighest = pd.highScore;
            gameCount = pd.gamesPlayed;
            Debug.Log("game loaded");
        }
        else
        {
            health = 0;
            hunger = 0;
            stress = 0;
            currentHighest = 0;
            gameCount = 0;
        }
    }

    public void ResetPlayer()
    {
        string filename = Application.persistentDataPath + "/playInfo.dat";
        File.Delete(filename);

        Debug.Log("Player reset");
    }

    public void CompareScores(float gameScore)
    {
        if (gameScore > currentHighest)
        {
            currentHighest = gameScore;
        }
    }

    IEnumerator PlayLimit()
    {
        yield return new WaitForSecondsRealtime(60*15);
        anim.Play("Exit Panel In");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Quit()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

    [Serializable]
    class PlayerData
    {
        public float healthAverage { get; set; }
        public float hungerAverage { get; set; }
        public float stressAverage { get; set; }

        public float highScore { get; set; }
        public int gamesPlayed { get; set; }

        public PlayerData()
        {

        }


    }
}
