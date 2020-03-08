using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //how fast are we talkin'?
    [SerializeField] private float speed;
    [SerializeField] private GameObject target;

    private Transform player;
    private Vector3 pinpoint;

    void Start()
    {
        player = target.transform;
        //pin point the player
        pinpoint = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    void Update()
    {
        //move towards player location NOT the player itself
        transform.position = Vector3.MoveTowards(transform.position, pinpoint, speed * Time.deltaTime);
        //Destroy bullet if it reaches the player position
        if(transform.position.x == pinpoint.x && transform.position.y == pinpoint.y && transform.position.z == pinpoint.z)
        {
            DestroyBullet();
        }
    }

    //destroy bullet if it collides with the player
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
