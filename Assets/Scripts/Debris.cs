using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour {

    private Collider2D myCollider;
    private object myRigidbody;

    [SerializeField]
    private float resistance = 1F;
     private float turn_velocity;

   
    // Use this for initialization
     void Start ()
    {
        myCollider = GetComponent<Collider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        turn_velocity = Random.Range(50,250);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * turn_velocity));
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

