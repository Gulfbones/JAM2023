using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    [SerializeField] private float worth;
    [SerializeField] private bool hostile = false;
    [SerializeField] private bool aware = true;
    [SerializeField] private int range = 15;
    //[SerializeField] private AnimationCurve curve;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    //private Vector3 ;
    [SerializeField] private int normalMoveSpeed = 40;
    [SerializeField] private int fleeMoveSpeed = 20;
    [SerializeField] private int attackMoveSpeed = 30;
    //private float moveSpeed;
    private Vector3 startScale;
    private Vector3 desiredScale;
    private Vector3 destination;
    private bool wanderCoRunning;
    public FishSpawnPoint parentSpawnPoint;
    //[SerializeField] private 
    private enum FishState
    {
        WANDERING,
        ATTACKING,
        FLEEING,
        WAITING,
        HOME,
    };
    [SerializeField] private FishState state;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.transform.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        state = FishState.WANDERING;
        startScale = transform.localScale;
        desiredScale = transform.localScale;
        destination = transform.localPosition;
        wanderCoRunning = false;
        //desiredScale = startScale;
        rb.drag = 20;
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
                    //Debug.Log("ran");
                }
                //rb.AddForce(destination);
                if (Vector3.Distance(gameObject.transform.position, destination) < 4)
                {
                    rb.drag = 2;

                }
                else
                {
                    rb.AddForce((destination - transform.position).normalized * normalMoveSpeed);
                }
                break;
            case FishState.ATTACKING:
                Attack();
                break;
            case FishState.FLEEING:
                Flee();
                break;
            case FishState.WAITING:
                StartCoroutine("WaitCoroutine");
                break;
            case FishState.HOME:
                Home();
                break;
            default:
                break;

        }
        //rb.AddForce(destination);
        //transform.Find("Debug Square").gameObject.transform.position = destination;
    }

    /*
    void FixedUpdate()
    {
        if(state == FishState.WANDERING )
        {
            if( Vector3.Distance(gameObject.transform.position , destination) < 4)
            {
                rb.drag = 55;

            }
            else
            {
                
            }
            //rb.drag = gameObject.transform.position / destination;
        }
        
        //rb.AddForce(destination);
        
    }
    */
    private void LateUpdate()
    {
        if (rb.velocity.x > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

    }

    public IEnumerator WanderCoroutine()
    {
        //Debug.Log("WanderCoroutine");
        //Vector3 position = Randomize(5,10);
        //destination = Vector3.Normalize(Randomize(5,10));
        destination = (Randomize(1,range));
        //Debug.Log("\nX:" + destination.x + " |     Y:" + destination.y);
        rb.drag = 50;
        normalMoveSpeed = Random.Range(20, 40);
        //rb.AddForce((destination - transform.position) * 10 * normalMoveSpeed);
        int count = Random.Range(8, 10);
        yield return new WaitForSeconds(count);
        //Debug.Log("set none");
        destination = gameObject.transform.position*0;
        wanderCoRunning = false;
    }

    public IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(2);
        state = FishState.WANDERING;
    }
    public void Flee()
    {
        rb.AddForce((transform.position - GameObject.Find("Player").gameObject.transform.position).normalized * fleeMoveSpeed);
    }

    public void Attack()
    {
        rb.AddForce((GameObject.Find("Player").gameObject.transform.position - transform.position).normalized * attackMoveSpeed);
    }

    public void Home()
    {
        destination = gameObject.transform.parent.position;
        rb.AddForce((destination - transform.position).normalized * normalMoveSpeed);
        wanderCoRunning = false;
        state = FishState.WANDERING;
        //yield return new WaitForSeconds(2);
        //state = FishState.WANDERING;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            StopAllCoroutines();
            state = FishState.HOME;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(aware)
        if (collision.gameObject.name == "Player Range Box" && aware)
        {
            StopAllCoroutines();
            wanderCoRunning = false;    
            if (hostile)
            {
                state = FishState.ATTACKING;
            }
            else
            {
                state = FishState.FLEEING;
            }
            rb.drag = 50;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player Range Box" && aware)
        {
            state = FishState.FLEEING;
            StartCoroutine("WaitCoroutine");
            //rb.drag = 50;
        }
    }

}
