using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FishSpawnRegion : MonoBehaviour
{
    private bool isPlayerInside;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<PlayerInput>() != null) {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<PlayerInput>() != null) {
            isPlayerInside = false;
        }
    }
}
