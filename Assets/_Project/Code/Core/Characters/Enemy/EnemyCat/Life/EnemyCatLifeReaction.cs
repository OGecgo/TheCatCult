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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        enemyCatLife = this.GetComponent<ILifeAction>();
    }

    private void OnEnable()
    {
        enemyCatLife.OnIsAttacked += UpdateMeshAttaked;
    }

    private void OnDisable()
    {
        enemyCatLife.OnIsAttacked -= UpdateMeshAttaked; 
    }

    private void UpdateMeshAttaked()
    {
        if (coroutine != null)StopCoroutine(coroutine);
        coroutine = StartCoroutine(ShowDamageTaked());        
    }

    private IEnumerator ShowDamageTaked()
    {
        Color tempColor = meshRenderer.material.color;
        meshRenderer.material.color = colorWhenTakedDamage;
        yield return new WaitForSeconds(timeDamageWillShowed);
        meshRenderer.material.color = tempColor;
    }
}
