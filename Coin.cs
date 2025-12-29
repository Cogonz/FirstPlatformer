using UnityEngine;

public class Coin : MonoBehaviour
{

    public AudioClip collectSound;
    
    private AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreKeeper.AddToScore(1); // add score
            audioSource.PlayOneShot(collectSound);
            Destroy(gameObject,0.1f);           // remove coin
        }
    }
}
