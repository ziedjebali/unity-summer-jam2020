using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public GameObject ProjectilePrefab;
    public Transform ShootPosition;
    public float ProjectileSpeed;

    public float timeBetweenShots;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            GameObject projectile = Instantiate(ProjectilePrefab, ShootPosition.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * ProjectileSpeed);
            timer = timeBetweenShots;
        }
    }
}

