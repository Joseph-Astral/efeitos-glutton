using UnityEngine;

public class Desacelerador : MonoBehaviour
{
    public float fatorDeDesaceleracao = 0.5f;
    public float duracao = 3f;
    public float tempoFlutuando = 6f;
    public float amplitude = 0.2f;
    public float frequencia = 2f;

    private bool atingiuChao = false;
    private float tempoRestante;
    private bool coletado = false;
    private Vector3 posicaoInicial;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tempoRestante = tempoFlutuando;
    }

    void Update()
    {
        if (atingiuChao)
        {
            float novaY = posicaoInicial.y + Mathf.Sin(Time.time * frequencia) * amplitude;
            transform.position = new Vector3(transform.position.x, novaY, transform.position.z);

            tempoRestante -= Time.deltaTime;
            if (tempoRestante <= 0)
                Destroy(gameObject);

            transform.Rotate(Vector3.up * 50f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!atingiuChao && collision.gameObject.CompareTag("Ilha"))
        {
            atingiuChao = true;
            rb.angularVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, 3f))
            {
                transform.position = hit.point + Vector3.up * 0.05f;
            }
            else
            {
                transform.position += Vector3.up * 0.2f;
            }

            rb.isKinematic = true;
            rb.useGravity = false;

            posicaoInicial = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (coletado) return;

        Jogador jogador = other.GetComponent<Jogador>();
        if (jogador != null)
        {
            jogador.AplicarDebuff(duracao, fatorDeDesaceleracao);
            coletado = true;
            Destroy(gameObject);
        }
    }
}