using UnityEngine;
using System.Collections;

public class b9Mecanim04 : MonoBehaviour {

    public float DampTime = 3f;                     // adjust motion lerping:  0 - infinity, 10 almost instant, default 3
	private Animator anim;							// a reference to the animator on the character

    //Actual Animator speed and turn rates
    float animSpeedABS;             //absolute values of Animator
    float animRotABS;

    float strafe = 0f;              //strafing speed
    float walk = 0f;                 //walk speed
    float run = 0f;                 //running speed
    float turn = 0f;                 //turning speed

    //===OLD===
    // private AnimatorStateInfo animState;			// a reference to the current state of the animator, used for base layer
    //float h = 0f;				// setup h variable as our horizontal input axis
    //float v = 0f;				// setup v variables as our vertical input axis
    //public bool Altkey = false;     //is alt key pessed
    ////animation state hashes
    //static int idleState = Animator.StringToHash("Base Layer.Stand_Idle");
    //static int idleSwitchFeetState = Animator.StringToHash("Base Layer.Stand_Idle (change feet)");
    //static int standAlertState = Animator.StringToHash("Base Layer.Alert");
    //static int sideStepState = Animator.StringToHash("SIDESTEP.SideStep");
    //static int walkRunState = Animator.StringToHash("WALK-RUN.WALK-RUN");
    //static int walkRunBackState = Animator.StringToHash("WALK_BACK.WALK-RUN-BACK");
    //static int stand2walkState = Animator.StringToHash("WALK-RUN.Stand-2-Walk");
    //static int walkState = Animator.StringToHash("WALK-RUN.Walk");
    //static int standTurnState = Animator.StringToHash("TURN_ON_SPOT.TURN_ON_SPOT");

	void Start () 
	{
		anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () 
	{
        JoyInput();         //Apply inputs
        AvatarSpeed();      //Read avatar current condition

        //GenericInput();
        //LogicStates();
    }

    void AvatarSpeed()
    {
        //animSpeedABS = anim.deltaPosition.z * 100;                                //unclamped
        //animRotABS = anim.deltaRotation.y * 100;
        animSpeedABS = Mathf.Clamp01(Mathf.Abs(anim.deltaPosition.z * 100)/4.5f);   //scaledValue = rawValue / max; //Scale a value with min=0
        animRotABS = Mathf.Clamp01(Mathf.Abs(anim.deltaRotation.y * 100)/1.5f);     //scaledValue = (rawValue - min) / (max - min);  //Scale a value
        anim.SetFloat("absSpeed", animSpeedABS);    //input Speed absolute value
        anim.SetFloat("absRotation", animRotABS);   //input Direcion absolute value
    }

    void JoyInput()
    {

        //Walk
        if (Input.GetKey(KeyCode.UpArrow))
            walk = Mathf.Lerp(walk, .5f, DampTime * Time.deltaTime);
        else if (Input.GetKey(KeyCode.DownArrow))
            walk = Mathf.Lerp(walk, -.5f, DampTime * Time.deltaTime);
        else if (!Input.GetKey(KeyCode.UpArrow) || !Input.GetKey(KeyCode.DownArrow))
        {
            walk = Mathf.Lerp(walk, 0f, DampTime * Time.deltaTime);
            run = Mathf.Lerp(run, 0f, DampTime * Time.deltaTime);
        }

        //Run
        if (Mathf.Abs(walk) > .05)
        {
            if (Input.GetKey(KeyCode.LeftShift) && (Mathf.Sign(walk) == 1))
                run = Mathf.Lerp(run, .5f, DampTime * Time.deltaTime);
            else if (Input.GetKey(KeyCode.LeftShift) && (Mathf.Sign(walk) == -1))
                run = Mathf.Lerp(run, -.5f, DampTime * Time.deltaTime);
            else if (!Input.GetKey(KeyCode.LeftShift))
                run = Mathf.Lerp(run, 0f, DampTime * Time.deltaTime);
        }

        //Alt-Strafe
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftArrow))
        {
            strafe = Mathf.Lerp(strafe, -1f, DampTime * Time.deltaTime);
            turn = Input.GetAxis("LHorizontal") * -1;
        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.RightArrow))
        {
            strafe = Mathf.Lerp(strafe, 1f, DampTime * Time.deltaTime);
            turn = Input.GetAxis("LHorizontal") * -1;
        }
        else if (!Input.GetKey(KeyCode.LeftAlt) || (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)))
            strafe = Mathf.Lerp(strafe, 0f, DampTime * Time.deltaTime);
            

        //Turn
        if (Input.GetKey(KeyCode.LeftArrow))
            turn = Mathf.Lerp(turn, -1f, DampTime * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow))
            turn = Mathf.Lerp(turn, 1f, DampTime * Time.deltaTime);
        else if (!Input.GetKey(KeyCode.LeftArrow) || !Input.GetKey(KeyCode.RightArrow))
            turn = Mathf.Lerp(turn, 0f, DampTime * Time.deltaTime);

        //Random Integer
        anim.SetFloat("Rand", Random.value);

        // Stick Controls
        anim.SetFloat("LVert", Input.GetAxis("LVertical")+walk+run);							// set our animator's Speed to the Left Stick vertical input axis				
        anim.SetFloat("LHoriz", Input.GetAxis("LHorizontal")+turn); 						// set our animator's Direction to the Left Stick horizontal input axis	
        anim.SetFloat("RVert", Input.GetAxis("RVertical"));                         // Right Stick Vertical
        anim.SetFloat("RHoriz", (Input.GetAxis("RHorizontal")+strafe));             // Right Stick Horizontal

        //anim.SetBool("Fire1Ctrl", true);     
        //Button Fire
        if (Input.GetButtonDown("joystick button 5"))
            anim.SetBool("Fire1Ctrl", true);
        else if (Input.GetButtonUp("joystick button 5"))
            anim.SetBool("Fire1Ctrl", false);
        //anim.SetBool("Fire2Alt", true);
        //anim.SetBool("Fire3Shift", true);
            //Button Alert
        if (Input.GetButtonDown("joystick button 4"))
            anim.SetBool("Fire4Alert", true);
        else if (Input.GetButtonUp("joystick button 4"))
            anim.SetBool("Fire4Alert", false);

        
        //anim.SetBool("xboxYjump", true);
        //anim.SetBool("xboxAcrouch", true);
        //anim.SetBool("Xstrafe", true);
        //anim.SetBool("xboxB", true);

        //Example Button lerping
        //if (Input.GetButton("joystick button 0"))
        //    StrafeX = Mathf.Lerp(StrafeX, 1f, DampTime * Time.deltaTime);
        //else if (StrafeX > 0.01f && !Input.GetButton("joystick button 0"))
        //    StrafeX = Mathf.Lerp(StrafeX, 0f, DampTime * Time.deltaTime);

        //    anim.SetFloat("strafeX", StrafeX);


    }

    //=======OLD================

    //void GenericInput()
    //{
    //    h = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
    //    v = Input.GetAxis("Vertical");				// setup v variables as our vertical input axis
    //    anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
    //    anim.SetFloat("Direction", h); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis	


    //    //anim.SetFloat("Speed", Input.GetAxis("Vertical"));							// set our animator's float parameter 'Speed' equal to the vertical input axis				
    //    //anim.SetFloat("Direction", Input.GetAxis("Horizontal"), DampTime, Time.deltaTime); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis		
    //    animState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

    //    if (Input.GetKey(KeyCode.LeftAlt))		//While LeftShift pressed
    //    {
    //        //print("alt");
    //        Altkey = true;
    //        anim.SetBool("Alt", true);
    //    }
    //    else
    //    {
    //        Altkey = false;
    //        anim.SetBool("Alt", false);
    //    }
    //}

    //void LogicStates() {
    //    if (animState.nameHash == idleState)
    //    {
    //        // to Turn on place
    //        if (Altkey == false && h != 0f) //(!Input.anyKeyDown)
    //        {
    //            anim.CrossFade(standTurnState, 0f, -1, 0f);
    //            //print("turn");
    //        }
    //        //Idle variations
    //        else if (Input.GetKey(KeyCode.I)) //(!Input.anyKeyDown)
    //        {
    //            anim.CrossFade(idleSwitchFeetState, .3f, -1, 0f);
    //        }

    //        // to Alert
    //        else if (Input.GetKey(KeyCode.L)) //
    //        {
    //            anim.CrossFade(standAlertState, .3f, -1, 0f);
    //        }
    //        // to Sidestep -- implemented in Animator

    //        // to Look Left, Right, Over Shoulder
    //        // to Walk
    //    }

    //    if (animState.nameHash == standTurnState)
    //    {
    //        if (Input.GetKey(KeyCode.LeftShift))		//While LeftShift pressed
    //        {
    //            anim.SetFloat("Shift", 1f, DampTime, Time.deltaTime);
    //        }
    //        else
    //        {
    //            anim.SetFloat("Shift", 0f, DampTime, Time.deltaTime);
    //        }

    //        if (h == 0f)
    //        {
    //            anim.CrossFade(idleState, 0f, -1, 0f);
    //        }

    //    }

    //    if (animState.nameHash == stand2walkState)
    //    {
    //        if (Input.GetKey(KeyCode.DownArrow))   //(Input.GetAxis("Vertical") == 0 && Input.GetKey(KeyCode.DownArrow))
    //        {
    //            anim.CrossFade(standTurnState, .2f, -1, .1f);
    //        }
    //        //Debug.Log("trans");

    //    }

    //    if (animState.nameHash == walkRunState)
    //    {
    //        //print("walkrun state");
    //        if (Input.GetKey(KeyCode.LeftShift))		//While LeftShift pressed
    //        {
    //            anim.SetFloat("Shift", 1f, DampTime, Time.deltaTime);
    //        }
    //        else
    //        {
    //            anim.SetFloat("Shift", 0f, DampTime, Time.deltaTime);
    //        }

    //        //Debug.Log("walkRun");
			
    //    }

    //    if (animState.nameHash == walkRunBackState)
    //    {
    //        //print("walkrunBack state");
    //        if (Input.GetKey(KeyCode.LeftShift))		//While LeftShift pressed
    //        {
    //            anim.SetFloat("Shift", 1f, DampTime, Time.deltaTime);
    //        }
    //        else
    //        {
    //            anim.SetFloat("Shift", 0f, DampTime, Time.deltaTime);
    //        }


    //    }
    //}

    //=======UNUSED================

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
