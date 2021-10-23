using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Load_Character : MonoBehaviour
{
    public GameObject[] Charac;
    public Transform spawn;

    void Start()
    {
        int nb = PlayerPrefs.GetInt("CharacOption");
        GameObject prefab = Charac[nb];
        GameObject clone = Instantiate(prefab, spawn.position, Quaternion.identity);
    }

}
