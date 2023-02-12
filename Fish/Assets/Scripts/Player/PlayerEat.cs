using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEditor.Animations;

public class PlayerEat : MonoBehaviour
{
    [SerializeField]
    private float _foodPoints = 5.0f;
    public float FoodPoints { get { return _foodPoints; } set { _foodPoints = value; } }
    
    private float lastAteValue = 0.0f;
    [SerializeField]
    private Transform mouthTransform;
    [SerializeField]
    private float eatSize = 0.35f;
    [SerializeField] 
    private Sprite[] newSprite;
    [SerializeField]
    private AnimatorController[] newController;
    [SerializeField] 
    private int spriteNum = 0;
    private float startingSize;
    private float startingFood;
    [SerializeField]
    private float amountChanged = 0;
    private Text lastAteFish;

    private GameObject cinemaVirtualCamera;
    private CameraGrow camGrowRef;
    private GameObject chomping;

    private bool size2;
    private bool size3;
    private bool damagedRecently;
    private float newMinSize;


    private void Start()
    {
        startingFood = FoodPoints;
        cinemaVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>().gameObject;
        camGrowRef = cinemaVirtualCamera.GetComponent<CameraGrow>();
        startingSize = transform.localScale.x;
        chomping = transform.Find("Chomper").gameObject;
        chomping.SetActive(false);
        Debug.Log("Size"+ newSprite.Length);
        size2 = false;
        size3 = false;
        damagedRecently = false;
        newMinSize = 5.0f;
    }

    void Update()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(mouthTransform.position, eatSize, transform.forward, 0.1f);
        if (Input.GetKeyDown(KeyCode.R))
        {
            FoodPoints -= 0.1f;
            Scale();
        }
        foreach(RaycastHit2D hit in hits) {
            FoodObject fObj;
            if(hit.collider.TryGetComponent(out fObj)) {
                if(FoodPoints >= fObj.GetRequiredFoodPoints()) {
                    lastAteValue = fObj.GetFoodPoints();
                    FoodPoints += fObj.GetFoodPoints();
                    StartCoroutine(ChompCoroutine());
                    if (lastAteFish != null)
                    {
                        lastAteFish.text = gameObject.ToString();
                    }
                    fObj.DestroyObj();
                    Scale(); // properly scales fish
                    //var changing = (FoodPoints / startingFood);
                }
            }
        }
    }

    private void Scale() { // properly scales fish
        transform.localScale = new Vector2(startingSize * FoodPoints / startingFood, startingSize * FoodPoints / startingFood);
        //transform.Find("sprite").GetComponent<CheckSizeChange>().SizeUp(lastAteValue);
        //amountChanged += lastAteValue;
        if (transform.localScale.x > 4.0f && !size3)
        {
            //spriteNum = 2;
            size3 = true;
            eatSize = 1.5f;
            newMinSize = 40f;
            StartCoroutine(ChangeSpriteCoroutine(1));
            
        }
        if (transform.localScale.x > 2.0f && !size2) {
            //spriteNum = 1;
            size2 = true;
            eatSize = 0.8f;
            newMinSize = 20f;
            StartCoroutine(ChangeSpriteCoroutine(0));
            
        }
        camGrowRef.ChangeSize(transform.localScale.x * 2); // scales the camera with the fish, can be edited via CM vCam1
    }

    public IEnumerator ChompCoroutine()
    {
        chomping.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        chomping.SetActive(false);
    }

    public IEnumerator ChangeSpriteCoroutine(int spriteNum)
    {
        //var emission = GetComponent<ParticleSystem>().emission; // Stores the module in a local variable
        var emission = GetComponent<ParticleSystem>(); // Stores the module in a local variable
        emission.Play(); // Applies the new value directly to the Particle System
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite[spriteNum];
        gameObject.GetComponent<Animator>().runtimeAnimatorController = newController[spriteNum];
        //emission.enabled = false;
    }

    public IEnumerator DamageCoroutine()
    {
        if (!((FoodPoints - FoodPoints * 0.05f) < newMinSize))
        {
            FoodPoints -= FoodPoints * 0.05f;
            Scale();
        }
        
        yield return new WaitForSeconds(2.0f);
        damagedRecently = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(mouthTransform.position, eatSize);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Hostile" && !damagedRecently)
        {
            damagedRecently = true;
            StartCoroutine(DamageCoroutine());
        }
    }
}
