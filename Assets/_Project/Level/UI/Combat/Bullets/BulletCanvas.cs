using TMPro;
using UnityEngine;

public class BulletCanvas : MonoBehaviour
{
    [SerializeField] private MonoBehaviour bulletCounter;
    [SerializeField] private TextMeshProUGUI textBullets;
    [SerializeField] private TextMeshProUGUI textBunchOfBullets;


    private IBulletUI _bulletCounter;

    private void Awake()
    {
        _bulletCounter = bulletCounter.GetComponent<IBulletUI>();
    }

    private void OnEnable()
    {
        _bulletCounter.OnSetBullets += ShowBullets;
        _bulletCounter.OnSetBunchOfBullets += ShowBunchOfBullets;
    }

    private void OnDisable()
    {
        _bulletCounter.OnSetBullets -= ShowBullets;
        _bulletCounter.OnSetBunchOfBullets += ShowBunchOfBullets;
    }

    private void ShowBullets(int currentBullelts, int bullets)
    {
        textBullets.text = currentBullelts+"/"+bullets;
    }
    private void ShowBunchOfBullets(int currentBullelts)
    {
        textBunchOfBullets.text = "x" + currentBullelts;
    }
}
