using UnityEngine;
using System.Collections;

public class GuiComponents : MonoBehaviour {

    GameObject player;
    GameObject cameraView;
    GameObject mainCamera;
    GameObject miniMap;
    GameObject mainLight;
    bool GUIEnabled = false;
    string movementlabel;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("carl");
        cameraView = GameObject.Find("ColorImagePlane");
        mainCamera = GameObject.Find("carlCamera");
        miniMap = GameObject.Find("MiniMap");
        mainLight = GameObject.Find("MainLight");
        movementlabel = "Horizontal body movement";
        miniMap.camera.enabled = false;
        cameraView.renderer.enabled = false;
        mainLight.light.enabled = false;
        PlayerController.circleradious = 0;
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
                movementlabel = "Horizontal body movement";
            }
            else if (PlayerController.movement == PlayerController.MoveType.KinectMovement)
            {
                PlayerController.movement = PlayerController.MoveType.CenterPointKinectMovement;
                PlayerController.speedFactor = 150;
                PlayerController.rotationFactor = 134;
                movementlabel = "Central Point with hands for speed";
            }
            else if (PlayerController.movement == PlayerController.MoveType.CenterPointKinectMovement)
            {
                PlayerController.movement = PlayerController.MoveType.CenterPointKinectWithoutHands;
                PlayerController.rotationFactor = 15;
                PlayerController.speedFactor = 50;
                movementlabel = "Central Point without hands for speed";
            }
            else if (PlayerController.movement == PlayerController.MoveType.CenterPointKinectWithoutHands)
            {
                PlayerController.movement = PlayerController.MoveType.KeyboardMovement;
                PlayerController.rotationFactor = 15;
                PlayerController.speedFactor = 50;
                movementlabel = "Keyaboard";
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
            GUI.Box(new Rect(0, 0, 550, 150), "Movement type : " + movementlabel);
            GUI.Label(new Rect(3, 20, 150, 20), "Speed Factor: " + PlayerController.speedFactor.ToString());
            PlayerController.speedFactor = GUI.HorizontalSlider(new Rect(160, 25, 350, 20), PlayerController.speedFactor, 0.0F, 100F);
            GUI.Label(new Rect(3, 40, 150, 20), "Velocity: " + (player.rigidbody.velocity.magnitude).ToString());
            if((PlayerController.movement == PlayerController.MoveType.KinectMovement)|| (PlayerController.movement == PlayerController.MoveType.KeyboardMovement)){
                GUI.Label(new Rect(3, 60, 150, 20), "Rotation Factor: " + PlayerController.rotationFactor.ToString());
                PlayerController.rotationFactor = GUI.HorizontalSlider(new Rect(160, 65, 350, 20), PlayerController.rotationFactor, 0.0F, 1000F);
                GUI.Label(new Rect(3, 80, 150, 20), "Angular Velocity: " + (player.rigidbody.angularVelocity.magnitude).ToString());
            }
            else if (PlayerController.movement == PlayerController.MoveType.CenterPointKinectWithoutHands) {
                GUI.Label(new Rect(3, 60, 150, 40), "No-speed circle radious: " + PlayerController.circleradious.ToString());
                PlayerController.circleradious = GUI.HorizontalSlider(new Rect(160, 65, 350, 20), PlayerController.circleradious, 0.0F, 3.0F);
            }
            GUI.Label(new Rect(3, 100, 150, 20), "Camera distance: " + mainCamera.transform.position.y);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, GUI.HorizontalSlider(new Rect(160, 105, 350, 20), mainCamera.transform.position.y, 0.0F, 100F), mainCamera.transform.position.z);
        }

    }
}
