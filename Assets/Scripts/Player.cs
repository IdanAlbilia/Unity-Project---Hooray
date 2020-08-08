using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement vars
    public float moveSpeed;
    private float maxSpeed = 7f;

    //Refrences
    public GameObject deathParticles;
    public GameManager manager;

    //Level vars
    private Vector3 input;
    private Vector3 spawn;
    public bool useManager = true;

    //audio
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
        if (useManager)
            manager = manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && (transform.position.y < 1.3) && (transform.position.y > 0.75))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 420);
        }
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
        {
            GetComponent<Rigidbody>().AddForce(input * moveSpeed);
        }
        if (transform.position.y < -4)
            Die();

        Physics.gravity = Physics.Raycast(transform.position, Vector3.down, 0.5f) ? Vector3.zero : new Vector3(0, -9.5f, 0);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            Die();
        }

        if (other.transform.tag == "Coin")
        {
            Destroy(other.gameObject);
            manager.currentScore += 50;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins")+1);
            //PlaySound(0);
        }
        if (other.transform.tag == "Token")
        {
            print("took it");
            Destroy(other.gameObject);
            manager.myTokens += 1;
            manager.currentScore += 25;
        }

        if (other.transform.tag == "Hooray")
        {
            Destroy(other.gameObject);
            manager.currentScore += 25;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Goal")
        {
            manager.currentScore += 100;
            manager.CompleteLevel();
        }
        if (other.transform.tag == "Enemy")
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        transform.position = spawn;
        manager.currentScore -= 25;
    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClips[clip];
        GetComponent<AudioSource>().Play();
    }
}
