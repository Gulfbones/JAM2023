using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBall : MonoBehaviour
{
    [SerializeField]
    private float health = 5.0f;

    private void OnCollisionEnter2D(Collision2D collision) {
        PlayerMovement pM;
        if(collision.collider.TryGetComponent(out pM)) {
            if(pM.gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 1) {
                health -= 1;
            }

            if(health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
