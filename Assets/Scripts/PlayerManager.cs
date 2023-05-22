using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private string playerPrefab = "Tank";

    [SerializeField]
    private Transform bulletSpawn;
    [SerializeField]
    private int bulletSpeed = 100;
    //[SerializeField]
    //private float damping = 1;

    void Start()
    {
        var player  = Resources.Load(playerPrefab);
        GameObject.Instantiate(player,transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Move("move", 1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move("move", -1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Move("rotate", -1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move("rotate", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        } 
    }

    public void Move(string axis, int unit)
    {

        if (axis == "move")
        {
            //Debug.Log(transform.forward);
            transform.position += transform.forward * unit;
            //transform.localPosition = new Vector3(transform.position.x , transform.position.y, transform.position.z + unit);
        }

        if (axis == "rotate")
        {
            //transform.localPosition = new Vector3(transform.position.x + unit, transform.position.y, transform.position.z);
            var desiredRot = Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y + unit * 90,transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot,1);
            
            //transform.rotation = Quaternion.Euler(0, 90 * unit, 0);
        }

    }

    public void ShootBullet()
    {
        Debug.Log("shoot!");
        GameObject bullet = (GameObject)Instantiate(Resources.Load("Bullet"),bulletSpawn.position,bulletSpawn.rotation);
        //go.GetComponent<Rigidbody>().velocity = go.transform.forward * bulletSpeed

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
    }

    
}
