using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;

    private void Update() {
        transform.position = Vector3.Lerp(transform.position, MatchZ(transform.position, followTarget.position), Mathf.Pow(Time.deltaTime, 0.5f));
    }

    Vector3 MatchZ(Vector3 first, Vector3 second) {
        return new Vector3(second.x, second.y, first.z);
    }
}
