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
    [SerializeField]
    private float amountChanged = 0;
    private Text lastAteFish;

    private GameObject cinemaVirtualCamera;
    private CameraGrow camGrowRef;
    private GameObject chomping;

    private void Start()
    {
        cinemaVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>().gameObject;
        camGrowRef = cinemaVirtualCamera.GetComponent<CameraGrow>();
        startingSize = transform.localScale.x;
        chomping = transform.Find("Chomper").gameObject;
        chomping.SetActive(false);
        Debug.Log("Size"+ newSprite.Length);
    }

    void Update()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(mouthTransform.position, eatSize, transform.forward, 0.1f);

        foreach(RaycastHit2D hit in hits) {
            FoodObject fObj;
            if(hit.collider.TryGetComponent(out fObj)) {
                if(FoodPoints >= fObj.GetRequiredFoodPoints()) {
                    lastAteValue = fObj.GetFoodPoints();
                    FoodPoints += fObj.GetFoodPoints();
                    StartCoroutine("ChompCoroutine");
                    if (lastAteFish != null)
                    {
                        lastAteFish.text = gameObject.ToString();
                    }
                    fObj.DestroyObj();
                    Scale(); // properly scales fish
                    camGrowRef.ChangeSize(); // scales the camera with the fish, can be edited via CM vCam1
                }
            }
        }
    }

    private void Scale() { // properly scales fish
        transform.localScale = new Vector2(startingSize * FoodPoints / 5, startingSize * FoodPoints / 5);
        transform.Find("sprite").GetComponent<CheckSizeChange>().SizeUp(lastAteValue);
        amountChanged += lastAteValue;
        if (transform.localScale.x > 4.0f)
        {
            //spriteNum = 2;
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite[1];
            gameObject.GetComponent<Animator>().runtimeAnimatorController = newController[1];
        }
        if (transform.localScale.x > 2.0f) {
            //spriteNum = 1;
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite[0];
            gameObject.GetComponent<Animator>().runtimeAnimatorController = newController[0];
        }
    }

    public IEnumerator ChompCoroutine()
    {
        chomping.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        chomping.SetActive(false);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(mouthTransform.position, eatSize);
    }
}
