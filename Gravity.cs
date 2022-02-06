using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    [SerializeField] private GameObject player;

    private bool isGrounded;
    public float dist;
    public LayerMask mask;

    private Rigidbody playerRB;


    private void Update()
    {
        Check();
    }

    private void Check()
    {
        isGrounded = Physics.CheckSphere(transform.position, dist, mask);

        if (!isGrounded)
        {

        }
    }
}
