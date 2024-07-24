using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //Power of Fire
    public int damage = 1;

    //true = enemy , false = player fire
    public bool isEnemyFire = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }
}
