using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 20.0f;
    private Vector3 direction;
    private CharacterController characterController;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        //if (Input.GetKeyDown(KeyCode.Space))
            direction.y = Input.GetAxis("Jump");

        Vector3 moveDirection = new Vector3(direction.x, 0, direction.z);
        characterController.SimpleMove(moveDirection * moveSpeed);
    }
}
