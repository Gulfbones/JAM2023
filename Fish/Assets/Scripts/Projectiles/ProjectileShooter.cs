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

    private Vector3 displacementVec;
    private float displacementAngle;

    private void Start()
    {
        player = FindObjectOfType<PlayerInput>().gameObject;
        playerTransform = player.GetComponent<Transform>();
        FireProjectile();
    }

    private void FireProjectile()
    {
        displacementVec = (playerTransform.transform.position - gameObject.transform.position).normalized;
        displacementAngle = Mathf.Atan2(displacementVec.y, displacementVec.x) * Mathf.Rad2Deg;
        Debug.Log(displacementAngle);
        Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, displacementAngle));
    }

}
