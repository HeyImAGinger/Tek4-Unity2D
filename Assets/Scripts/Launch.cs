using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    [SerializeField] private HighScoreTable highScore;

    private void Start() {
        //Call Input and scoreboard
        // highScore.HideScore();
        // UI_input.StaticShow("Enter Player Name", "", "ABCDEFGHIJKLMNOPQRSTUVXYWZ", 3, (string name) => {
            // highScore.AddScore(9999999, name);
            highScore.DisplayScore();
            highScore.DisplayMyHighScore();
        // });
        //Call scoreboard
    }
}
