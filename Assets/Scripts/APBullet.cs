using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APBullet : MonoBehaviour {

    [SerializeField]
    private int damage = 1;

    private Collider2D myCollider;
    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float force = 10F;

    [SerializeField]
    private float autoDestroyTime = 5F;

    // Use this for initialization
    private void Start()
    {
        myCollider = GetComponent<Collider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();

        myRigidbody.AddForce(transform.up * force, ForceMode2D.Impulse);

        Invoke("AutoDestroy", autoDestroyTime);
    }

    private void AutoDestroy()
    {
        Destroy(gameObject);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<Hazard>() || !collision.gameObject.GetComponent<Debris>() || !collision.gameObject.GetComponent<Invader>())
        {
            Destroy(gameObject);
        }
    }
}
