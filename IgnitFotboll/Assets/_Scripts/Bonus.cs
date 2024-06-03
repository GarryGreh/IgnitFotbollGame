using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public enum BonusType
    {
        Coin,
        Heart,
        Invisibility,
        Hit,
        Speed
    }
    public BonusType typeBonus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerGetBonus>())
        {
            //Debug.Log(typeBonus.ToString());
            CheckBonus(typeBonus, other);
            Destroy(this.gameObject);
        }
    }
    private void CheckBonus(BonusType _type, Collider col)
    {
        if(col.gameObject.GetComponent<PlayerGetBonus>() != null)
        {
            switch (typeBonus)
            {
                case BonusType.Coin:
                    col.gameObject.GetComponent<PlayerGetBonus>().Coin();
                    break;

                case BonusType.Heart:
                    col.gameObject.GetComponent<PlayerGetBonus>().Heart();
                    break;

                case BonusType.Invisibility:
                    col.gameObject.GetComponent<PlayerGetBonus>().Invisibility(false);
                    break;

                case BonusType.Hit:
                    col.gameObject.GetComponent<PlayerGetBonus>().PowerHit();
                    break;

                case BonusType.Speed:
                    col.gameObject.GetComponent<PlayerGetBonus>().IncreaseSpeed();
                    break;
            }
        }        
    }
}
