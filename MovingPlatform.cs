using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float start;
    public float end;

    public float speed = 2f;
    
    private Rigidbody2D rb;
    private Vector2 target;

    private float fixedY;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedY = rb.position.y;
        target = new Vector2(end,fixedY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, Time.fixedDeltaTime * speed);
        newPos.y = fixedY;
        rb.MovePosition(newPos);

        if (Mathf.Abs(rb.position.x - target.x) < 0.01f)
        {
            target.x = (target.x == start) ? end : start;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
