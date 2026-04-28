using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    AudioSource m_AudioSource;
    public InputAction MoveAction;
    Animator m_Animator;

    public float walkSpeed = 1.0f;
    public float turnSpeed = 20f;
    private float boostSpeed = 1f;
    bool boostActive = false;
    public bool boostReady = true;
    public GameObject BoostIcon;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start ()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_Animator = GetComponent<Animator> ();

        m_Rigidbody = GetComponent<Rigidbody> ();
        MoveAction.Enable();
    }

    void FixedUpdate ()
    {
        var pos = MoveAction.ReadValue<Vector2>();
        
        float horizontal = pos.x;
        float vertical = pos.y;
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();
        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);

        Boost();
        if (boostReady == true)
        {
            BoostIcon.SetActive (true);
        }
        else
        {
            BoostIcon.SetActive (false);
        }
            

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
        
            m_Rigidbody.MoveRotation(m_Rotation);
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * walkSpeed * boostSpeed * Time.deltaTime);
        if (isWalking)
{
    if (!m_AudioSource.isPlaying)
    {
        m_AudioSource.Play();
    }
}
else
{
    m_AudioSource.Stop();
}
    }
    async Task Boost()
    {
        if (boostReady != false && Input.GetButtonDown("Jump"))
        {
            boostReady = false;
            boostActive = true;
            await Task.Delay (4000);
            boostActive = false;
            await Task.Delay(10000);
            boostReady = true;
        }
        if (boostActive == true)
        {
            boostSpeed = 3f;
        }
        else
        {
            boostSpeed = 1f;
        }
    }
}
    
