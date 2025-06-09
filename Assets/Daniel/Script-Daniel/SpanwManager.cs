using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwManager : MonoBehaviour
{
    /**/
    [SerializeField] List<GameObject> poolEnemy = new List<GameObject>();
    [SerializeField] GameObject enemy;
    [SerializeField] int spawnRate;
    [SerializeField] GameObject player;
    /**/
    private int poolSize = 10;
    public int wave = 1;
    public int enemiesPerWave = 10;
    public bool isSpawningWave = false;
    public int enemiesSpawnedInWave = 0;
    private int blockSize = 5;
    private SoulCollectorA soulCollectorA;
    private AlmaSpawnPoint currentAlmaSpawnPoint; // Referencia al AlmaSpawnPoint actual para spawning
    private AlmaComprobacion almaComprobacion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soulCollectorA = FindFirstObjectByType<SoulCollectorA>();
        almaComprobacion = FindAnyObjectByType<AlmaComprobacion>();
        //puntos de spawn

        AddToPool(poolSize);
        StartCoroutine(SpawnRutine());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update - alma: " + soulCollectorA.alma + ", isSpawningWave: " + isSpawningWave + ", EnemysEnable(): " + EnemysEnable() + ", enemiesSpawnedInWave: " + enemiesSpawnedInWave + ", enemiesPerWave: " + enemiesPerWave);

        // Si un alma ha sido activada y no estamos spawneando una oleada, inicia la oleada
        if (soulCollectorA.alma && !isSpawningWave)
        {
            isSpawningWave = true;
            enemiesSpawnedInWave = 0;
            StartCoroutine(SpawnWave()); // Inicia el primer bloque
        }
        // Si estamos spawneando una oleada y el bloque inicial ha sido derrotado
        // y aun faltan enemigos en la oleada, lanza el siguiente bloque.
        else if (isSpawningWave && EnemysEnable() == 0 && enemiesSpawnedInWave < enemiesPerWave)
        {
            StartCoroutine(SpawnWave()); // Lanza el siguiente bloque
        }
        // Si la oleada actual ha terminado (todos los enemigos spawnearon y murieron)
        else if (isSpawningWave && enemiesSpawnedInWave >= enemiesPerWave && EnemysEnable() == 0)
        {
            //Debug.Log("Final de oleada detectado. enemiesSpawnedInWave: " + enemiesSpawnedInWave + ", enemiesPerWave: " + enemiesPerWave + ", EnemysEnable(): " + EnemysEnable());
            wave++;
            almaComprobacion.EnableAlma();
            enemiesPerWave *= 2;
            isSpawningWave = false;
            soulCollectorA.alma = false; // Desactiva el alma al final de la oleada completa

        }
    }

    public void AddToPool(int maxPrefab)
    {
        //creamos un for para que se agregen enemigos al pool deacuerdo al limite de objetos
        for (int i = 0; i < maxPrefab; i++)
        {
            GameObject prefab;//intanciamos un GameObject para instanciar los prefabs
            prefab = Instantiate(enemy, SpawnPoint(), Quaternion.identity);//se instancia el prefab en el punto de spawn
            prefab.SetActive(false);//se desactiva para usarce cuando sea necesario
            poolEnemy.Add(prefab);// Lo agrega a la lista de la piscina
        }
    }

    //metodo para el Spawn de los enemigos
    public GameObject SpawnEnemy()
    {
        foreach (GameObject Enemy in poolEnemy)
        {
            if (!Enemy.activeInHierarchy)
            {
                Enemy.transform.position = SpawnPoint();
                Enemy.GetComponent<Enemy>().SetTarget(player);
                Enemy.SetActive(true);
                return Enemy;
            }
        }
        // Si no hay enemigos disponibles, crear uno nuevo, agregarlo a la pool y devolverlo
        GameObject newEnemy = Instantiate(enemy, SpawnPoint(), Quaternion.identity);
        newEnemy.SetActive(true);
        poolEnemy.Add(newEnemy);
        return newEnemy;
    }

    /*Metodo para la posicion del spanw de los enemigos*/
    public void SetCurrentAlmaSpawnPoint(AlmaSpawnPoint almaSpawnPoint)
    {
        currentAlmaSpawnPoint = almaSpawnPoint;
    }
    public Vector3 SpawnPoint()
    {
        if (currentAlmaSpawnPoint != null)
        {
            return currentAlmaSpawnPoint.GetSpawnPoint();
        }
        else
        {
            //Debug.LogWarning("No se ha consumido un alma o no tiene AlmaSpawnPoint asignado. Spawneando en la posición del SpawnManager.");
            return transform.position;//si no hay alma consumida
        }
    }

    /*metodo que spanwnea los enemigos*/
    public IEnumerator SpawnRutine()
    {
        while (true)
        {
            yield return null; // Espera el siguiente frame
        }
    }
    /*Metodo para el spanw de las olas*/
    public IEnumerator SpawnWave()
    {
        //enemigos que aun faltan por spanw
        int enemigosRestantes = enemiesPerWave - enemiesSpawnedInWave;
        int cantidadEnEsteBloque = Mathf.Min(blockSize, enemigosRestantes);//enemigos que aparecen primero por ronda
                                                                           //recorremos la cantidad de enemigos que aparecen primero simpre que sean mayores que 0
        for (int i = 0; i < cantidadEnEsteBloque; i++)
        {
            //si alma es verdadera spanw enemigos
            if (soulCollectorA.alma)
            {
                SpawnEnemy();
                enemiesSpawnedInWave++;//me cuenta los enemigos que an aparecido
                //Debug.Log("SpawnWave - enemiesSpawnedInWave: " + enemiesSpawnedInWave);
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }
    //metodos para contar los enemigos activos
    public int EnemysEnable()
    {
        int count = 0;
        foreach (GameObject prefab in poolEnemy)
        {
            if (prefab.activeInHierarchy)
            {
                count++;
            }
        }
        //Debug.Log("Enemigos activos:" + count);
        return count;

    }
    //metodo para manegar la muerte de los enemigos 
    public void EnemyDied(GameObject enemyDie)
    {
        //Debug.Log("enemigo:" + enemyDie + " muerto");
        enemyDie.SetActive(false);
    }
}
