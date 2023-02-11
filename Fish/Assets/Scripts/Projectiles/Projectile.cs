using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float moveSpeed;

    private void Update()
    {
        gameObject.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}
