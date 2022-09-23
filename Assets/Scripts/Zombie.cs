using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    protected int damage;

    public int GetZombieDamage()
    {
        return damage;
    }
}
