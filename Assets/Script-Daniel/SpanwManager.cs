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
    /*
    [SerializeField] GameObject spawnPoint1;
    [SerializeField] GameObject spawnPoint2;
    [SerializeField] GameObject spawnPoint3;*/
    /**/
    private int poolSize = 10;
    public int wave = 1;
    private int enemiesPerWave = 10;
    private bool isSpawningWave = false;
    private int enemiesSpawnedInWave = 0;
    public bool alma = false;
    private int blockSize=5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddToPool(poolSize);
        StartCoroutine(SpawnRutine());
    }

    // Update is called once per frame
    void Update()
    {
         // Si no hay enemigos vivos, pero aún faltan por aparecer, lanzamos otro bloque.
        if (alma && isSpawningWave && EnemysEnable() == 0 && enemiesSpawnedInWave < enemiesPerWave)
        {
            StartCoroutine(SpawnWave());
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
    public Vector3 SpawnPoint()
    {
        Vector3 pos = transform.position;
        // Genera un desplazamiento aleatorio en un rango alrededor del punto de spawn
        float offsetX = Random.Range(-10, 10);
        float offsetZ = Random.Range(-10, 10);
        pos += new Vector3(offsetX, 0f, offsetZ);
        return pos;
    }

    /*metodo que spanwnea los enemigos*/
    public IEnumerator SpawnRutine()
    {
        while (true)
        {
            if (!isSpawningWave && alma)
            {
                isSpawningWave = true;
                enemiesSpawnedInWave = 0;//enemigos que estan en la escena

                yield return StartCoroutine(SpawnWave());

                // Esperar hasta que se terminen todos los enemigos de la ola
                yield return new WaitUntil(() => enemiesSpawnedInWave >= enemiesPerWave && EnemysEnable() == 0);

                // Nueva ola
                wave++;
                enemiesPerWave *= 2;
                isSpawningWave = false;
            }

            yield return null;
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
            if (alma)
            {
                SpawnEnemy();
                enemiesSpawnedInWave++;//me cuenta los enemigos que an aparecido
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
        return count;

    }
}
