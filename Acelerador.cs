using UnityEngine;

public class Acelerador : MonoBehaviour
{
    public float fatorAceleracao = 2f;
    public float duracao = 5f;
    public GameObject pocaPrefab;

    private void OnTriggerEnter(Collider other)
    {
        Jogador jogador = other.GetComponent<Jogador>();
        if (jogador != null)
        {
            jogador.AplicarBuff(duracao, fatorAceleracao);
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
