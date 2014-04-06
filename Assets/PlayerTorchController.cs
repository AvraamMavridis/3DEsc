using UnityEngine;
using System.Collections;

public class PlayerTorchController : MonoBehaviour {
    GameObject player;
    GameObject torch;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("carl");
        torch = GameObject.Find("PlayerTorch");
	}
	
	// Update is called once per frame
	void Update () {

        //torch.transform.rotation = Quaternion.Euler(0, 0, 0);
        
	}
}
