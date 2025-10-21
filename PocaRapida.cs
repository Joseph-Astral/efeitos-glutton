using UnityEngine;

public class PocaRapida : MonoBehaviour
{
    public float fator = 1.5f;
    public float duracao = 3f;
    public float tempoDeVida = 3f;
    public GameObject particulasPrefab;

    private void Start()
    {
        if (particulasPrefab != null)
        {
            GameObject fx = Instantiate(particulasPrefab, transform.position, Quaternion.identity);
            fx.transform.SetParent(transform);
        }

        Destroy(gameObject, tempoDeVida);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jogador"))
        {
            Jogador jogador = other.GetComponent<Jogador>();
            if (jogador != null)
            {
                jogador.AplicarDebuff(duracao, fator);
            }
        }
    }
}
