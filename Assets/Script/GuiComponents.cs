using UnityEngine;
using System.Collections;

public class GuiComponents : MonoBehaviour {

    GameObject player;
    GameObject cameraView;
    GameObject miniMap;
    GameObject mainLight;
    bool GUIEnabled = false;
    string movementlabel;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("carl");
        cameraView = GameObject.Find("ColorImagePlane");
        miniMap = GameObject.Find("MiniMap");
        mainLight = GameObject.Find("MainLight");
        movementlabel = "hipCenter";
        miniMap.camera.enabled = false;
        cameraView.renderer.enabled = false;
        mainLight.light.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
       
        //enable kinect camera
        if (Input.GetKeyDown(KeyCode.F1))
        {
            cameraView.renderer.enabled = !cameraView.renderer.enabled;
        }
        //enable camera view
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GUIEnabled = !GUIEnabled;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (PlayerController.movement == PlayerController.MoveType.KeyboardMovement)
            {
                PlayerController.movement = PlayerController.MoveType.KinectMovement;
                PlayerController.speedFactor = 150;
                PlayerController.rotationFactor = 134;
                movementlabel = "hipCenter";
            }
            else if (PlayerController.movement == PlayerController.MoveType.KinectMovement)
            {
                PlayerController.movement = PlayerController.MoveType.CenterPointKinectMovement;
                PlayerController.speedFactor = 150;
                PlayerController.rotationFactor = 134;
                movementlabel = "centerpoint";
            }
            else if (PlayerController.movement == PlayerController.MoveType.CenterPointKinectMovement)
            {
                PlayerController.movement = PlayerController.MoveType.KeyboardMovement;
                PlayerController.rotationFactor = 80;
                PlayerController.speedFactor = 15;
                movementlabel = "centerpoint";
                movementlabel = "keyboard";
            }
        }

        if (Input.GetKeyDown(KeyCode.F4)) {
            miniMap.camera.enabled = !miniMap.camera.enabled;
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            mainLight.light.enabled = !mainLight.light.enabled;
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
            GUI.Box(new Rect(0, 0, 550, 150), "Information");
            GUI.Label(new Rect(3, 20, 150, 20), "Speed Factor: " + PlayerController.speedFactor.ToString());
            PlayerController.speedFactor = GUI.HorizontalSlider(new Rect(160, 25, 350, 20), PlayerController.speedFactor, 0.0F, 5000F);
            GUI.Label(new Rect(3, 40, 150, 20), "Velocity: " + (player.rigidbody.velocity.magnitude).ToString());
            GUI.Label(new Rect(3, 60, 150, 20), "Rotation Factor: " + PlayerController.rotationFactor.ToString());
            PlayerController.rotationFactor = GUI.HorizontalSlider(new Rect(160, 65, 350, 20), PlayerController.rotationFactor, 0.0F, 1000F);
            GUI.Label(new Rect(3, 80, 150, 20), "Angular Velocity: " + (player.rigidbody.angularVelocity.magnitude).ToString());
            GUI.Label(new Rect(3, 100, 150, 20),  movementlabel);           
        }

    }
}
