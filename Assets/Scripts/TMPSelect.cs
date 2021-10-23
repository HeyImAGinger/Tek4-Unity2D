using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPSelect : MonoBehaviour
{
    public GameObject[] Charac;
    public GameObject[] CharacPrefab;
    public int nb = 0;

    public void NextCharac() {
        Charac[nb].SetActive(false);
        nb = (nb + 1) % Charac.Length;
        Charac[nb].SetActive(true);
    }

    public void BackCharac() {
        Charac[nb].SetActive(false);
        nb--;
        if (nb < 0)
            nb += Charac.Length;
        Charac[nb].SetActive(true);
    }

    private void LoadPlayerPrefs() {
        nb = PlayerPrefs.GetInt("CharacOption");
    }

    private void SavePlayerPrefs() {
        PlayerPrefs.SetInt("CharacOption", nb);
    }

    public void StartGame() {
        PlayerPrefs.SetInt("CharacOption", nb);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().SetPlayer(CharacPrefab[nb]);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().Restart();
    }
}
