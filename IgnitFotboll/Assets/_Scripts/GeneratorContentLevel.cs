using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorContentLevel : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject[] bonuses;

    private float currentPosZSpawn;
    private int bonusChance = 6;
    private int[] spawnLines = { -3, 0, 3 };
    private int[] mixLine;

    private bool bonusEnabled = false;

    private void Start()
    {
        //mixLine = MixLine(spawnLines);
        //for(int i = 0 ; i < mixLine.Length; i++)
        //{
        //    Debug.Log(mixLine[i]);
        //}
        for (int i = 0; i < 10; i++)
        {
            SpawnContent();
        }
    }
    private void SpawnContent()
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
                    if (obs.GetComponent<Enemy>())
                    {
                        obs.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                    }
                }
                else if(numObstacles <= 0 ||  i >= numObstacles)
                {
                    if (bonusEnabled)
                    {
                        Instantiate(bonuses[RandomBonus()], spawnPos, Quaternion.identity);
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
        return Random.Range(30.0f, 70.0f);
    }
    private int RandonNumberObstacles()
    {
        return Random.Range(-1, 4);
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

        //рандомное расстояние следуюшего спавна по оси Z от 30 до 70
        //позиция спавна в данный момент = 0, затем прибавляется рандомное расстояние
        //рандомное количество препятствий в ряд (-1, 4)
        //если количество препятствий < 3 определяем будет бонус или нет
        //для этого нужна переменная int например bonusChance = 6 которая, каждый спавн бонуса будет декрементироваться
        //будет бонус или нет будет определяться рандомно (-5, bonusChance) - если > 0, то будет бонус
        //при спавне бонуса - bonusChance--; как только он достигнет 0, то сбрасывается по умолчанию на 6
        //затем нужно определить на какой полосе будет стоять то что будет спавниться
        //для этого нужно создать массив int[] lines = {-3, 0, 3};, а затем перемешивать его

        //перемешивание массива: в цикле перебираем массив lines[] создаём переменную к которой присваиваем текущее значение
        //и переменную которая определяет рандомный индекс массива, затем текущему элементу присваиваем рандомный элемент,
        //а рандомному элементу присваиваем текущее значение

        //перед спавном, зная сколько элементов будет спавниться - нужно создать переменную lenghtSpawn, она будет хранить в себе общее количество элементов
        //и зная позицию спавна, нужно создать Vector3, где будет храниться позиция данного спавна
        //спавн будет в цикле, количество итераций которого будет определять lenghtSpawn (цикл начать с 1)
        //когда всё определено, нужно создать условие - если препятствий > 0 и i не превышает их количество - 
        //то спавним препятствия, а если препятствий для спавна <= 0 или i == количество препятствий и бонусы = тру, то спавним бонусы
        //автоматически бонусы не будут спавниться если бонусы = фолс, и закончилось число итераций.

}
