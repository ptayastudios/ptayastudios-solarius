using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxLife = 100f;
    public float life;

    public Transform lifeBar; // arraste o filho aqui no Inspector

    private Vector3 initialScale;

    void Start()
    {
        life = maxLife;
        if (lifeBar != null)
            initialScale = lifeBar.localScale;
    }

    void Update()
    {
        // Apenas para teste: diminui a vida apertando espaço
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
        }

        UpdateLifeBar();
    }

    public void TakeDamage(float amount)
    {
        life -= amount;
        life = Mathf.Clamp(life, 0f, maxLife);
    }

    void UpdateLifeBar()
    {
        if (lifeBar != null)
        {
            float ratio = life / maxLife;
            lifeBar.localScale = new Vector3(initialScale.x * ratio, initialScale.y, initialScale.z);
        }
    }
}
