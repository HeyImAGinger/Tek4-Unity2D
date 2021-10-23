using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public void Score()
    {
        SceneManager.LoadScene("Game over scoreboard");

    }
    public void Continue()
    {
        SceneManager.LoadScene("Character Selection");

    }
    public void Quitgame()
    {
        Application.Quit();
    }
 
    IEnumerator LoadLevel()
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Character Selection");
    }
}
