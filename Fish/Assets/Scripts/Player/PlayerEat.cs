using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEat : MonoBehaviour
{
    private int _foodPoints;
    public int FoodPoints { get { return _foodPoints; } set { _foodPoints = value; } }
    
    [SerializeField]
    private Transform mouthTransform;
    [SerializeField]
    private float eatSize = 0.35f;
    
    void Update()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(mouthTransform.position, eatSize, transform.forward, 0.1f);

        foreach(RaycastHit2D hit in hits) {
            FoodObject fObj;
            if(hit.collider.TryGetComponent(out fObj)) {
                FoodPoints += fObj.GetFoodPoints();
                fObj.DestroyObj();
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(mouthTransform.position, eatSize);
    }
}
