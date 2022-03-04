using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    SplineFollower sf;
    public Transform target;
    public GameObject Bullet;
    public Transform Bow;
    public Transform SpawnPoint;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        sf = GetComponent<SplineFollower>();
    }
    void Update()
    {
        if(target!=null)
        {
            AimingTowardsTarget();
        }
        Attack();

        if (Input.GetMouseButtonDown(1))
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);
    }

    public bool targetLocked;
    void AimingTowardsTarget()
    {
        if (!targetLocked)
        {
            sf.followSpeed = 0;
            sf.motion.applyRotationY = false;
            Vector3 targetDirection = (target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            if (Quaternion.Angle(transform.rotation, targetRotation) > 1f)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.005f);
        }
    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, 10)) - Bow.localPosition;
            Bow.LookAt(difference);
            GameObject go = Instantiate(Bullet, Bow.GetChild(0).position, Quaternion.Euler(Bow.eulerAngles));
            //  Debug.Break();
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 40, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyZone"))
        {
            other.GetComponent<EnemyManager>().enabled = true;  
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            print("GameOver");
        }
    }
}