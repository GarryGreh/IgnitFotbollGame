using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorContentLevel : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject[] bonuses;

    private List<GameObject> currentContent = new List<GameObject>();
    private List<GameObject> removeListContent = new List<GameObject>();

    private float currentPosZSpawn;
    private int bonusChance = 6;
    private int[] spawnLines = { -3, 0, 3 };
    private int[] mixLine;

    private bool bonusEnabled = false;
    private LevelGenerator lvlGen;

    private void Start()
    {
        //mixLine = MixLine(spawnLines);
        //for(int i = 0 ; i < mixLine.Length; i++)
        //{
        //    Debug.Log(mixLine[i]);
        //}
        lvlGen = GetComponent<LevelGenerator>();
        Spawn();
        //for (int i = 0; i < 25; i++)
        //{
        //    SpawnContent();
        //}
    }
    private void Update()
    {
        //здесь читать позицию игрока и удалять всё что за спиной

    }
    public void Spawn()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnContent();
        }
    }
    public void SpawnContent()
    {
        currentPosZSpawn += RandomDistanceZ();
        int numObstacles = RandonNumberObstacles();
        if(numObstacles < 3)
        {
            bonusEnabled = ChanceBonus();
        }
        else if(numObstacles >= 3)
        {
            bonusEnabled = false;

        }
        mixLine = MixLine(spawnLines);
        int spawnLength = 0;
        if(numObstacles > 0)
        {
            spawnLength += numObstacles;            
        }
        if (bonusEnabled)
        {
            spawnLength++;
        }
        if(spawnLength > 0)
        {
            Vector3 spawnPos = new Vector3(0.0f, 0.0f, currentPosZSpawn);

            for(int i = 0; i < spawnLength; i++)
            {
                spawnPos = new Vector3(mixLine[i], 0.0f, currentPosZSpawn);

                if(numObstacles > 0 && i < numObstacles)
                {
                    GameObject obs = Instantiate(obstacles[RandomObstacles()], spawnPos, Quaternion.identity);
                    currentContent.Add(obs);

                    if (obs.GetComponent<Enemy>())
                    {
                        obs.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                    }
                }
                else if(numObstacles <= 0 ||  i >= numObstacles)
                {
                    if (bonusEnabled)
                    {
                        GameObject _bonus = Instantiate(bonuses[RandomBonus()], spawnPos, Quaternion.identity);
                        currentContent.Add(_bonus);

                        if(bonusChance > 0)
                        {
                            bonusChance--;
                        }
                        else
                        {
                            bonusChance = 6;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
    //public void AddToDeletionList()
    //{
    //    foreach(GameObject current in currentContent)
    //    {
    //        if(lvlGen.playerPos.position.z > current.transform.position.z)
    //        {
    //           removeListContent.Add(current);
    //        }
    //    }
    //}
    public void DeleteContent()
    {
        for (int i = 0; i < 10; i++)
        {
            Destroy(currentContent[0]);
            currentContent.RemoveAt(0);
        }
        
        //foreach (GameObject current in currentContent)
        //{
        //    if (lvlGen.playerPos.position.z > current.transform.position.z)
        //    {
        //        removeListContent.Add(current);
        //    }
        //}
        //foreach (GameObject remove  in removeListContent)
        //{
        //    currentContent.Remove(remove);
        //    removeListContent.Remove(remove);
        //    Destroy(remove);
        //}
    }
    private int RandomObstacles()
    {
        return Random.Range(0, obstacles.Length);
    }
    private int RandomBonus()
    {
        return Random.Range(0, bonuses.Length);
    }
    private float RandomDistanceZ()
    {
        return Random.Range(30.0f, 50.0f);
    }
    private int RandonNumberObstacles()
    {
        return Random.Range(0, 4);
    }
    private bool ChanceBonus()
    {
        int rand = Random.Range(-3, bonusChance);
        if(rand >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private int[] MixLine(int[] lines)
    {
        for(int i = 0; i < lines.Length; i++)
        {
            int currentValue = lines[i];
            int randIdx = Random.Range(i, lines.Length);
            lines[i] = lines[randIdx];
            lines[randIdx] = currentValue;
        }
        return lines;
    }
}