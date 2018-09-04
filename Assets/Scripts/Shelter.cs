using UnityEngine;

public class Shelter : MonoBehaviour
{
    [SerializeField]
    private int maxResistance = 5;
    [SerializeField]
    private float regenTime; // En segundos

    private float cont = 0;
    private bool hit = false;

    public int MaxResistance
    {
        get
        {
            return maxResistance;
        }
        protected set
        {
            maxResistance = value;
        }
    }

    private void Update()
    {
        if(hit && maxResistance < 5)
        {
            cont += Time.deltaTime;

            if (cont >= regenTime)
            {
                MaxResistance += 1;
                hit = false;
                cont = 0;
            }
        }

        if(MaxResistance <= 0)
        {
            gameObject.SetActive(false);
            Debug.Log(gameObject.name + " ha sido destruido");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Hazard>() != null)
        {
            hit = true;
        }
    }

    public void Damage(int damage)
    {
        MaxResistance -= damage;
    }
}