using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    //Fire prefab
    public GameObject firePrefab;

    //Cooldown between shots
    public float fireRate = 0.25f;

    //Cooldown counter (0 = ready to shoot)
    public float cooldown;

    //Spawn position
    public GameObject mouthPosition;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.Instance.isPlaying())
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    public bool CanAttack
    {
        get
        {
            return cooldown <= 0f;
        }
    }

    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            cooldown = fireRate;
            var fire = Instantiate(firePrefab);
            fire.transform.position = mouthPosition.transform.position;

            Fire fir = fire.GetComponent<Fire>();

            if (fir != null)
            {
                fir.isEnemyFire = isEnemy;
            }
        }
    }
}
