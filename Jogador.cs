using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Jogador : MonoBehaviour
{
    public float forceMultiplier = 5f;
    public float velocidadeMaxima = 6f;
    public float duracaoEfeito = 3f;

    public ParticleSystem particulasMorte;
    private Rigidbody rb;
    public float velocidadeBase;
    public float forcaBase;

    public int pontos = 0;
    public int danoPontos = 10;

    public bool invencivel = false;
    public float duracaoInvencivel = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocidadeBase = velocidadeMaxima;
        forcaBase = forceMultiplier;
    }

    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        Vector3 direcao = new Vector3(horizontalInput, 0, verticalInput);

        if (rb.linearVelocity.magnitude <= velocidadeMaxima)
        {
            rb.AddForce(direcao * forceMultiplier);
        }

        if (duracaoEfeito > 0)
        {
            duracaoEfeito -= Time.deltaTime;
            if (duracaoEfeito <= 0)
            {
                ResetarEfeitos();
            }
        }

        if (invencivel && duracaoInvencivel > 0)
        {
            duracaoInvencivel -= Time.deltaTime;
            if (duracaoInvencivel <= 0)
            {
                invencivel = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Perigo"))
        {
            Instantiate(particulasMorte, transform.position, Quaternion.identity);
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }

        if (invencivel)
        {
            int perda = Mathf.RoundToInt(danoPontos * 0.3f);
            pontos = Mathf.Max(0, pontos - perda);
        }
    }

    public void AplicarBuff(float duracao, float fator)
    {
        velocidadeMaxima = velocidadeBase * fator;
        forceMultiplier = forcaBase * fator;
        duracaoEfeito = duracao;
    }

    public void AplicarDebuff(float duracao, float fator)
    {
        velocidadeMaxima = velocidadeBase * fator;
        forceMultiplier = forcaBase * fator;
        duracaoEfeito = duracao;
    }

    public void AplicarInvencivel(float duracao)
    {
        invencivel = true;
        duracaoInvencivel = duracao;
    }

    private void ResetarEfeitos()
    {
        velocidadeMaxima = velocidadeBase;
        forceMultiplier = forcaBase;
    }
}
