using UnityEngine;
using System.Collections;

public class b9Mecanim04 : MonoBehaviour {

	// public float animSpeed = 1.5f;				// a public setting for overall animator animation speed
    public float DampTime = 3f;
	private Animator anim;							// a reference to the animator on the character
    private AnimatorStateInfo animState;			// a reference to the current state of the animator, used for base layer

    float h = 0f;				// setup h variable as our horizontal input axis
    float v = 0f;				// setup v variables as our vertical input axis
    public bool Altkey = false;     //is alt key pessed

    //Animator speed and turn rate
    float animSpeed;
    float animRot;
    float absSpeed;
    float absRot;

    //animation state hashes
	static int idleState = Animator.StringToHash("Base Layer.Stand_Idle");
    static int idleSwitchFeetState = Animator.StringToHash("Base Layer.Stand_Idle (change feet)");
    static int standAlertState= Animator.StringToHash("Base Layer.Alert");
    static int sideStepState = Animator.StringToHash("SIDESTEP.SideStep");
    static int walkRunState = Animator.StringToHash("WALK-RUN.WALK-RUN");
    static int walkRunBackState = Animator.StringToHash("WALK_BACK.WALK-RUN-BACK");   
    static int stand2walkState = Animator.StringToHash("WALK-RUN.Stand-2-Walk");
    static int walkState = Animator.StringToHash("WALK-RUN.Walk");
    static int standTurnState = Animator.StringToHash("TURN_ON_SPOT.TURN_ON_SPOT");

    //inputs new version
    //string currentButton;
    //string currentAxis;
    //float axisInput;
    float axLhoriz = 0f;        //main stick direction (XY)
    float axLvert = 0f;
    float axRhoriz = 0f;        //second stick direction
    float axRvert = 0f;
    //float ax5leftright = 0f;    //Dpad direction
    //float ax6updown = 0f;

    //bool JumpY = false;         //xboxY/up
    //bool CrouchA = false;       //xboxA/down
    float StrafeX = 0f;         //xboxX/left
    bool rightB = false;        //xboxB/right

    //bool fire1 = false;         //leftCtrl/mouseL/XboxFireR
    //bool fire2 = false;         //leftAlt/mouseR/XboxFireL
    //bool fire3 = false;         //leftShift/xboxBumpR
    //bool fire4 = false;         //xboxBumpL

	void Start () 
	{
		anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () 
	{
        //GenericInput();
        //LogicStates();
        JoyInput();
        AvatarSpeed();
    }

    void AvatarSpeed()
    {
        animSpeed = anim.deltaPosition.z * 100;
        animRot = anim.deltaRotation.y * 100;

        anim.SetFloat("animSpeed", animSpeed);
        anim.SetFloat("animRotation", animRot);

    }

    void JoyInput()
    {
        //Get Inputs
        axLhoriz = Input.GetAxis("LHorizontal");
        axLvert = Input.GetAxis("LVertical");
        absSpeed = Mathf.Abs(axLvert);
        absRot = Mathf.Abs(axLhoriz);

        axRhoriz = Input.GetAxis("RHorizontal");
        axRvert = Input.GetAxis("RVertical");
        //absSpeed = Mathf.Abs(axRvert);
        //absRot = Mathf.Abs(axRhoriz);


        if (Input.GetButton("joystick button 0"))
            StrafeX = Mathf.Lerp(StrafeX, 1f, DampTime * Time.deltaTime);
        else if (StrafeX > 0.01f && !Input.GetButton("joystick button 0"))
            StrafeX = Mathf.Lerp(StrafeX, 0f, DampTime * Time.deltaTime);

            anim.SetFloat("strafeX", StrafeX);


        

        //Control Animator
        anim.SetFloat("SpeedLY", axLvert);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
        anim.SetFloat("DirectionLX", axLhoriz); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis	
        anim.SetFloat("RVert", axRvert);
        anim.SetFloat("RHoriz", axRhoriz);

        anim.SetFloat("absSpeed", absSpeed);						// Speed absolute value
        anim.SetFloat("absRotation", absRot);						// Rotation absolute value

        //anim.SetBool("Fire1Ctrl", true);     
        //anim.SetBool("Fire2Alt", true);
        //anim.SetBool("Fire3Shift", true);
        //anim.SetBool("Fire4Stop", true);
        //anim.SetBool("xboxYjump", true);
        //anim.SetBool("xboxAcrouch", true);
        //anim.SetBool("Xstrafe", true);
        //anim.SetBool("xboxB", true);



        //if (Input.GetAxisRaw("X axis") > 0.3 || Input.GetAxisRaw("X axis") < -0.3)
        //{
        //    currentAxis = "X axis";
        //    axisInput = Input.GetAxisRaw("X axis");
        //}
    }

    void GenericInput()
    {
        h = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
        v = Input.GetAxis("Vertical");				// setup v variables as our vertical input axis
        anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
        anim.SetFloat("Direction", h); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis	


        //anim.SetFloat("Speed", Input.GetAxis("Vertical"));							// set our animator's float parameter 'Speed' equal to the vertical input axis				
        //anim.SetFloat("Direction", Input.GetAxis("Horizontal"), DampTime, Time.deltaTime); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis		
        animState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

        if (Input.GetKey(KeyCode.LeftAlt))		//While LeftShift pressed
        {
            //print("alt");
            Altkey = true;
            anim.SetBool("Alt", true);
        }
        else
        {
            Altkey = false;
            anim.SetBool("Alt", false);
        }
    }

    void LogicStates() {
        if (animState.nameHash == idleState)
		{
            // to Turn on place
            if (Altkey == false && h != 0f) //(!Input.anyKeyDown)
            {
                anim.CrossFade(standTurnState, 0f, -1, 0f);
                //print("turn");
            }
            //Idle variations
            else if (Input.GetKey(KeyCode.I)) //(!Input.anyKeyDown)
            {
                anim.CrossFade(idleSwitchFeetState, .3f, -1, 0f);
            }

            // to Alert
            else if (Input.GetKey(KeyCode.L)) //
            {
                anim.CrossFade(standAlertState, .3f, -1, 0f);
            }
            // to Sidestep -- implemented in Animator

            // to Look Left, Right, Over Shoulder
            // to Walk
		}

        if (animState.nameHash == standTurnState)
        {
            if (Input.GetKey(KeyCode.LeftShift))		//While LeftShift pressed
            {
                anim.SetFloat("Shift", 1f, DampTime, Time.deltaTime);
            }
            else
            {
                anim.SetFloat("Shift", 0f, DampTime, Time.deltaTime);
            }

            if (h == 0f)
            {
                anim.CrossFade(idleState, 0f, -1, 0f);
            }

        }

        if (animState.nameHash == stand2walkState)
        {
            if (Input.GetKey(KeyCode.DownArrow))   //(Input.GetAxis("Vertical") == 0 && Input.GetKey(KeyCode.DownArrow))
            {
                anim.CrossFade(standTurnState, .2f, -1, .1f);
            }
            //Debug.Log("trans");

        }

        if (animState.nameHash == walkRunState)
		{
            //print("walkrun state");
            if (Input.GetKey(KeyCode.LeftShift))		//While LeftShift pressed
            {
                anim.SetFloat("Shift", 1f, DampTime, Time.deltaTime);
            }
            else
            {
                anim.SetFloat("Shift", 0f, DampTime, Time.deltaTime);
            }

            //Debug.Log("walkRun");
			
		}

        if (animState.nameHash == walkRunBackState)
        {
            //print("walkrunBack state");
            if (Input.GetKey(KeyCode.LeftShift))		//While LeftShift pressed
            {
                anim.SetFloat("Shift", 1f, DampTime, Time.deltaTime);
            }
            else
            {
                anim.SetFloat("Shift", 0f, DampTime, Time.deltaTime);
            }


        }
	}

    //IEnumerator WaitSec()
    //{
    //    float waitTime = Random.Range(0.0f, 100.0f);
    //    yield return new WaitForSeconds(waitTime);

    //    //yield return new WaitForSeconds(5);
    //    //print(Time.time);
    //}

    //void OnGUI()
    //{
    //    Event e = Event.current;
    //    if (e.alt)      //Strafing in Animator
    //    {
    //        if (Application.platform == RuntimePlatform.OSXEditor)
    //        {
    //            Altkey = true;
    //            anim.SetBool("Alt", true);
    //        }
    //        else
    //            if (Application.platform == RuntimePlatform.WindowsEditor)
    //            {
    //                Altkey = true;
    //                anim.SetBool("Alt", true);
    //            }
    //    }
    //    else
    //    {
    //        Altkey = false;
    //        anim.SetBool("Alt", false);
    //    }
    //}

}
