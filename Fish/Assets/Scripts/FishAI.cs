using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    [SerializeField] private float worth;
    [SerializeField] private bool hostile = false;
    //[SerializeField] private AnimationCurve curve;
    private Rigidbody2D rb;
    //private Vector3 ;
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
        //rb.drag = curve.Evaluate()
        //Debug.Log("");
        switch (state)
        {
            case FishState.WANDERING:
                if (!wanderCoRunning)
                {
                    wanderCoRunning = true;
                    StartCoroutine("WanderCoroutine");
                    Debug.Log("ran");
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
        transform.Find("Debug Square").gameObject.transform.position = destination;
    }
    void FixedUpdate()
    {
        if(state == FishState.WANDERING && Vector3.Distance(gameObject.transform.position , destination) < 4 )
        {

            //rb.drag = gameObject.transform.position / destination;
            rb.drag = 2;
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
        //destination = Vector3.Normalize(Randomize(5,10));
        destination = (Randomize(5,10));
        Debug.Log("\nX:" + destination.x + " |     Y:" + destination.y);
        rb.drag = 0;
        rb.AddForce((destination - transform.position) * 15);
        int count = Random.Range(4, 8);
        yield return new WaitForSeconds(count);
        //Debug.Log("set none");
        destination = gameObject.transform.position*0;
        wanderCoRunning =false;
    }

    public void Flee()
    {
        rb.AddForce(GameObject.Find("Player").gameObject.transform.position * -1);
    }

    public void Attack()
    {
        rb.AddForce(GameObject.Find("Player").gameObject.transform.position );
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
        var vectorDistance = new Vector3(gameObject.transform.parent.position.x + x, gameObject.transform.parent.position.y + y, 0);
        //Debug.Log("Destination X: " + (gameObject.transform.position.x + x) + " Y: " + (gameObject.transform.position.y + y));
        
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
                rb.drag = 10;
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
            rb.drag = 5;
        }
    }

}
