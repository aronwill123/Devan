using UnityEngine;
using RootMotion.Dynamics;
public class Enemy : MonoBehaviour
{
    public int health = 4;
    Material mat;
    public float speed = 0;
    void Start()
    {
        mat = transform.GetChild(2).GetChild(0).GetComponent<SkinnedMeshRenderer>().materials[0];
    }

    void Update()
    {
        if (health <= 0)
        {
            Killme();
        }
        EnemyMovement();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.transform.parent = transform;
            health--;
        }
    }

    void Killme()
    {
        mat.color = Color.black;
        transform.GetChild(1).GetComponent<PuppetMaster>().state = PuppetMaster.State.Dead;
        Time.timeScale = 0.4f;
        if (EnemyManager.instance.enemyList.Contains(this.transform))
        {
            if (EnemyManager.instance.enemyList.Contains(PlayerController.Instance.target))
                PlayerController.Instance.target = null;
            EnemyManager.instance.enemyList.Remove(this.transform);
        }
        Invoke("Rescale", 0.5f);
    }

    void Rescale()
    {
        print("RescaleMethod");
        Time.timeScale = 1;
        enabled = false;
    }

    void EnemyMovement()
    {
        // if (!isgothit)
        {
            transform.LookAt(new Vector3(PlayerController.Instance.transform.position.x,
                transform.position.y, PlayerController.Instance.transform.position.z));

            transform.position = Vector3.MoveTowards(transform.position,
                PlayerController.Instance.transform.position, speed * Time.deltaTime);
        }

    }

    public void speedDecrease()
    {
        speed = 0;
    } 
    public void speedIncrease()
    {
        speed = 2;
    }
}
