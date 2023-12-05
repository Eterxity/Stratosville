using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : NetworkBehaviour
{

    public float _speed;
    private Vector2 moveDir, mouseLook;
    private Vector3 rotationTarget;

    public void onMove(InputAction.CallbackContext context)
    {

        moveDir = context.ReadValue<Vector2>();

    }

    public void onMouseLook(InputAction.CallbackContext context)
    {

        mouseLook = context.ReadValue<Vector2>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!IsOwner) return;
        
        
        //RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mouseLook);

        //if (Physics.Raycast(ray, out hit))
        //{
        //    rotationTarget = hit.point;
        //}
        

        movePlayer();
    }

    public void movePlayer()
    {

        Vector3 movement = new Vector3(moveDir.x, 0f, moveDir.y);

        if (moveDir.x == 0f && moveDir.y == 0f) return;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);

        transform.Translate(movement * _speed * Time.deltaTime, Space.World);

    }

    public void movePlayerWithAim()
    {

        var lookPos = rotationTarget - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);

        Vector3 aimDir = new Vector3(rotationTarget.x, 0f, rotation.z);

        if (aimDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        Vector3 movement = new Vector3(moveDir.x, 0f, moveDir.y);
        transform.Translate(movement * _speed * Time.deltaTime, Space.World);
    }
}
