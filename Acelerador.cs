using UnityEngine;

public class Acelerador : MonoBehaviour
{
    public float fatorAceleracao = 2f;
    public float duracao = 5f;
    public float tempoFlutuando = 6f;  // tempo antes de desaparecer
    public float amplitude = 0.2f;     // intensidade da flutuação
    public float frequencia = 2f;      // velocidade da flutuação

    private bool atingiuChao = false;
    private bool coletado = false;
    private float tempoRestante;
    private Vector3 posicaoInicial;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tempoRestante = tempoFlutuando;
    }

    void Update()
    {
        if (atingiuChao && !coletado)
        {
            // flutuando
            float novaY = posicaoInicial.y + Mathf.Sin(Time.time * frequencia) * amplitude;
            transform.position = new Vector3(transform.position.x, novaY, transform.position.z);

            // desaparece depois de um tempo
            tempoRestante -= Time.deltaTime;
            if (tempoRestante <= 0)
                Destroy(gameObject);

            // objeto fica rotacionando
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
            jogador.AplicarBuff(duracao, fatorAceleracao);
            coletado = true;
            Destroy(gameObject);
        }
    }
}