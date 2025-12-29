using JetBrains.Annotations;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject winText;

    public GameObject playAgain;

    public float delay = 2f;
    
    private bool triggered = false;

    private float timer = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                Time.timeScale = 0f;
                triggered = false;
                winText.SetActive(false);
                playAgain.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winText.SetActive(true);
            Animator animator = other.GetComponent<Animator>();
            animator.SetBool("Finished", true);
            
            triggered = true;
        }
    }
}
