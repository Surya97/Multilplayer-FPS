using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth=100;
    public int currentHealth=0;
    private PlayerHealth playerHealth;
    private Animator anim;
    //private AudioSource audioSource;
    public float fallSpeed=2f;
    //public AudioClip deathClip;
    public bool dead;
    private GameObject hitParticles;
    public GameObject blood;

	void Awake () {
        currentHealth = startingHealth;
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
        dead = false;
	}
	
	
    public void TakeDamage(int damage, Vector3 position)
    {
        //audioSource.Play();
        print("Enemy Hit");
        currentHealth -= damage;
        hitParticles = Instantiate(blood, position, Quaternion.identity) as GameObject;
        hitParticles.GetComponent<ParticleSystem>().Stop();
        hitParticles.GetComponent<ParticleSystem>().Play();
        if (dead)
        {
            this.transform.rotation = Quaternion.AngleAxis(90f, Vector3.left);
            this.GetComponent<Rigidbody>().velocity = Vector3.down * Time.deltaTime * fallSpeed;
            this.GetComponent<NavMeshAgent>().enabled = false;
            this.GetComponent<Animator>().enabled = false;
            print("EnemyDead");
        }
        if(currentHealth<=0&&!dead)
        {
            dead = true;
            
        }
    }
}
