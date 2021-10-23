using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    private Transform Container;
    private Transform Template;
    public GameObject Back;
    private List<Transform> highScoreEntryTransformList;

    private void Awake() {
        Container = transform.Find("HighScoreContainer");
        Template = Container.Find("HighScoreTemplate");
        Template.gameObject.SetActive(false);
    }

    //Display Score
    public void DisplayMyHighScore() {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScore highScore = JsonUtility.FromJson<HighScore>(jsonString);

        //sort score
        for (int i = 0; i < highScore.highScoreEntryList.Count; i++) {
            for (int j = i + 1; j < highScore.highScoreEntryList.Count; j++) {
                if (highScore.highScoreEntryList[j].score > highScore.highScoreEntryList[i].score) {
                    HighScoreEntry tmp = highScore.highScoreEntryList[i];
                    highScore.highScoreEntryList[i] = highScore.highScoreEntryList[j];
                    highScore.highScoreEntryList[j] = tmp;
                }
            }
        }

        //display and create scores
        highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highScore.highScoreEntryList) {
            if (highScoreEntryTransformList.Count < 10) {
                CreateScore(highScoreEntry, Container, highScoreEntryTransformList);
            }
        }
    }

    public void DisplayScore() {
        Back.gameObject.SetActive(true);
        Container.gameObject.SetActive(true);
        Template.gameObject.SetActive(false);
    }

    public void HideScore() {
        Back.gameObject.SetActive(false);
        Container.gameObject.SetActive(false);
        Template.gameObject.SetActive(false);
    }

    //create list of scores
    private class HighScore {
        public List<HighScoreEntry> highScoreEntryList;
    }

    //entry score
    [System.Serializable]
    private class HighScoreEntry {
        public int score;
        public string name;
    }

    private void CreateScore(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList) {
        float Height = 8f;
        int rank = 0;
        string rankString;
        Transform myTransform = Instantiate(Template, container);
        RectTransform rect = myTransform.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(33, -Height * transformList.Count);
        myTransform.gameObject.SetActive(true);

        rank = transformList.Count + 1;

        //check rank position and change rankString value
        switch (rank) {
            default:
                rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        //set value to the HighScoreTemplate
        int score = highScoreEntry.score;
        myTransform.Find("Position").GetComponent<Text>().text = rankString;
        myTransform.Find("Score").GetComponent<Text>().text = score.ToString();
        myTransform.Find("Name").GetComponent<Text>().text = highScoreEntry.name;

        //change color for the top 3
        if (rank == 1) {
            myTransform.Find("Position").GetComponent<Text>().color = Color.red;
            myTransform.Find("Score").GetComponent<Text>().color = Color.red;
            myTransform.Find("Name").GetComponent<Text>().color = Color.red;
        }

        if (rank == 2) {
            myTransform.Find("Position").GetComponent<Text>().color = new Color(1.0f, 0.64f, 0.0f);
            myTransform.Find("Score").GetComponent<Text>().color = new Color(1.0f, 0.64f, 0.0f);
            myTransform.Find("Name").GetComponent<Text>().color = new Color(1.0f, 0.64f, 0.0f);
        }

        if (rank == 3) {
            myTransform.Find("Position").GetComponent<Text>().color = Color.yellow;
            myTransform.Find("Score").GetComponent<Text>().color = Color.yellow;
            myTransform.Find("Name").GetComponent<Text>().color = Color.yellow;
        }

        //set Background score 1/2
        myTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);

        transformList.Add(myTransform);
    }

    //add and save score to the playerprefs
    public void AddScore(int score, string name) {
        HighScoreEntry highScoreEntry = new HighScoreEntry {score = score, name = name};

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScore highScore = JsonUtility.FromJson<HighScore>(jsonString);

        highScore.highScoreEntryList.Add(highScoreEntry);
        string json = JsonUtility.ToJson(highScore);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
}
