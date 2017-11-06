using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    // Movement
    public float initSpeed; // Player velocity in z-axis
    private float velocityY, velocityZ;

    // Jump
    public float jumpPulse = 5f; // Upward pulse
    public AudioClip jumpSound;
    //public float fallingSpeed = 9.8f; // Falling speed defaults to gravity
    private bool isGrounded = true; // Is the character on the floor
    private float initialY;
    

    // Cameras
    public GameObject cam1, cam2; // FPS & TPS cameras
    private bool isCam1; // Boolean for toggling cams

    private Lane currLane; // Current Lane
    private enum Direction {Right, Left} // Possible actions

    private const float accThreshold = 0.5f; // Mobile acceloremeter threshold

    private Rigidbody rigid;
    private bool movFinished = true;
    // Use this for initialization
    void Start () {
        currLane = Lane.Middle;
        isCam1 = true;
        rigid = GetComponent<Rigidbody>();
        initialY = rigid.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        // Character returned to ground
        if (rigid.position.y <= initialY || rigid.velocity.y == 0)
        {
            isGrounded = true;
            rigid.position = new Vector3(rigid.position.x, initialY, rigid.position.z);
        }

        // Inputs
        if (Input.GetKeyDown(KeyCode.C))  // Toggle cameras when C is pressed
            ToggleCameras();
        else if (LeftPressed() && movFinished) // Move left
            StartCoroutine(MoveToLane(NextLane(Direction.Left)));
        else if (RightPressed() && movFinished) // Move right
            StartCoroutine(MoveToLane(NextLane(Direction.Right)));
    }

    void FixedUpdate()
    {
        velocityY = rigid.velocity.y;
        velocityZ = initSpeed + (GameManager.score / 50) * 2;

        // Move forward while preserving vertical velocity
        rigid.velocity = new Vector3(rigid.velocity.x, velocityY, velocityZ);


        // Inputs
        if (JumpPressed())
        {
            PerformJump();
        }
    }

    // Check if a left key is pressed or triggered
    bool LeftPressed()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.acceleration.x < -accThreshold;
    }

    // Check if a right key is pressed or triggered
    bool RightPressed()
    {
        return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.acceleration.x > accThreshold;
    }

    // Check if a right key is pressed or triggered
    bool JumpPressed()
    {
        return Input.GetButton("Jump");
    }

    public void PerformJump()
    {
        if (isGrounded) // Not to spam jumps
        {
            isGrounded = false;
            rigid.AddForce(new Vector3(0, jumpPulse, 0), ForceMode.Impulse);

            // Play Sound
            GetComponent<AudioSource>().clip = jumpSound;
            GetComponent<AudioSource>().Play();
        }
    }

    // Toggle Cameras
    public void ToggleCameras()
    {
        isCam1 = !isCam1;
        cam1.SetActive(isCam1);
        cam2.SetActive(!isCam1);
    }

    // Move the player smoothly to a lane
    IEnumerator MoveToLane(Lane lane)
    {
        float totalTime = 0.25f; // Animation for t seconds
        float elapsedTime = 0f;

        Vector3 startPos = rigid.position;
        Vector3 endPos = new Vector3(GameManager.lanePositions[lane], startPos.y, startPos.z + velocityZ * totalTime);

        while (elapsedTime < totalTime)
        {
            rigid.position = Vector3.Lerp(startPos, endPos, (elapsedTime / totalTime));
            elapsedTime += Time.deltaTime;
            movFinished = false;
            yield return new WaitForEndOfFrame();
        }
        movFinished = true;
    }

    // What next lane should the player be in
    private Lane NextLane(Direction dir)
    {
        if (currLane == Lane.Middle)
        {
            if (dir == Direction.Left)
                currLane = Lane.Left;
            else currLane = Lane.Right;
        }
        else if (currLane == Lane.Left)
        {
            if (dir == Direction.Right)
                currLane = Lane.Middle;
        }
        else if (currLane == Lane.Right)
        {
            if (dir == Direction.Left)
                currLane = Lane.Middle;
        }

        return currLane;
    }
}
