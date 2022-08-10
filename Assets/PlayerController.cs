using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    float dotP;
    bool input;
    public CinemachineImpulseSource impulse;
    public Animator anim;
    public bool isShooting;
    void Start()
    {
        impulse = GetComponent<CinemachineImpulseSource>();

    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }
    // Update is called once per frame

    public void Update()
    {

        //dotP = Vector3.Dot(transform.localRotation.eulerAngles.normalized, Vector3.forward);
        input = Input.GetKeyDown(KeyCode.UpArrow);
        dotP = Vector3.Dot(transform.forward ,Vector3.forward);

        if (isShooting)
        {
            anim.SetBool("isShooting", true);

        }
        else
        {
            anim.SetBool("isShooting", false);

        }

        if (input)
        {
            impulse.GenerateImpulse();
            isShooting = true;

            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionX;



        }
    }
    public void ShootingAnimationComplete()
    {
        isShooting = false;
        Shoot();
    }

    public void Shoot()
    {
        var forceAmmount = 30f;

        if (dotP > 0f)
        {
            rb.freezeRotation = true;
            rb.freezeRotation = false;


            rb.AddTorque(-transform.right * 500f, ForceMode.VelocityChange);
            rb.AddForce(-transform.forward * forceAmmount, ForceMode.VelocityChange);
            rb.AddForce(-Vector3.forward * 5);


        }
        else
        {

            if (dotP == 0)
            {
                rb.AddForce(-transform.forward * forceAmmount, ForceMode.VelocityChange);
                rb.AddForce(Vector3.forward * 5);
            }
            else
            {
                rb.freezeRotation = true;
                rb.freezeRotation = false;
                rb.AddTorque(transform.right * 500f, ForceMode.VelocityChange);
                rb.AddForce(-transform.forward * forceAmmount, ForceMode.VelocityChange);
                rb.AddForce(Vector3.forward * 5);
            }



        }
    }
    void FixedUpdate()
    {

        

    }
}
