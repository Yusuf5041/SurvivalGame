using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public enum AiMode { Patrol, Pursue, Wander };
    public AudioClip[] sounds;

    private NavMeshAgent agent;
    private float timer, wanderTimer, wanderRadius;

    private float distance, lookAtDistance, attackDistance, fov, targetAngle;
    private float damage;

    private Vector3 lastPos;
    private bool posCheck = false, chasing = false, isDying = false;

    private Animator animator;

    private Transform player;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        lookAtDistance = 25;
        attackDistance = 6;
        fov = 30;

        lastPos = transform.position;

        animator = GetComponent<Animator>();
        agent = GetComponentInChildren<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        wanderTimer = 6f;
        wanderRadius = 40f;
        timer = wanderTimer;

        if (gameObject.tag == "Enemy")
            damage = 5;
        else
            damage = 75;

        StartCoroutine("WalkingSounds");
        StartCoroutine("Movement");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDying)
        {
            if (transform.position == lastPos && !posCheck && !chasing)
            {
                PlayIdle();
                posCheck = true;
            }

        }


    }

    IEnumerator Movement()
    {
        for (; ; )
        {
            if (DecideMovement() == AiMode.Pursue)
            {
                Pursue();
                if (distance < attackDistance)
                {
                    Attack();
                }
            }
            else
            {
                Wander();
            }
            yield return new WaitForSeconds(5);
        }

    }

    IEnumerator WalkingSounds()
    {
        for (; ; )
        {
            audioSource.PlayOneShot(sounds[0]);
            yield return new WaitForSeconds(4.5f);
        }

    }

    private AiMode DecideMovement()
    {
        RaycastHit hit;
        distance = Vector3.Distance(player.position, transform.position);
        if (chasing) return AiMode.Pursue;
        if (distance < lookAtDistance)
        {
            targetAngle = Vector3.Angle(transform.forward, player.position - transform.position);
            //Debug.Log("target angle: " + targetAngle);
            if (targetAngle < fov)
            {
                return AiMode.Pursue;
                //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 30, -1))
                //{
                //    if (hit.collider.gameObject.tag == "Player")
                //    {
                //        return AiMode.Pursue;
                //    }
                //}
            }
            //chasing = false;
        }

        return AiMode.Wander;
    }

    private void Wander()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
        timer = 0;
        PlayWalk();
        posCheck = false;
    }

    private void Pursue()
    {
        agent.SetDestination(player.position);
        if (!chasing)
        {
            chasing = true;
            PlayRun();
        }

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void PlayIdle()
    {
        if (gameObject.tag == "Enemy")
        {
            animator.Play("Cooldown");
        }
        else
        {
            animator.Play("GrenadierIdle");
        }

    }

    private void PlayWalk()
    {
        if (gameObject.tag == "Enemy")
            animator.Play("ChomperWalkForward");
        else
            animator.Play("GrenadierWalk");
    }

    private void PlayRun()
    {
        if (gameObject.tag == "Enemy")
            animator.Play("ChomperRunForward");
        else
            animator.Play("GrenadierWalkFast");
    }

    public void PlayDeath()
    {
        StopCoroutine("WalkingSounds");
        isDying = true;
        audioSource.PlayOneShot(sounds[2]);
        if (gameObject.tag == "Enemy")
            animator.Play("ChomperHit3");
        else
            animator.Play("GrenadierDeath");
    }

    private void PlayAttack()
    {
        if (gameObject.tag == "Enemy")
            animator.Play("ChomperAttack");
        else
            animator.Play("GrenadierMeleeAttack");
    }

    private void Attack()
    {
        audioSource.PlayOneShot(sounds[1]);
        PlayAttack();
        player.SendMessage("ApplyDamage", damage);
    }

    private void ChasePlayer()
    {
        Pursue();
    }

}
