using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObject : MonoBehaviour
{
    [SerializeField]
    private float foodWorth = 1;
    [SerializeField]
    private float requiredFoodPoints = 1;
    [SerializeField]
    private bool isFish;

    public void DestroyObj() {
        Destroy(gameObject);
        if (isFish)
            FishSpawnPoint.fishCountGlobal -= 1;
    }

    public float GetFoodPoints() { return foodWorth; }
    public float GetRequiredFoodPoints() { return requiredFoodPoints; }
}
