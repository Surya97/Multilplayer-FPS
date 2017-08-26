using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoped : MonoBehaviour {

    Animator anim;
    private bool isScoped=false;
    private GameObject scopeOverlay;
    public GameObject WeaponCamera;

    public float scopedFOV=15f;
    private float normalFOV;
    // Update is called once per frame

    private void Start()
    {
        scopeOverlay = GameObject.Find("Canvas");
        anim = GetComponent<Animator>();
        //print(scopeOverlay);
    }

    void Update () {
		if(Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            anim.SetBool("Scoped", isScoped);
            if (isScoped)
            {
                StartCoroutine(onScoped());
            }
            else
                onUnscoped();
        }
	}

    IEnumerator onScoped()
    {
        yield return new WaitForSeconds(0.15f);
        Color c=scopeOverlay.GetComponentInChildren<Image>().color;
        c.a = 255;
        scopeOverlay.GetComponentInChildren<Image>().color = c;
        scopeOverlay.GetComponentInChildren<Image>().enabled=true;
        WeaponCamera.SetActive(false);
        normalFOV = Camera.main.fieldOfView;
        Camera.main.fieldOfView = scopedFOV;
    }

     void onUnscoped()
    {
        scopeOverlay.GetComponent<Image>().enabled = false;
        WeaponCamera.SetActive(true);
        Camera.main.fieldOfView = normalFOV;
    }
}
