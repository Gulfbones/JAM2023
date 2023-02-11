using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;

    [Header("Projectile Info")]
    [SerializeField] private GameObject projectile;
    [SerializeField] float projectileSpeed = 1;

    [Header("For debug")]
    [SerializeField] private bool fireProjectiles;
    [SerializeField] private float shotDelay = 1.0f;
    private bool isCoroutineRunning;

    private Vector3 displacementVec;
    private float displacementAngle;

    private void Start()
    {
        fireProjectiles = false;
        player = FindObjectOfType<PlayerInput>().gameObject;
        playerTransform = player.GetComponent<Transform>();
        StartCoroutine(TestFire());
    }

    /*private void Update()
    {
        if(fireProjectiles)
        {
            if (!isCoroutineRunning)
            {
                StartCoroutine(TestFire());
                isCoroutineRunning = true;
            }
        } else if(!fireProjectiles)
        {
            if (isCoroutineRunning)
            {
                StopCoroutine(TestFire());
                isCoroutineRunning = false;
            }
        }
    }*/

    private void FireProjectile()
    {
        displacementVec = (playerTransform.transform.position - gameObject.transform.position).normalized;
        displacementAngle = Mathf.Atan2(displacementVec.y, displacementVec.x) * Mathf.Rad2Deg;
        Debug.Log(displacementAngle);
        Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, displacementAngle));
    }

    private IEnumerator TestFire()
    {
        while(true)
        {
            if (fireProjectiles)
            {
                FireProjectile();
                yield return new WaitForSeconds(shotDelay);
            } else
            {
                yield return new WaitForSeconds(shotDelay);
            }
        }
    }


}
