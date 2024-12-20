using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;

    Animator anim;
    Transform cam;
    public float speed = 10f;
    float gravity = -10;
    public float jumpvalue = 10;
    float verticalVelocity = 10;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(Horizontal, 0, Vertical);
        bool isSprint = Input.GetKey(KeyCode.LeftShift);
        float sprint = isSprint ? 1.7f : 1;
        anim.SetFloat("speed", Mathf.Clamp(moveDirection.magnitude, 0, .5f) + (isSprint ? .5f : 0));
        if (controller.isGrounded)
        {
            if (Input.GetAxis("Jump") > 0)
                verticalVelocity = jumpvalue;
        }
        else
            verticalVelocity += gravity * Time.deltaTime;

        if (moveDirection.magnitude > .1)
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
        moveDirection = cam.TransformDirection(moveDirection);
        moveDirection.y = verticalVelocity;
        moveDirection = new Vector3(moveDirection.x * speed * sprint, verticalVelocity, moveDirection.z * speed * sprint);
        controller.Move(moveDirection * Time.deltaTime);

    }
    public void DoAttack()
    {
        transform.Find("Collider").GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(HideCollider());
    }
    IEnumerator HideCollider()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Find("Collider").GetComponent<BoxCollider>().enabled = false;
    }
}
