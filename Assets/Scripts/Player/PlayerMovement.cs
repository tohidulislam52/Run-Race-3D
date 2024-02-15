using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Veriables")]
    public Vector3 move;
    [SerializeField] private float speed,jumforce,gravity,vectorialvelocity;
    private bool doublejump;
    private bool wallslide,turn,Superjump;

    [Header("Components")]
    private CharacterController charController;
    private Animator anim;
    //{ "Joy","Tazim","azad","Tonmoy", };


    private SkinnedMeshRenderer _skinnedMesh;
    [SerializeField] private Material[] _colors;

    void Awake()
    {
        _skinnedMesh = GameObject.Find("PlayerColor").GetComponent<SkinnedMeshRenderer>();
        charController = GetComponent<CharacterController>(); 
        anim = transform.GetChild(0).GetComponent<Animator>();   
    }
    void Start()
    {
        _skinnedMesh.material = _colors[PlayerPrefs.GetInt("PlayerColor",0)];
        gameObject.name = PlayerPrefs.GetString("PlayerName");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.finish)
        {
            move =Vector3.zero;
            if(!charController.isGrounded)
                vectorialvelocity -= gravity *Time.deltaTime;
            else
                vectorialvelocity = 0;
            
            move.y = vectorialvelocity;
            charController.Move(new Vector3(0,move.y*Time.deltaTime,0));
            
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
            {
                anim.SetTrigger("Dance");
                transform.eulerAngles = Vector3.up * 180;
            }
            return;
        }
        
        if(!GameManager.instance.start)
            return;

        move = Vector3.zero;
        move = transform.forward;
        if(charController.isGrounded)
        {
            wallslide = false;
            vectorialvelocity = -1;
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                vectorialvelocity = jumforce;
                doublejump = true;
                anim.SetTrigger("Jump");
            }
            if(turn)
            {
                turn = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y +180,
                     transform.eulerAngles.z);
            }
        }
        else 
        {
            if(!wallslide)
            {
                gravity = 30;
                vectorialvelocity -= gravity * Time.deltaTime;
            }
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && doublejump)
            {
                vectorialvelocity += jumforce * .2f;
                doublejump = false;
            }
        }
        if(wallslide)
        {
             vectorialvelocity -= gravity*.4f * Time.deltaTime;
            
        }

        if(Superjump)
        {
            Superjump = false;
            vectorialvelocity = jumforce *1.75f;
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                anim.SetTrigger("Jump");
        }
        

        anim.SetBool("Grounded",charController.isGrounded);
        anim.SetBool("Wallslide",wallslide);

        move.Normalize();
        move*=speed;
        move.y = vectorialvelocity;
        // Move= transform.forward;
        charController.Move(move*Time.deltaTime);
        // Debug.Log(charController.isGrounded);
        // Debug.Log(wallslide);

    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!charController.isGrounded)
        {
            if(hit.collider.tag == "Wall" || hit.collider.tag == "Slide")
            {
                if(vectorialvelocity < -.7f)
                    wallslide = true;
                if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    
                    vectorialvelocity = jumforce;
                    anim.SetTrigger("Jump");
                    doublejump = false;

                     transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y +180,
                     transform.eulerAngles.z);
                     wallslide = false;
                }
            }
        }
        else
        {
            if(transform.forward != hit.collider.transform.right && hit.collider.tag == "Ground" && !turn)
            {
                 turn = true;
            }
        }

        if(hit.collider.tag == "SuperJump")
            Superjump = true;
    }

    
}
