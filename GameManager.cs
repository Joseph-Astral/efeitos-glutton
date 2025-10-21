using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Over UI (opcional)")]
    public GameObject gameOverUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;

        if (gameOverUI != null) gameOverUI.SetActive(true);

        // Pare o spawner, se existir
        /*Spawner sp = FindObjectOfType<Spawner>();
        if (sp != null) sp.enabled = false;*/

        // Ou recarregue a cena depois de x segundos:
        // StartCoroutine(RecarregarDepois(2f));
    }

    // opcional
    // IEnumerator RecarregarDepois(float secs) { yield return new WaitForSecondsRealtime(secs); Time.timeScale = 1f; SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
}
