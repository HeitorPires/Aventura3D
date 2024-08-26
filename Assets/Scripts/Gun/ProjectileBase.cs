using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    public float timeToDestroy = 5f;

    public int damageAmount = 1;
    public float speed = 50f;

    public List<string> tagsToHit;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        foreach (string tag in tagsToHit)
        {
            if(tag == collision.transform.tag)
            {
                var damageable = collision.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;

                    damageable.Damage(damageAmount, dir);

                    Destroy(gameObject);
                }

                else
                {
                    var healthBase = collision.transform.GetComponent<HealthBase>();
                    if(healthBase != null)
                    {
                        healthBase.Damage(damageAmount);
                        Destroy(gameObject);
                    }
                }
                break;
            }


        }

    }

    
}
