using UnityEngine;
public class Bullet : MonoBehaviour
{
     Vector3 force = new Vector3(0f, 3f,35f);
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
        }

        if (collision.gameObject.CompareTag("Body"))
        {
            GetComponent<BoxCollider>().enabled = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            collision.transform.GetComponent<Rigidbody>().AddForce(Quaternion.LookRotation(ray.direction) * force, ForceMode.VelocityChange);
            // collision.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);
            collision.transform.root.GetChild(2).GetComponent<Rigidbody>().AddForce(transform.forward * 15, ForceMode.Impulse);
            GetComponent<Rigidbody>().isKinematic = true;
            collision.transform.root.GetComponent<Enemy>().health--;
            transform.root.parent = collision.transform;
        }

        if (collision.gameObject.CompareTag("Head"))
        {
            GetComponent<BoxCollider>().enabled = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            collision.transform.GetComponent<Rigidbody>().AddForce(Quaternion.LookRotation(ray.direction) * force * 3, ForceMode.VelocityChange);
            collision.transform.root.GetChild(2).GetComponent<Rigidbody>().AddForce(transform.forward * 15, ForceMode.Impulse);
            GetComponent<Rigidbody>().isKinematic = true;
            collision.transform.root.GetComponent<Enemy>().health=0;
          //  collision.transform.root.GetChild(1).GetComponent<PuppetMaster>().state = PuppetMaster.State.Dead;
            transform.root.parent=collision.transform;
        }
    }
}
