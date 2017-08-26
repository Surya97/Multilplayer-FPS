using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    private Ray shootRay;
    private RaycastHit hitPoint;
    public float range = 100f;

    [SerializeField]
    private LayerMask mask;

    AudioSource audioSource;
    public int damagePerShot=20;
    public float TimeBetweenAttacks=0.5f;
    private float timer = 0f;
	
	void Awake () {
        audioSource = GetComponent<AudioSource>(); 
	}
	
	
	void FixedUpdate () {
        timer += Time.deltaTime;
	    if(Input.GetButton("Fire1")&&timer>=TimeBetweenAttacks)
        {
            Shoot();
        }
	}


    [Client]
    void Shoot()
    {
        timer = 0f;
        print("Player Shoot");
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        if (Physics.Raycast(shootRay,out hitPoint, range, mask))
        {
            if (hitPoint.collider.tag == "Player")
            {
                CmdPlayerShot(hitPoint.collider.name);
            }
            else
            {
                EnemyHealth enemyHealth = hitPoint.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    print("EnemyFound");
                    enemyHealth.TakeDamage(damagePerShot, hitPoint.point);
                }
            }
        }
    }

    [Command]
    void CmdPlayerShot(string _playerid)
    {
        Debug.Log(_playerid + " has been shot");
        PlayerHealth _player = GameManager.GetPlayer(_playerid);
        _player.RpcTakeDamage(damagePerShot);
    }
}
