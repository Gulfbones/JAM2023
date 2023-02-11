using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    [SerializeField] private float worth;
    [SerializeField] private bool hostile = false;
    private Rigidbody2D rb;
    private float moveSpeed;
    private Vector3 startScale;
    private Vector3 desiredScale;
    private Vector3 destination;
    private bool wanderCoRunning;
    //[SerializeField] private 
    private enum FishState
    {
        WANDERING,
        ATTACKING,
        FLEEING,
    };
    private FishState state;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = FishState.WANDERING;
        startScale = transform.localScale;
        desiredScale = transform.localScale;
        destination = transform.localPosition;
        wanderCoRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("");
        switch (state)
        {
            case FishState.WANDERING:
                if (!wanderCoRunning)
                {
                    wanderCoRunning = true;
                    StartCoroutine("WanderCoroutine");
                    Debug.Log("");
                }
                break;
            case FishState.ATTACKING:

                break;
            case FishState.FLEEING:

                break;
            default:
                break;

        }
        //rb.AddForce(destination);
    }
    private void LateUpdate()
    {
        Flip();
    }

    public IEnumerator WanderCoroutine()
    {
        Debug.Log("WanderCoroutine");
        Vector3 position = Randomize(5,10);
        destination = Vector3.Normalize(Randomize(5,10));
        //destination
        rb.AddForce(destination);
        int count = Random.Range(5, 10);
        yield return new WaitForSeconds(count);
        rb.AddForce(destination);
        wanderCoRunning =false;
    }

    // Flips the enemies scale to face the destination
    void Flip()
    {
        if (state == FishState.FLEEING)
        {
            // Correctly set flip for fleeing specificlly
            if ((transform.position).x > destination.x && desiredScale.x < 0) // On right side
            {
                desiredScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            if ((transform.position).x < destination.x && desiredScale.x > 0) // On left side
            {
                desiredScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            if ((transform.position).x > destination.x && desiredScale.x > 0) // On right side
            {
                desiredScale = new Vector3(desiredScale.x * -1, desiredScale.y, desiredScale.z);
            }
            if ((transform.position).x < destination.x && desiredScale.x < 0) // On left side
            {
                desiredScale = new Vector3(desiredScale.x * -1, desiredScale.y, desiredScale.z);
            }
        }
    }

    // Returns a random vector3 based on given minimum distance and maximum
    public Vector3 Randomize(float min, float max)
    {
        float x = Random.Range(min, max) * PositiveOrNegative();
        float y = Random.Range(min, max) * PositiveOrNegative();
        var vectorDistance = new Vector3(x, y, 0);
        return  vectorDistance;
    }

    // Returns a Positve or a Negative number randomly
    public float PositiveOrNegative()
    {
        if (Random.value >= 0.5)
        {
            return 1.0f;
        }
        return -1.0f;
    }
    /*
    public IEnumerator FleeCoroutine()
    {
        fleeCoroutineRunning = true;
        moveSpeed = fleeMoveSpeed;
        destination = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Correctly set flip for fleeing specificlly
        if ((transform.position).x > destination.x && desiredScale.x < 0) // On right side
        {
            desiredScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        if ((transform.position).x < destination.x && desiredScale.x > 0) // On left side
        {
            desiredScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        //playerObject.transform.rotation = Quaternion.Lerp(playerObject.transform.rotation, desiredAngle, 10.0f * Time.deltaTime);
        yield return new WaitForSeconds(5);
        state = Enemy_State.STALKING;
        Teleport();
        fleeCoroutineRunning = false;
    }
    */

    public void Eaten()
    {

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player Range Box")
        {
            if (hostile)
            {
                state = FishState.ATTACKING;
            }
            else
            {
                state = FishState.FLEEING;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player Range Box")
        {
            state = FishState.WANDERING;
        }
    }

}
