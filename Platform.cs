using UnityEngine;

public class Platform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    private Rigidbody2D rigid;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
