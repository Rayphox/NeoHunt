using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPesonController : MonoBehaviour
{
    public float moveSpeed = 5f;//Velocidad de movimiento
    private float mouseSensitivityInterno = 2f;//sensivilidad del mouse
    public Transform cameraTransform;//referencia al transform de la camara
    public Rigidbody rb;//referencia al Rigidbody 
    public Animator anim;//referencia al animator
    public GameObject mesh;//referencia al mesh del robot

    private float rotationX = 0f;//variable para manejar la rotacion de la camara
    private float shiftSpeed;//variable para guardar la velocidad al apretar shift
    private float realSpeed;//la velocidad que actualmente usamos

    //Disparo
    public GameObject balaPrefad;
    public Transform[] puntoDisparo;
    private int indexDisparo = 0;
    public float radioDeteccion = 10f;
    public float velocidadBala = 10f;
    public float tiempoEntreDisparos = 1f;
    public LayerMask capaEnemigo;

    private float tiempoUltimoDisparo = 0f;
    private bool permitidoDisparar = true;
    void Start()
    {
        realSpeed = moveSpeed;//asignamos por defecto la speed normal a la speed real
        shiftSpeed = moveSpeed * 1.5f;//definimos la speed al apretar shift
        mouseSensitivityInterno = GameManager.Instance.mouseSensitivity;//seteamos la sensivilidad del mouse
        if(rb == null)
            rb = GetComponent<Rigidbody>();//buscamos el componente Rigidbody y lo asignamos a nuestra variable
        rb.freezeRotation = true;//impedimos que pueda rotar las fisicas para evitar que se caiga el personaje 
        Cursor.lockState = CursorLockMode.Locked;//bloqueamos el mouse
        Cursor.visible = false;//quitamos la vision del mouse
    }

    
    void Update()
    {
        Move();//funcion de movimiento
        RotateWithMouse();//funcion de camara
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Tecla disparo");
            Disparo();
        }
    }

    private void Move()
    {
        

        float horizontal = Input.GetAxis("Horizontal");//detectamos inputs horizontales
        float vertical = Input.GetAxis("Vertical");//detectamos inputs verticales

        if(vertical != 0)//si vertical no es 0 osea que se esta apretando si o si alguna de las dos teclas
        {
            anim.SetBool("run", true);//activamos la animcacion de movernos 
            if(vertical > 0)//si vertical es mayor a 1 significa que se inclina para adelante si no para atras
                mesh.transform.localRotation = Quaternion.Euler(-70, 0f, 0f);
            else
                mesh.transform.localRotation = Quaternion.Euler(-110, 0f, 0f);
        }
        else//si esta en 0 significa que no se esta moviendo
        {
            anim.SetBool("run", false);//apagamos animacion
            mesh.transform.localRotation = Quaternion.Euler(-90, 0f, 0f);
        }



        if (Input.GetKeyDown(KeyCode.LeftShift))//seteamos la velocidad segun si se aprieta o levanta la tecla shift
        {
            realSpeed = shiftSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            realSpeed = moveSpeed;
        }


        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;//creamos un Vector3 que suma en funcion de moverse al frente y a los laterales
        moveDirection.Normalize();//normalizamos al vector3

        Vector3 velocity = new Vector3(moveDirection.x * realSpeed, rb.velocity.y, moveDirection.z * realSpeed);//creamos el Vector3 de velocidad en los ejes "X" y "Z", el eje Y queda igual ya que no tenemos salto
        rb.velocity = velocity;//aplicamos al Rigidbody una velocidad usando la variable Vector3
    }
    private void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityInterno;//seteamos las cordenadas mouse X teniendo encuenta la sensivilidad
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityInterno;

        transform.Rotate(Vector3.up * mouseX);//rotamos el robot segun rotemos el mouse para los lados

        rotationX -= mouseY;//a nuestra variable de rotacion le restamos la rotacion en mouse Y
        rotationX = Mathf.Clamp(rotationX, -45f, 45f);//ponemos maximos y minimos de rotacion
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);//modificamos la rotacion de la camara en X
    }

    //Disparo
    public void Disparo()
    {
        Collider[] enemigos = Physics.OverlapSphere(transform.position, radioDeteccion, capaEnemigo);

        if(enemigos.Length > 0)
        {
            Transform enemigoMasCercano = ObtenerRnemigoMasCercano(enemigos);

            if(enemigoMasCercano != null && permitidoDisparar)
            {
                permitidoDisparar = false;
                StartCoroutine(nuevoDisparo());
                Disparar(enemigoMasCercano);
            }
        }
    }
    public void Disparar(Transform enemigo)
    {

        GameObject bala = Instantiate(balaPrefad, puntoDisparo[indexDisparo].position, Quaternion.identity);
        indexDisparo++;
        if(indexDisparo == puntoDisparo.Length)
            indexDisparo = 0;

        bala.transform.LookAt(enemigo.position);
        bala.AddComponent<MovimientoBala>();
        bala.GetComponent<MovimientoBala>().velocidad = velocidadBala;
    }
    public Transform ObtenerRnemigoMasCercano(Collider[] enemigos)
    {
        Transform enemigoMasCercano = null;
        float distanciaMinima = Mathf.Infinity;

        foreach(Collider enemigo in enemigos)
        {
            float distancia = Vector3.Distance(transform.position, enemigo.transform.position);
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                enemigoMasCercano = enemigo.transform;
            }
        }
        return enemigoMasCercano;
    }
    IEnumerator nuevoDisparo()
    {
        yield return new WaitForSeconds(tiempoEntreDisparos);
        permitidoDisparar = true;
    }
}
