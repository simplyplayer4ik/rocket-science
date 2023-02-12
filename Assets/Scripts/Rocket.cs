using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    GameManager gm;
    bool intransition;
    [SerializeField] float boostPower = 1000;
    [SerializeField] float rotationSpeed = 300;
    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip fail;
    [SerializeField] GameObject Explosion;
    [SerializeField] GameObject Victory;
    [SerializeField] ParticleSystem Fire;   
    [SerializeField] Transform explosionSpawn;
    AudioSource aS;
    Rigidbody rb;
 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aS = GetComponent<AudioSource>();
        aS.volume = 0;
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!intransition)
        {
            Thrust();
            Rotation();
        }
    }
    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up*boostPower*Time.deltaTime);
            if (!Fire.isPlaying)
            {
                Fire.Play();
            }
           
            aS.volume = 0.72f;
        }
        else
        {
            Fire.Stop();
            aS.volume = 0;
          
        }
    }
   
    void Rotation()
    {
        float deltaZ = -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        if (deltaZ != 0)
        {
            rb.freezeRotation = true;
            transform.Rotate(new Vector3(0, 0, deltaZ));
            rb.freezeRotation = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (intransition)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Start":
                break;
            case "Finish":
                Transition();
                break;
            default:
                Die();
                break;
        }
        void Die()
        {
            aS.volume = 0;
            AudioSource.PlayClipAtPoint(fail, Camera.main.transform.position, 0.3f);
            intransition = true;
            Instantiate(Explosion, explosionSpawn.position, Quaternion.identity);
            //Destroy(gameObject, 1);
            StartCoroutine(gm.LevelReload());

        }
        void Transition()
        {
            aS.volume = 0;
            AudioSource.PlayClipAtPoint(victory, Camera.main.transform.position, 0.3f);
            intransition = true;
            Instantiate(Victory, explosionSpawn.position, Quaternion.identity);
            StartCoroutine(gm.LoadNextLevel());        
        }

    }
}
