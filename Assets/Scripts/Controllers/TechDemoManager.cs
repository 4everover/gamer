using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechDemoManager : MonoBehaviour
{
    [SerializeField] GameObject rabbit;
    [SerializeField] GameObject rabbitBoss;

    List<GameObject> summonedEnemies;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        summonedEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var enemy = Instantiate(rabbit, (player.transform.position + new Vector3(10, 0, 0)), Quaternion.identity) as GameObject;
            summonedEnemies.Add(enemy);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var enemy = Instantiate(rabbitBoss, (player.transform.position + new Vector3(10, 0, 0)), Quaternion.identity) as GameObject;
            summonedEnemies.Add(enemy);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (GameObject enemy in summonedEnemies)
            {
                if (enemy) enemy.GetComponent<Health>().TakeDamage(99999);
            }
            summonedEnemies.Clear();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.GetComponent<Health>().ResetHealth();
        }
    }
}
