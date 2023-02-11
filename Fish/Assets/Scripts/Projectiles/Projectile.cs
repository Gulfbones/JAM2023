using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;

    private void Start()
    {
        StartCoroutine(ProjectileDeath());
    }

    private void Update()
    {
        gameObject.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    private IEnumerator ProjectileDeath()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
