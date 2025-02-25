using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoControler : MonoBehaviour
{
    public int life;
    public int Damage;
    public Transform player;
    private NavMeshAgent agent;
    private bool enemigoDetectado = false;
    public Transform point;

    //Ray
    public float rayDistance = 10f;
    public GameObject vista;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        point = LevelManager.instance.getNewPoint().transform;
    }

    public void DamagenEmigo(int _damage)
    {
        life -= _damage;
        if (life <= 0)
        {
            LevelManager.instance.enemigoDestruido();
            Destroy(gameObject);
        }
    }
    private void Update()
    {

        RaycastHit hit;
        if(Physics.Raycast(vista.transform.position,transform.forward,out hit, rayDistance))
        {
            //Debug.Log("Tag detectado: " + hit.collider.tag);
            if(hit.collider.tag == "Player")
            {
                LevelManager.instance.endGame = true;
                LevelManager.instance.loseGame();
                enemigoDetectado=true;
            }
        }
        Debug.DrawRay(vista.transform.position, transform.forward * rayDistance, Color.red);


        if(player != null && enemigoDetectado)
        {
            agent.SetDestination(player.position);
        }
        else if (!enemigoDetectado)
        {
            agent.SetDestination(point.position);
        }
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                if (enemigoDetectado)
                {
                    Debug.Log("GameOver");
                }
                else
                {
                    point = LevelManager.instance.getNewPoint().transform;
                }
            }
        }
    }
}
