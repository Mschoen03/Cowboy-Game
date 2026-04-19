using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBox : MonoBehaviour
{
    public float explosionRadius = 6f;
    public int damage = 50;
    public float destroyDelay = 4f;

    public GameObject explosionEffect;

    private bool exploded = false;

    public void Explode()
    {
        if (exploded) return;

        exploded = true;

        // Damage nearby enemies
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in hits)
        {
            EnemyAI enemy = hit.GetComponent<EnemyAI>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        // Spawn explosion FX
        if (explosionEffect != null)
        {
            GameObject fx = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(fx, 4f);
        }

        // Disable box
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        // Destroy object after delay
        Destroy(gameObject, destroyDelay);
    }
}
