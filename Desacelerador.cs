using UnityEngine;

public class Desacelerador : MonoBehaviour
{
    public float fatorDeDesaceleracao = 0.5f; // diminui na metade
    public float duracao = 3f; // duração do efeito em segundos
    public GameObject pocaPrefab;

    private void OnTriggerEnter(Collider other)
    {
        Jogador jogador = other.GetComponent<Jogador>();
        if (jogador != null)
        {
            jogador.AplicarDebuff(duracao, fatorDeDesaceleracao);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ilha"))
        {
            Vector3 pontoColisao = collision.contacts[0].point;

            if (Physics.Raycast(pontoColisao + Vector3.up, Vector3.down, out RaycastHit hit, 2f))
            {
                Instantiate(pocaPrefab, hit.point, Quaternion.identity);
            }
            else
            {
                Instantiate(pocaPrefab, pontoColisao, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
