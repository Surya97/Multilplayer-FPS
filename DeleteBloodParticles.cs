using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBloodParticles : MonoBehaviour {

    private float timer = 0f;
    public float destroyAfter = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer += Time.deltaTime;
        if (timer >= destroyAfter)
            Destroy(gameObject);
	}
}
