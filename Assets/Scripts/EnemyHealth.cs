using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1;
    public int health;
    public bool isEnemy = true;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        SpecialEffectsHelper.Instance.Explode(transform.position);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fire fire = collision.gameObject.GetComponent<Fire>();
        if (fire != null)
        {
            if (fire.isEnemyFire != isEnemy)
            {
                TakeDamage(fire.damage);
                Destroy(fire.gameObject);
            }
        }
    }
}
