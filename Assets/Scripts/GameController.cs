using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private string status = "character";
    private int score = 0;
    private float timer = 0f;
    private bool timerOn = false;
    private int numberOfWave = 0;
    private int numberOfKill = 0;
    private bool timerAsStarted = false;
    private float life = 0f;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            timer = Time.deltaTime + timer;
        }
        if (status == "level")
        {
            GameObject.FindGameObjectWithTag("Timer_Text").GetComponent<Text>().text = GetFormatedTimer();
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCore>().isPlayerDead())
            {
                PauseTimer();
                SceneManager.LoadScene("GameOver");
                Debug.LogError("Load GameOver");
                status = "gameover";
            }
        }
    }

    private void LaunchTimer()
    {
        timer = 0f;
        timerOn = true;
    }

    private void OnLevelWasLoaded(int level)
    {
        var scene = SceneManager.GetActiveScene();
        if (scene.name == "GameOver")
        {
            status = "gameover";
             GameObject.FindGameObjectWithTag("Waves_stats").GetComponent<TextMeshPro>().text = "" + numberOfWave;
             GameObject.FindGameObjectWithTag("Score_stats").GetComponent<TextMeshPro>().text = "" + (score + (numberOfWave * 10) + Mathf.CeilToInt(timer / 10));
             GameObject.FindGameObjectWithTag("Kills_stats").GetComponent<TextMeshPro>().text = "" + numberOfKill;
             GameObject.FindGameObjectWithTag("Time_stats").GetComponent<TextMeshPro>().text = GetFormatedTimer();
        }
        else if (scene.name == "Map")
        {
            status = "level";
            GameObject.FindGameObjectWithTag("Replay_Button").GetComponent<Button>().onClick.AddListener(Restart);
            var newplayer = GameObject.Instantiate(player);
            newplayer.transform.position = new Vector3(296, -8, 0);
            var camera = GameObject.FindGameObjectWithTag("MainCamera");
            GameObject.FindGameObjectWithTag("Tree_parallax").GetComponent<Parallax>().cam = camera;
            GameObject.FindGameObjectWithTag("Treeshadow_parallax").GetComponent<Parallax>().cam = camera;
            GameObject.FindGameObjectWithTag("Plantshadow_parallax").GetComponent<Parallax>().cam = camera;
            GameObject.FindGameObjectWithTag("Front_parallax").GetComponent<Parallax>().cam = camera;
            newplayer.GetComponent<PlayerCore>().shotpointLeft = GameObject.FindGameObjectWithTag("pointleft").transform;
            newplayer.GetComponent<PlayerCore>().shotpointRight = GameObject.FindGameObjectWithTag("pointright").transform;
            UpdateLife(newplayer.GetComponent<PlayerCore>().maxHealth);
            LaunchTimer();
        }
        else if (scene.name == "Character Selection")
        {
            status = "character";
        }
    }

    private void PauseTimer()
    {
        timerOn = false;
    }
    
    private void ResumeTimer()
    {
        timerOn = true;
    }

    public string GetFormatedTimer()
    {
        var second = timer;
        var minute = Mathf.FloorToInt(timer / 60);
        var hour = Mathf.FloorToInt(minute / 60);

        return (hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00"));
    }

    public int GetScore()
    {
        return (score);
    }

    public void AddKill()
    {
        numberOfKill += 1;
        score += 10;
        GameObject.FindGameObjectWithTag("Score_Text").GetComponent<Text>().text = "Score : " + score;
    }

    public void updateWave(int wave)
    {
        numberOfWave = wave;
        GameObject.FindGameObjectWithTag("Waves_Text").GetComponent<Text>().text = "Waves : " + numberOfWave;
    }

    public void Restart()
    {
        numberOfKill = 0;
        numberOfWave = 0;
        
        score = 0;
        SceneManager.LoadScene("Map");
        var newplayer = GameObject.Instantiate(player);
        newplayer.transform.position = new Vector3(296, -8, 0);
    }

    public void SetPlayer(GameObject selectedPlayer)
    {
        player = selectedPlayer;
    }

    public void UpdateLife(float updatedLife)
    {
        life = updatedLife;
        GameObject.FindGameObjectWithTag("Life_Text").GetComponent<Text>().text = "Life : " + life;
    }

}
