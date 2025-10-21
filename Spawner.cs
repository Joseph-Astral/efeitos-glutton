using UnityEngine;

public class Spawner3D : MonoBehaviour
{
    [Header("Objetos para spawnar")]
    public GameObject[] objetosParaSpawnar;

    [Header("Área de spawn (X, Y, Z)")]
    public Vector3 areaSpawn = new Vector3(10f, 10f, 10f); // largura, altura, profundidade

    [Header("Controle de spawn")]
    public float intervaloMin = 1.5f;
    public float intervaloMax = 3f;
    public int maxObjetosNaCena = 15;

    private float timer = 0f;
    private float proximoSpawn;

    void Start()
    {
        SortearProximoTempo();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= proximoSpawn && ContarObjetosNaCena() < maxObjetosNaCena)
        {
            SpawnarObjeto();
            SortearProximoTempo();
            timer = 0f;
        }
    }

    void SpawnarObjeto()
    {
        int index = Random.Range(0, objetosParaSpawnar.Length); // item aleatório

        Vector3 posicao = new Vector3( // lugar aleatório
            Random.Range(-areaSpawn.x / 2, areaSpawn.x / 2),
            Random.Range(3, areaSpawn.y), // altura aleatória
            Random.Range(-areaSpawn.z / 2, areaSpawn.z / 2)
        );

        Instantiate(objetosParaSpawnar[index], posicao, Quaternion.identity);
    }

    void SortearProximoTempo()
    {
        proximoSpawn = Random.Range(intervaloMin, intervaloMax);
    }

    int ContarObjetosNaCena()
    {
        return GameObject.FindGameObjectsWithTag("Caindo").Length;
    }
}
