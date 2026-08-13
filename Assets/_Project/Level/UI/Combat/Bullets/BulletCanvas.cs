using TMPro;
using UnityEngine;

public class BulletCanvas : MonoBehaviour
{
    [SerializeField] private MonoBehaviour bulletCounter;

    private IBulletUI _bulletCounter;
    private TextMeshProUGUI text;

    private void Awake()
    {
        _bulletCounter = bulletCounter.GetComponent<IBulletUI>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _bulletCounter.OnSetBullets += ShowBullets;
    }

    private void OnDisable()
    {
        _bulletCounter.OnSetBullets -= ShowBullets;
    }

    private void ShowBullets(int lastBullelts, int bullets)
    {
        text.text = lastBullelts+"/"+bullets;
    }
}
