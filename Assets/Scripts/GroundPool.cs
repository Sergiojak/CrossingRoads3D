using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPool : MonoBehaviour
{
    //num de suelos que vamos a crear
    [SerializeField]
    int maxElements = 10;
    //el elemento que queremos crear
    [SerializeField]
    GameObject groundPrefab;

    //lugar para guardarlos (la pool)
    private Stack<GameObject> pool;


    //Static es que solo se puede poner en 1 sitio, si pongo el script en 2 sitios diferentes da error
    public static GroundPool instance;

    private void Awake()
    {
        //Singletone para usarlo desde el groundMovementInput;
        if (GroundPool.instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        //que al iniciar el juego se haga el Setup de la Pool
        SetupPool();
    }

    //Creando objetos dentro de la pool
    void SetupPool()
    {
        //esta es la pila, siempre cogemos el último que hemos puesto en el stack, el de más arriba de la pila
        pool = new Stack<GameObject>();
        GameObject groundCreated = null;


        //Crear un for hasta el max de elementos
        for (int i = 0; i < maxElements; i++)
        {
            //En balaCreada instanciamos nuestro prefab (instantiate es meter dentro del juego)
            groundCreated = Instantiate(groundPrefab);
            //balaCreada lo desactivo
            groundCreated.SetActive(false);
            //balaCreada lo meto en la pool, con pool.Push(gameObject)
            pool.Push(groundCreated);
        }

    }

    //ya tenemos la Pool hecha, ahora creamos función de obtener el objeto
    public GameObject ObtenerObjeto()
    {
        GameObject ground = null;

        //si no quedan elementos en mi pool...
        if (pool.Count == 0)
        {
            //pues creas uno
            ground = Instantiate(groundPrefab);
        }
        else
        {
            //pop para cogerla y la activamos
            ground = pool.Pop();
            ground.SetActive(true);
        }
        return ground;

    }

    //Creamos función de devolver el objeto, una vez usado volverá a la pool
    public void DevolverObjeto(GameObject groundReturned)
    {
        //Vuelve a guardar el objeto en la pool, con pool.Push(GameObject)
        pool.Push(groundReturned);
        //Se desactiva de la escena
        groundReturned.SetActive(false);
    }
    //Push meter
    //Pop sacar
}