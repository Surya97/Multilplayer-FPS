using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

    [SerializeField]
    private int initialHealth = 100;

    [SyncVar]
    public int currentHealth;

    [SyncVar]
    private bool _isDead = false;

    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    public void Awake()
    {
        currentHealth = initialHealth;
    }

    [ClientRpc]
    public void RpcTakeDamage(int amount)
    {
        if (isDead)
            return;
        currentHealth -= amount;
        Debug.Log(transform.name + " now has health " + currentHealth);

        if(currentHealth<=0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log(transform.name + " DEAD!");
        Network.Destroy(gameObject);
    }
}
