using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyCatLifeReaction : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [Header("Enemy take damage")]
    [SerializeField] private float timeDamageWillShowed = 0.3f;
    [SerializeField] private Color colorWhenTakedDamage = Color.red;

    private ILifeAction enemyCatLife;
    private Coroutine coroutine;
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        enemyCatLife = this.GetComponent<ILifeAction>();
    }

    private void Start()
    {
        originalColor = meshRenderer.material.color;
    }

    private void OnEnable()
    {
        enemyCatLife.OnIsHit += UpdateMeshAttaked;
    }

    private void OnDisable()
    {
        enemyCatLife.OnIsHit -= UpdateMeshAttaked; 
    }

    private void UpdateMeshAttaked()
    {
        if (coroutine != null) 
        {
            StopCoroutine(coroutine);
            meshRenderer.material.color = originalColor;
        }
        coroutine = StartCoroutine(ShowDamageTaked());        
    }

    private IEnumerator ShowDamageTaked()
    {
        meshRenderer.material.color = colorWhenTakedDamage;
        yield return new WaitForSeconds(timeDamageWillShowed);
        meshRenderer.material.color = originalColor;
    }
}
