using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    public GameObject spawn1;
    public GameObject spawn2;
    public int max_enemies = 100;
    public GameObject[] enemy_prefab;

    private bool canSpawn1;
    private bool canSpawn2;
    private int currently_spawn = 0;
    private int currently_waves = 0;
    private int max_spawn = 0;
    private float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        timer = timer + Time.deltaTime;
        if (enemies < max_enemies && currently_spawn < max_spawn)
        {
            SpawnEnnemies(Mathf.Clamp(max_spawn - currently_spawn, 1, max_enemies - enemies));
        }
        else if (timer >= 60)
        {
            currently_waves += 1;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().updateWave(currently_waves);
            max_spawn += (int)(currently_waves * 1.5 + 2);
            timer = 0f;
        }
        else if (enemies == 0 && currently_spawn == max_spawn)
        {
            currently_waves += 1;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().updateWave(currently_waves);
            currently_spawn = 0;
            max_spawn = (int)(currently_waves * 1.5 + 2);
        }
    }

    private void SpawnEnnemies(int number)
    {
        canSpawn1 = !spawn1.GetComponent<Spawnpoint>().isPlayerDetected();
        canSpawn2 = !spawn2.GetComponent<Spawnpoint>().isPlayerDetected();

        for (int i = 0; i < number; i++)
        {
            var nb_enemy = (int) Random.Range(0, enemy_prefab.Length);
            var last_enemy = GameObject.Instantiate(enemy_prefab[nb_enemy]);
            if (canSpawn1 && canSpawn2)
            {
                if (Random.Range(0, 2) < 0.5)
                {
                    last_enemy.GetComponent<Transform>().position = new Vector3((spawn1.GetComponent<Transform>().position.x + Random.Range(-50, 50)), spawn1.GetComponent<Transform>().position.y, 0);
                }
                else
                {
                    last_enemy.GetComponent<Transform>().position = new Vector3((spawn2.GetComponent<Transform>().position.x + Random.Range(-50, 50)), spawn2.GetComponent<Transform>().position.y, 0);
                }
            }
            else if (canSpawn1)
            {
                last_enemy.GetComponent<Transform>().position = new Vector3((spawn1.GetComponent<Transform>().position.x + Random.Range(-50, 50)), spawn1.GetComponent<Transform>().position.y, 0);
            }
            else
            {
                last_enemy.GetComponent<Transform>().position = new Vector3((spawn2.GetComponent<Transform>().position.x + Random.Range(-50, 50)), spawn2.GetComponent<Transform>().position.y, 0);
            }
            currently_spawn += 1;
        }
    }

    public int GetWaves()
    {
        return (currently_waves);
    }
}
