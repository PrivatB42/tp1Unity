using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    private Vector3 moveDir;
    private Animator anim;
    private bool isWalking;
    private bool collisionCam1;
    private bool collisionCam2;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // R�cup�re les entr�es horizontales et verticales
        float moveX;
        float moveZ;

        if (collisionCam1 == true)
        {
            moveX = Input.GetAxis("Vertical") * moveSpeed;
            moveZ = -Input.GetAxis("Horizontal") * moveSpeed;
        }
        else if (collisionCam2 == true)
        {
            moveX = Input.GetAxis("Horizontal") * moveSpeed;
            moveZ = Input.GetAxis("Vertical") * moveSpeed;
        }
        else
        {
            moveX = -Input.GetAxis("Horizontal") * moveSpeed;
            moveZ = -Input.GetAxis("Vertical") * moveSpeed;
        }

        // Combine les entr�es pour obtenir la direction du d�placement
        moveDir = new Vector3(moveX, moveDir.y, moveZ);

        // V�rifie si le joueur est au sol
        if (characterController.isGrounded)
        {
            // R�initialise le mouvement vertical si au sol
            moveDir.y = -1f;

            // V�rifie si la touche de saut est enfonc�e
            if (Input.GetButtonDown("Jump"))
            {
                // Applique la force de saut
                moveDir.y = jumpForce;
            }
        }
        else
        {
            // Applique la gravit� lorsque le joueur n'est pas au sol
            moveDir.y -= gravity * Time.deltaTime;
        }

        // V�rifie si le joueur se d�place
        if (moveX != 0 || moveZ != 0)
        {
            isWalking = true; // Le joueur marche
            // Fait pivoter le personnage dans la direction du d�placement
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveX, 0, moveZ)), 0.15f);
        }
        else
        {
            isWalking = false; // Le joueur s'arr�te de marcher
        }

        // D�clenche l'animation de marche
        anim.SetBool("IsWalking", isWalking);

        // Applique le d�placement au personnage
        characterController.Move(moveDir * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cam1") { 
            collisionCam1 = true;
        }

        if (other.gameObject.tag == "cam2")
        {
            collisionCam2 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "cam1")
        {
            collisionCam1 = false;
        }

        if (other.gameObject.tag == "cam2")
        {
            collisionCam2 = false;
        }
    }

}