using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
//using UnityEditor.Animations;
using UnityEngine.Animations;
using UnityEngine.Windows;
using TMPro;
using UnityEngine.SceneManagement;
//using UnityEditor.Animations;

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
    private RuntimeAnimatorController[] newController;
    [SerializeField] 
    private int spriteNum = 0;
    private float startingSize;
    private float startingFood;
    [SerializeField]
    private float amountChanged = 0;
    private Text lastAteFish;
    [SerializeField]
    private TextMeshProUGUI sizeText;

    private PlayerInput pInput;

    private GameObject cinemaVirtualCamera;
    private CameraGrow camGrowRef;
    private GameObject chomping;

    private bool size2;
    private bool size3;
    private bool size4;
    
    private bool damagedRecently;
    private float newMinFood;


    private void Start()
    {
        startingFood = FoodPoints;
        pInput = GetComponent<PlayerInput>();
        cinemaVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>().gameObject;
        camGrowRef = cinemaVirtualCamera.GetComponent<CameraGrow>();
        startingSize = transform.localScale.x;
        chomping = transform.Find("Chomper").gameObject;
        chomping.SetActive(false);
        Debug.Log("Size"+ newSprite.Length);
        size2 = false;
        size3 = false;
        size4 = false;
        damagedRecently = false;
        newMinFood = 6.0f;
    }

    void Update()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(mouthTransform.position, eatSize, transform.forward, 0.1f);
        if (pInput.ShrinkFish)
        {
            if ((FoodPoints >= newMinFood))
            {
                FoodPoints -= 0.1f;
                //FoodPoints -= FoodPoints * 0.05f;
                Scale();
            }
            //Scale();
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
                    if(fObj.tag == "Final")
                    {
                        SceneManager.LoadScene("CreditsScene"); //LoadSceneMode.Additive);
                    }
                    if (fObj.lightAngler) { gameObject.GetComponentInChildren<HeadlampAttached>().ActivateLight(); }
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
        if (transform.localScale.x > 10.0f && !size4)
        {
            //spriteNum = 2;
            size4 = true;
            //eatSize = 5.5f;
            newMinFood = 51.0f;
            StartCoroutine(ChangeSpriteCoroutine(1));

        }
        if (transform.localScale.x > 6.0f && !size3)
        {
            //spriteNum = 2;
            size3 = true;
            //eatSize = 1.5f;
            newMinFood = 31.0f;
            StartCoroutine(ChangeSpriteCoroutine(1));
            
        }
        if (transform.localScale.x > 2.0f && !size2) {
            //spriteNum = 1;
            size2 = true;
            //eatSize = 0.8f;
            newMinFood = 11f;
            StartCoroutine(ChangeSpriteCoroutine(0));
            
        }
        camGrowRef.ChangeSize(transform.localScale.x); // scales the camera with the fish, can be edited via CM vCam1
        //GameObject.Find("SIZE").gameObject.GetComponent<TextMeshPro>().text = "Size" + transform.localScale.x;
        sizeText.text = "Size: " + Mathf.Round(transform.localScale.x * 100f) / 100f;
        eatSize = 0.35f + (0.35f*(transform.localScale.x/2));
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
        if (!((FoodPoints - FoodPoints * 0.05f) < newMinFood))
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
        if (FoodPoints > 80 && collision.gameObject.tag == "Hostile")
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Hostile" && !damagedRecently && FoodPoints < 70)
        {
            damagedRecently = true;
            StartCoroutine(DamageCoroutine());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Foliage" && FoodPoints > 75)
        {
            GameObject.Destroy(collision.gameObject);
        }
    }
}
