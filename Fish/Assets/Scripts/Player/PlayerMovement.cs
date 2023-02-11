using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float targetVelocity = 4.0f;
    [SerializeField]
    private float acceleration = 2.50f;
    [SerializeField]
    private float rotateSmoothSpeed = 0.15f;
    [SerializeField]
    private AnimationCurve dragCurve;
    [SerializeField]
    private float maxDrag = 1.75f;

    float rotRefVelocity;

    PlayerInput input;
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;

    private void OnEnable() {
        input = GetComponent<PlayerInput>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        rb2D.rotation = rb2D.rotation % 360f;
        rb2D.angularVelocity = 0;
        rb2D.drag = dragCurve.Evaluate(rb2D.velocity.sqrMagnitude / Mathf.Pow(targetVelocity, 2)) * maxDrag;

        rb2D.AddForce(input.Movement * acceleration * rb2D.mass);
        rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, targetVelocity);

        float targetAngle = Mathf.Atan2(rb2D.velocity.y, rb2D.velocity.x) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(rb2D.rotation, targetAngle, ref rotRefVelocity, rotateSmoothSpeed);
        rb2D.rotation = angle;

        spriteRenderer.flipY = rb2D.velocity.x < 0;
    }
}
