using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIControlar : MonoBehaviour
{
    [Header("Veriables")]
    public Vector3 move;
    [SerializeField] private float speed,jumforce,gravity,vectorialvelocity,up;
    private bool doublejump;
    private bool wallslide,turn,jump,Superjump;

    [Header("Components")]
    private CharacterController charController;
    private Animator anim;

    public string[] Bot1Name = { "Joy", "Tazim", "azad", "Tonmoy", "sadik", "Sakib", "Rifat", "Safayt", "Najmul" };

    void Awake()
    {
        charController = GetComponent<CharacterController>(); 
        anim = transform.GetChild(0).GetComponent<Animator>();   
    }
    void Start()
    {
        gameObject.name = Bot1Name[Random.Range(0, Bot1Name.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.finish)
        {
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
            jump = true;
            wallslide =false;
            vectorialvelocity = -1;
            Raycasting();
            if(Superjump)
            {
                Superjump = false;
                vectorialvelocity = jumforce *1.75f;
                anim.SetTrigger("Jump");
            }
        }
        else 
        {
            if(!wallslide)
            {
                gravity = 30;
                vectorialvelocity -= gravity * Time.deltaTime;
            }
            // if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && doublejump)
            // {
            //     vectorialvelocity += jumforce * .5f;
            //     doublejump = false;
            // }
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

    private void Raycasting()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position ,transform.forward,out hit,8f))
        {
            Debug.DrawLine(transform.position,hit.point,Color.red);
            
            if(hit.collider.tag == "Wall")
            {
                vectorialvelocity = jumforce;
                anim.SetTrigger("Jump");
            }
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.tag == "Wall")
        {
            if(jump)
                StartCoroutine(Latejump(Random.Range(0.1f,.2f)));

            if(vectorialvelocity <0)
            wallslide = true;
        }
        if(hit.collider.tag == "Slide" && charController.isGrounded)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180,
                    transform.eulerAngles.z);
        }
        else if(hit.collider.tag == "Slide")
            wallslide = true;
        
        if(hit.collider.tag == "SuperJump")
            Superjump = true;
        
    }
    IEnumerator Latejump(float time)
    {
        jump = false;
        wallslide = true;
        yield return new WaitForSeconds(time);
        if(!charController.isGrounded)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180,
                    transform.eulerAngles.z);
            vectorialvelocity = jumforce;
            anim.SetTrigger("Jump");
        }
        jump =true;
        wallslide = false;
    }
}
