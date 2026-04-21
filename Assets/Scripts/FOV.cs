using System;
using System.Linq;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float viewRadius;

    public LayerMask obstacleMask;
    public LayerMask groundMask;

    Collider2D[] objectsInRange = new Collider2D[1];

    void FindObjectsInRadius()
    {
        Collider2D[] prevArr = objectsInRange;
        objectsInRange = Physics2D.OverlapCircleAll(transform.position, viewRadius, obstacleMask);

        Debug.Log(objectsInRange.Length);

        foreach(Collider2D obj in prevArr)
        {
            if (!objectsInRange.Contains(obj))
            {
                obj.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        foreach(Collider2D obj in objectsInRange)
        {
            float dist = Vector2.Distance(transform.position, obj.transform.position);

            Vector2 dir = (obj.transform.position - transform.position).normalized;

            Debug.DrawRay(transform.position, dir, Color.red);

            if (!Physics2D.Raycast(transform.position, dir, dist, groundMask))
            {
                obj.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                obj.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectsInRadius();
    }
}
