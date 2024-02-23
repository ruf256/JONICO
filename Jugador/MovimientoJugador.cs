using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    CharacterController cc;

    public AudioSource movimientoAudioSource;
    public AudioClip jumpClip;
    public AudioClip landClip;

    [HideInInspector]
    public StatsJugador statsJugador;

    [HideInInspector]
    public float speed;

    float rotationX = 0;

    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;

    [HideInInspector]
    public bool canMove = true;

    [HideInInspector]
    public bool isMoving = false;

    private Camera playerCamera;

    private float timerCooldownHabilidad;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        statsJugador = GetComponent<StatsJugador>(); 
    }

    private void Start()
    {
        StateManager.Instance.CambioEstado += CambioEstado;
        speed = statsJugador.data.initialSpeed;
        timerCooldownHabilidad = 100;
    }


    // Update is called once per frame
    void Update()
    {
        if (StateManager.Instance.isEsperando()) return;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        moveDirection *= speed;
        

        isMoving = moveDirection != Vector3.zero;

        if (Input.GetButton("Jump") && canMove && cc.isGrounded)
        {
            moveDirection.y = statsJugador.data.jumpSpeed;
            if (StateManager.Instance.isJonico())
            {
                statsJugador.data.habilidad.EjecutarHabilidad(this);
            }
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && StateManager.Instance.isLidio() && statsJugador.data.cooldownHabilidad < timerCooldownHabilidad )
        {
            statsJugador.data.habilidad.EjecutarHabilidad(this);
            timerCooldownHabilidad = 0;
        }
        timerCooldownHabilidad += Time.deltaTime;

        if (!cc.isGrounded)
        {
            moveDirection.y -= statsJugador.data.gravity * Time.deltaTime;
        }

        // Move the controller
        cc.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * statsJugador.data.lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -statsJugador.data.lookXLimit, statsJugador.data.lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * statsJugador.data.lookSpeed, 0);
        }
    }


    private void CambioEstado(object sender, EventArgs e)
    {
        speed = statsJugador.data.initialSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Limite")
        {

            StartCoroutine(resetPosition());
        }
    }

    IEnumerator resetPosition()
    {
        while (Vector3.Distance(transform.position, new Vector3(0, 5, 0)) > 5)
        {
            transform.position = new Vector3(0, 5, 0);
            yield return null;
        }
    }
}
