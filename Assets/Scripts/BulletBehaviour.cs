using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    private void Awake()
    {
        Destroy(gameObject, 3);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BulletBehaviour>())
        {
            Destroy(gameObject,1);
            Destroy(collision.gameObject,1);
            return;
        }
        BoxType type = collision.gameObject.GetComponent<Block>().boxType;
        if(type == BoxType.Metal)
        {
            Destroy(gameObject);
        }
        if ((type == BoxType.Brick || type == BoxType.Enemy) && gameObject.name == "Bullet(Clone)")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.instance.ExplodeAtPos(collision.transform.position);
        }
        if ((type == BoxType.Brick || type == BoxType.Player) && gameObject.name == "EnemyBullet(Clone)")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.instance.ExplodeAtPos(collision.transform.position);
        }
    }
}
