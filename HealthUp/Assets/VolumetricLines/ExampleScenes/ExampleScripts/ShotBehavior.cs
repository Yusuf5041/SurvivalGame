using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour
{

    private int damage;
    private ParticleSystem particleSys;

    // Use this for initialization
    void Start()
    {
        damage = 35;
        particleSys = GetComponent<ParticleSystem>();
        Destroy(gameObject, 1.3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 40f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Giant")
        {
            Debug.Log("enemy hit");
            other.transform.parent.SendMessage("ApplyDamage", damage);
            other.transform.parent.SendMessage("ChasePlayer");
            particleSys.Play();
            MeshRenderer mr = GetComponent<MeshRenderer>();
            mr.enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}
