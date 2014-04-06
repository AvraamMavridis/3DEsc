using UnityEngine;
using System.Collections;

public class MainLightController : MonoBehaviour {
    GameObject carlCamera;
	// Use this for initialization
	void Start () {
        carlCamera = GameObject.Find("carlCamera");
	}
	
	// Update is called once per frame
	void Update () {
        carlCamera.light.intensity = Random.Range(5.0F, 7.0F);
	}
}
