
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class moveorb : MonoBehaviour
{


    //Input's for movement
    public KeyCode moveL;
    public KeyCode moveR;
    public KeyCode jump;


    //Move LEFT RIGHT UP SOUND
    public AudioClip impact;

    //Cache the rigidbody
    public static Rigidbody selfRigidbody;


    //Particle Effects should go to maybe PARTICLE related scripts
    public Transform boomObj;

    public Transform Circular;

    //UIManager shall be deleted or no outcome for game
    public Slider HealthBar;





    //position of object not neccesary
    float z_pos = 0;


    //MovePosition Controller parameters 
    #region MovePosition Controller


    public  float speed = 0.1f;
    public Vector3 straightForward;
    public Vector3 relativelyUp;
    public Vector3 relativelyDown;

    public Quaternion spreadAngleRight;
    public Quaternion spreadAngleLeft;

    public Quaternion spreadAngleUp;

    public Quaternion spreadAngleDown;

    public Vector3 localRightVector;
    public Vector3 localLeftVector;
    public Vector3 localUpVector;
    public Vector3 localDownVector;

    public Vector3 newVectorRight;
    public Vector3 newVectorLeft;
    public Vector3 newVectorUp;
    public Vector3 newVectorDown;

    public float maxJumpHeight = 3.0f;
    public float groundHeight;
    public Vector3 groundPos;
    public float jumpSpeed = 7.0f;
    public float fallSpeed = 12.0f;
    public bool inputJump = false;
    public bool grounded = true;

    #endregion


    // Use this for initialization
    void Start()
    {

        HealthBar.maxValue = 100f;
        selfRigidbody = GetComponent<Rigidbody>();
        z_pos = 0;
        HealthBar.value = 100;

        


        /* MovePosition Controller parameters */

        straightForward = transform.TransformDirection(Vector3.forward);
        relativelyUp = transform.TransformDirection(new Vector3(0, 1, 1));
        relativelyDown = transform.TransformDirection(new Vector3(0, 1, 1));


        spreadAngleRight = Quaternion.AngleAxis(45, new Vector3(0, 1, 0));
        spreadAngleLeft = Quaternion.AngleAxis(315, new Vector3(0, 1, 0));
        spreadAngleUp = Quaternion.AngleAxis(30, new Vector3(0, 1, 1));
        spreadAngleDown = Quaternion.AngleAxis(330, new Vector3(0, 1, 1));

        localRightVector = spreadAngleRight * straightForward;
        localLeftVector = spreadAngleLeft * straightForward;
        localUpVector = spreadAngleUp * relativelyUp;
        localDownVector = spreadAngleUp * relativelyDown;

        newVectorRight = transform.TransformDirection(localRightVector);
        newVectorLeft = transform.TransformDirection(localLeftVector);
        newVectorUp = transform.TransformDirection(localUpVector);
        newVectorDown = transform.TransformDirection(localDownVector);

        groundPos = transform.position;
        groundHeight = transform.position.y;
        maxJumpHeight = transform.position.y + maxJumpHeight;







    }

    // Update is called once per frame
    void FixedUpdate()
    {


        //Try time.time = 0 later on.. usefull for slowmmotion mode
        if (PauseMenu.GameIsPaused)
        {
            return;
        }




        //Handle ball rotation
        RotationOfPlayerSphere();

        //scoreField.text = (System.Convert.ToInt32(scoreField.text) + 1 ).ToString();

        z_pos += DynamicGameSettings.maxSpeed; //Speed 


        //No physics controller
        selfRigidbody.transform.position = new Vector3(0, 1, z_pos); //Move Forward

        //Moving forward our sphere MovePosition Controller 
        // currentSpeed = transform.forward * Time.deltaTime * speed;
        //selfRigidbody.MovePosition(transform.position + currentSpeed);




        //TODO STOP player when colliding with an lethal object
        PlayerControllers();

    }

    private void RotationOfPlayerSphere()
    {
        float rotation = Input.GetAxis("Horizontal") * 100;
        rotation *= Time.deltaTime;
        selfRigidbody.AddRelativeTorque(Vector3.back * rotation);
        selfRigidbody.useGravity = true;
    }



    #region MovePosition Controllers
    /* Benefits
       1. Using physics and kinematic together
       2. Speed adjustment easily.
       3. Collision without changing the objects position (isKinematic)
       4. Çarptığında durma 
    
     */
    private void MovePositionControllers()
    {
        /* TODO
         1. Limit the left and right side of the enviroment
         2. Add Jump Feature
         3. 
        */
        //Works Perfect
        if (Input.GetKey(moveL))
        {
            Debug.Log("Left");
            selfRigidbody.MovePosition(transform.position + transform.TransformDirection(newVectorLeft * (speed)) * Time.deltaTime);

        }

        //Works Perfect
        if (Input.GetKey(moveR))
        {
            Debug.Log("Right");
            selfRigidbody.MovePosition(transform.position + transform.TransformDirection(newVectorRight * (speed)) * Time.deltaTime);

            // currentSpeed = transform.right * Time.deltaTime * speed;
            //selfRigidbody.MovePosition(transform.position + currentSpeed);
            //GetComponent<Rigidbody>().transform.position = new Vector3(1, 1, gameObject.transform.position.z);

        }


        if (Input.GetKey(KeyCode.Space))
        {

            Debug.Log("Space is bein presed");

            if (grounded)
            {
                groundPos = transform.position;
                inputJump = true;
                StartCoroutine("Jump");
            }
        }



        if (Input.GetKey(jump))
        {

            Debug.Log("Space is bein presed");

            if (grounded)
            {
                if (transform.position.y >= maxJumpHeight)
                {
                    Debug.Log("Max Height reached");
                    inputJump = false;
                }

                if (inputJump)
                {
                    Debug.Log("Jumping State");
                    selfRigidbody.MovePosition(transform.position + transform.TransformDirection(newVectorUp * (speed)) * Time.deltaTime);

                }
                else
                {
                    Debug.Log("Falling State");
                    selfRigidbody.MovePosition(transform.position + transform.TransformDirection(newVectorDown * (speed)) * Time.deltaTime);

                    if (transform.position.y <= 1f)
                    {
                        Debug.Log("Exiting Jumping State");
                        Vector3 n = new Vector3(transform.position.x, 1, transform.position.z);
                        transform.position = n;
                        grounded = true;
                    }
                }


            }

        }
        if (transform.position.y <= 1.15f)
        {
            grounded = true;
        }
        else
            grounded = false;


    }

    IEnumerator Jump()
    {
        while (true)
        {
            if (transform.position.y >= maxJumpHeight)
                inputJump = false;
            if (inputJump)
                transform.Translate(Vector3.up * jumpSpeed * Time.smoothDeltaTime);
            else if (!inputJump)
            {
                transform.position = Vector3.Lerp(transform.position, groundPos, fallSpeed * Time.smoothDeltaTime);
                if (transform.position == groundPos)
                    StopAllCoroutines();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    // IEnumerator Jump()
    // {
    //     if (grounded)
    //     {

    //         groundPos = transform.position;
    //         inputJump = true;

    //         if (transform.position.y >= maxJumpHeight)
    //         {
    //             inputJump = false;
    //             Debug.Log("maximum Height Reached");
    //         }

    //         if (inputJump)
    //         {
    //             Debug.Log("Jumpppping");
    //             selfRigidbody.MovePosition(transform.position + transform.TransformDirection(newVectorUp * (speed)) * Time.deltaTime);
    //         }
    //         else if (!inputJump)
    //         {
    //             Debug.Log("Fallingggg");
    //             selfRigidbody.MovePosition(transform.position + transform.TransformDirection(-newVectorUp * (speed)) * Time.deltaTime);
    //             //transform.position = Vector3.Lerp(transform.position, groundPos, fallSpeed * Time.smoothDeltaTime);
    //             if (transform.position.y <= 1f)
    //             {
    //                 Debug.Log("Exiting Jumping State");
    //                 Vector3 n = new Vector3(transform.position.x, 1, transform.position.z);
    //                 transform.position = n;
    //                 grounded = true;
    //                 StopAllCoroutines();


    //             }


    //         }

    //         yield return WaitAndDoSomething();

    //     }
    // }

    #endregion



    private void PlayerControllers()
    {
        if (Input.GetKey(moveL))
        {

            AudioManager.Instance.RandomizeSfx(impact);
            selfRigidbody.transform.position = new Vector3(-1, 1, gameObject.transform.position.z);

        }

        if (Input.GetKey(moveR))
        {
            AudioManager.Instance.RandomizeSfx(impact);
            selfRigidbody.transform.position = new Vector3(1, 1, gameObject.transform.position.z);

        }

        if (Input.GetKey(jump))
        {
            AudioManager.Instance.RandomizeSfx(impact);
            selfRigidbody.transform.position = new Vector3(gameObject.transform.position.x, 3, gameObject.transform.position.z);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        //TODO not kill the object
        //TODO make timer or quest based game
        if ( other.gameObject.tag == "Finish")
        {
            GM.lvlCompStatus = "Success";
            SceneManager.LoadScene(2);
        }
        if (other.gameObject.tag == "lethal")
        {
            UIManager.Instance.actualScoreMultipler = 1; //reset mult.
            HealthBar.value -= 5;
            Transform boom = Instantiate(boomObj, transform.position, boomObj.rotation); //Particle Effect 
            StartCoroutine(WaitAndDestroy(boom));

        }

        //TODO Collect data of Collectibles
        if (other.gameObject.name == "Coin(Clone)")
        {
            //Scoring 
            StartCoroutine(WaitAndDoSomething());
            HandleCollectibleCollision(other, 1);
        }

        if (other.gameObject.name == "Treasure(Clone)")
        {
            //Scoring 
            StartCoroutine(WaitAndDoSomething());
            HandleCollectibleCollision(other, 2);
        }

        if (other.gameObject.name == "Crystal(Clone)")
        {
            //Scoring 
            StartCoroutine(WaitAndDoSomething());

            HandleCollectibleCollision(other, 3);
        }



        if (other.gameObject.name == "HealthPWU(Clone)")
        {
            //Scoring 
            StartCoroutine(WaitAndDoSomething());
            HealthBar.value += 30;
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "ShieldPWU(Clone)")
        {
            //Scoring 
            StartCoroutine(WaitAndDoSomething());
            //TODO Shield Effect, FIRST COLLISION WİLL BE IGNORED    

            Destroy(other.gameObject);
        }


    }

    private void HandleCollectibleCollision(Collider other, int multiplier)
    {
        UIManager.Instance.scoreMultiplier.text = UIManager.Instance.actualScoreMultipler.ToString() + "x";
        UIManager.Instance.scoreFieldText.text = (int.Parse(UIManager.Instance.scoreFieldText.text) + multiplier * UIManager.Instance.actualScoreMultipler).ToString();
        UIManager.Instance.actualScoreMultipler += 1;

        GM.lvlCompStatus = "Success";
        Destroy(other.gameObject);
    }

    IEnumerator stopSlide()
    {

        yield return new WaitForSeconds(.5f);

        //controlLocked = "n";
    }

    IEnumerator WaitAndDoSomething()
    {
        yield return new WaitForSeconds(2);
        // do something

    }

    IEnumerator WaitAndDestroy(Transform obj)
    {
        yield return new WaitForSeconds(5f);
        Destroy(obj.gameObject);
    }
}
