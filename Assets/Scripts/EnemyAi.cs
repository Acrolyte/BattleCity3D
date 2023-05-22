using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;

    [SerializeField]
    float lookRange, shootRange;

    [SerializeField]
    Transform bulletSpawn;

    [SerializeField]
    float bulletSpeed, coolDownTime;

    [SerializeField]
    float timePassed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (GameObject.Find("Player") == null)
        {
            GameManager.instance.LoadScene("GameOver");
        }
        else
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < lookRange)
            {
                if (distance < shootRange && timePassed >= coolDownTime)
                {
                    timePassed = 0;
                    Shoot();
                }
                agent.SetDestination(player.transform.position);
            }
        }
       
    }

    void Shoot()
    {
        GameObject bullet = (GameObject)Instantiate(Resources.Load("EnemyBullet"), bulletSpawn.position, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRange);
        //Gizmos.DrawSphere(transform.position, lookRange);
    }
}
