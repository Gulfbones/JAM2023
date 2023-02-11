using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObject : MonoBehaviour
{
    [SerializeField]
    private int foodWorth = 1;

    public void DestroyObj() {
        Destroy(gameObject);
    }

    public int GetFoodPoints() { return foodWorth; }
}
