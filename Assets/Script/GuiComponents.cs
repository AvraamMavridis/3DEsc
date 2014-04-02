using UnityEngine;
using System.Collections;

public class GuiComponents : MonoBehaviour {

    GameObject player;
    bool GUIEnabled = false;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("carl");
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F1))
        {
            GUIEnabled = !GUIEnabled;
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            PlayerController.speedFactor += 10;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerController.speedFactor -= 10;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerController.rotationFactor += 100;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerController.rotationFactor -= 100;
        }
        //print(player.rigidbody.velocity.magnitude + " " + PlayerController.speedFactor);
	}

    void OnGUI() {
        if (GUIEnabled)
        {
            GUI.Box(new Rect(0, 0, 550, 100), "Information");
            GUI.Label(new Rect(3, 20, 150, 20), "Speed Factor: " + PlayerController.speedFactor.ToString());
            PlayerController.speedFactor = GUI.HorizontalSlider(new Rect(160, 25, 350, 20), PlayerController.speedFactor, 0.0F, 500F);
            GUI.Label(new Rect(3, 40, 150, 20), "Speed: " + (player.rigidbody.velocity.magnitude).ToString());
            GUI.Label(new Rect(3, 60, 150, 20), "Rotation Factor: " + PlayerController.rotationFactor.ToString());
            PlayerController.rotationFactor = GUI.HorizontalSlider(new Rect(160, 65, 350, 20), PlayerController.rotationFactor, 0.0F, 500F);
            GUI.Label(new Rect(3, 80, 150, 20), "Rotation: " + (player.rigidbody.angularVelocity.magnitude).ToString());
        }

    }
}
