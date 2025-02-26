using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoControler : MonoBehaviour
{
    public int life;//vida enemigo
    public int Damage;//ataque del enemigo
    public Transform player;//posicion del target a perseguir
    private NavMeshAgent agent;//referencia al agent navmesh
    private bool enemigoDetectado = false;//sabemos si ya detecto al enemigo
    public Transform point;//punto al cual patrullar

    //Ray
    public float rayDistance = 10f;//distancia en la que detecta al player
    public GameObject vista;//punto de salida de los ojos
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        point = LevelManager.instance.getNewPoint().transform;//pedimos al levelmanager un putno aleatorio para ir
    }

    public void DamagenEmigo(int _damage)//cuando el enemigo es lastimado
    {
        life -= _damage;
        if (life <= 0)//si se queda sin vidas muere
        {
            LevelManager.instance.enemigoDestruido();
            Destroy(gameObject);
        }
    }
    private void Update()
    {

        RaycastHit hit;
        if(Physics.Raycast(vista.transform.position,transform.forward,out hit, rayDistance))//chequjemos si ve al player para terminar el juego
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

        //determina que tarjet perseguir
        if(player != null && enemigoDetectado)
        {
            agent.SetDestination(player.position);
        }
        else if (!enemigoDetectado)
        {
            agent.SetDestination(point.position);
        }
        //calcula si ya llego al punto a perseguir
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
                    point = LevelManager.instance.getNewPoint().transform;//si ya llego al punto pide uno nuevo
                }
            }
        }
    }
}
