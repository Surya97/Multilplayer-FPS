using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;


[RequireComponent(typeof(PlayerHealth))]
public class PlayerSetup : NetworkBehaviour {


    private static string _ID;
    private static PlayerHealth player;

	// Use this for initialization
	void Start () {

		if(isLocalPlayer)
        {
            EnableComponents();
        }
        else
        {
            ChangetoRemoteLayer();
        }
	}


    public override void OnStartClient()
    {
        base.OnStartClient();
        _ID = GetComponent<NetworkIdentity>().netId.ToString();
        player = GetComponent<PlayerHealth>();
        GameManager.RegisterPlayer(_ID,player);
    }


    void EnableComponents()
    {
        GetComponent<CharacterController>().enabled = true;
        GetComponent<FirstPersonController>().enabled = true;
        GetComponent<PlayerShoot>().enabled = true;
        foreach (Camera cam in GetComponentsInChildren<Camera>())
        {
            cam.enabled = true;
            cam.GetComponent<AudioListener>().enabled = true;
        }
        GetComponentInChildren<Scoped>().enabled = true;
    }

    void ChangetoRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
    }


    private void OnDisable()
    {
        GameManager.DeRegisterPlayer(transform.name);
    }


}
