using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpikeBullet : MonoBehaviour
{
    public Vector3 moveDir;
    public float speed;
    public float spawnInterval;
    public GameObject spikeSinglePrefab;
    float spawnTimer;

    List<Monster> hittedMonsters;

    private void Awake()
    {
        hittedMonsters = new List<Monster>();
    }

    private void FixedUpdate()
    {
        transform.position += moveDir * Time.deltaTime * speed ;

        spawnTimer += Time.deltaTime;
        if (spawnTimer>spawnInterval)
        {
            spawnTimer = 0;
            GameObject spike = Instantiate(spikeSinglePrefab);
            spike.transform.position = transform.position;
            Destroy(spike, 1f);
            Vector3 normalDir = Vector3.Cross(Vector3.up, moveDir);
            spike.transform.position += normalDir * Random.Range(-1f, 1) * .5f + moveDir*.5f;
            spike.transform.localScale = Vector3.zero;
            spike.transform.DOScale(.3f, .35f);
        }


        Monster[] monsters = MonsterManager.instance.monsters;
        foreach (Monster monster in monsters)
        {
            if (monster.gameObject)
            {
                if (Vector3.Distance(transform.position, monster.transform.position) < 1)
                {
                    if (!hittedMonsters.Contains(monster))
                    {
                        hittedMonsters.Add(monster);
                        monster.Knocked();
                    }
                }
            }
            
        }                
    }
}
