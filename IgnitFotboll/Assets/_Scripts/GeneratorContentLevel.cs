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

        //��������� ���������� ���������� ������ �� ��� Z �� 30 �� 70
        //������� ������ � ������ ������ = 0, ����� ������������ ��������� ����������
        //��������� ���������� ����������� � ��� (-1, 4)
        //���� ���������� ����������� < 3 ���������� ����� ����� ��� ���
        //��� ����� ����� ���������� int �������� bonusChance = 6 �������, ������ ����� ������ ����� ������������������
        //����� ����� ��� ��� ����� ������������ �������� (-5, bonusChance) - ���� > 0, �� ����� �����
        //��� ������ ������ - bonusChance--; ��� ������ �� ��������� 0, �� ������������ �� ��������� �� 6
        //����� ����� ���������� �� ����� ������ ����� ������ �� ��� ����� ����������
        //��� ����� ����� ������� ������ int[] lines = {-3, 0, 3};, � ����� ������������ ���

        //������������� �������: � ����� ���������� ������ lines[] ������ ���������� � ������� ����������� ������� ��������
        //� ���������� ������� ���������� ��������� ������ �������, ����� �������� �������� ����������� ��������� �������,
        //� ���������� �������� ����������� ������� ��������

        //����� �������, ���� ������� ��������� ����� ���������� - ����� ������� ���������� lenghtSpawn, ��� ����� ������� � ���� ����� ���������� ���������
        //� ���� ������� ������, ����� ������� Vector3, ��� ����� ��������� ������� ������� ������
        //����� ����� � �����, ���������� �������� �������� ����� ���������� lenghtSpawn (���� ������ � 1)
        //����� �� ����������, ����� ������� ������� - ���� ����������� > 0 � i �� ��������� �� ���������� - 
        //�� ������� �����������, � ���� ����������� ��� ������ <= 0 ��� i == ���������� ����������� � ������ = ���, �� ������� ������
        //������������� ������ �� ����� ���������� ���� ������ = ����, � ����������� ����� ��������.

}
