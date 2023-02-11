using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CheckSizeChange : MonoBehaviour
{
    [SerializeField] private float currentSize;
    [SerializeField] private float ammountChange = 0.5f;
    [SerializeField] private float maxSize = 3f;
    private Vector3 vecChange;
    [SerializeField] private Sprite[] newSprite;
    [SerializeField] private int spriteNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Vector3 vec = gameObject.transform.lossyScale;
        //currentSize = vec.x;
    }

    /*
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vec = gameObject.transform.lossyScale;
        currentSize = vec.x;
        //Debug.Log(currentSize);
        if (currentSize < maxSize || spriteNum >= newSprite.Length)
        {
            //scale the sprite and the hitbox larger
            float newValues = currentSize + ammountChange;
            vecChange = new Vector3(newValues, newValues, 1f);
            gameObject.transform.localScale = vecChange;
        }
        else if(spriteNum < newSprite.Length)
        {
            //change the sprite to the larger sprite
            //reset the sprite scale and hitbox back to og values
            vecChange = new Vector3(1f, 1f, 1f);
            gameObject.transform.localScale = vecChange;
            gameObject.GetComponent<SpriteRenderer>().sprite=newSprite[spriteNum];
            spriteNum++;
        }
    }
    */
    public void SizeUp(float changed)
    {
        Debug.Log("value passed" + changed);
        ammountChange = changed;
        Vector3 vec = gameObject.transform.lossyScale;
        currentSize = vec.x;
        //Debug.Log(currentSize);
        if (currentSize < maxSize || spriteNum >= newSprite.Length)
        {
            //scale the sprite and the hitbox larger
            float newValues = currentSize + ammountChange;
            vecChange = new Vector3(newValues, newValues, 1f);
            gameObject.transform.localScale = vecChange;
        }
        else if (spriteNum < newSprite.Length)
        {
            //change the sprite to the larger sprite
            //reset the sprite scale and hitbox back to og values
            //vecChange = new Vector3(1f, 1f, 1f);
            //gameObject.transform.localScale = vecChange;
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite[spriteNum];
            spriteNum++;
        }
    }
}
