using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour {

    private Collider2D myCollider;
    private object myRigidbody;

    [SerializeField]
    private float resistance = 1F;
    [SerializeField]
    private float lateralVelocity;

    private float spinTime = 1F;
    private Vector3 objective;
    private float x;
    private Vector3 save_positionX;
    private bool move = false;

    // Use this for initialization
    protected void Start()
    {
        myCollider = GetComponent<Collider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        x = Random.Range(-5.45f,5.45f);
        objective = new Vector3(x, 0, 0);
        save_positionX = new Vector3(transform.position.x, 0);
    }

    protected void Update()
    {

        if (Vector3.Distance(transform.position, objective) > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, objective, lateralVelocity * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, objective) < 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, save_positionX, lateralVelocity * Time.deltaTime);
        }
    }

    protected bool InsideCamera(bool positive)
    {
        float direction = positive ? 1F : -1F;
        Vector3 cameraPoint = Camera.main.WorldToViewportPoint(
            new Vector3(
                myCollider.bounds.center.x + myCollider.bounds.extents.x * direction,
                0F,
                0F));
        return cameraPoint.x >= 0F && cameraPoint.x <= 1F;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            //TODO: Make this to reduce damage using Bullet.damage attribute
            resistance -= 1;

            if (resistance == 0)
            {
                OnHazardDestroyed();
            }
        }

        if (collision.gameObject.GetComponent<Shelter>() != null)
        {
            collision.gameObject.GetComponent<Shelter>().Damage(1);
        }
    }

    protected void OnHazardDestroyed()
    {
        //TODO: GameObject should spin for 'spinTime' secs. then disappear
        Destroy(gameObject);
    }
}
