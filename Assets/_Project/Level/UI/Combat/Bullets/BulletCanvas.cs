using TMPro;
using UnityEngine;

public class BulletCanvas : MonoBehaviour
{
    [SerializeField] private MonoBehaviour bulletCounter;
    [SerializeField] private TextMeshProUGUI textBullets;
    [SerializeField] private TextMeshProUGUI textBunchOfBullets;


    private IBulletUI bulletUI;

    private void Awake()
    {
        bulletUI = bulletCounter.GetComponent<IBulletUI>();
    }

    private void OnEnable()
    {
        bulletUI.OnSetBullets += ShowBullets;
        bulletUI.OnSetBunchOfBullets += ShowBunchOfBullets;
    }

    private void OnDisable()
    {
        bulletUI.OnSetBullets -= ShowBullets;
        bulletUI.OnSetBunchOfBullets += ShowBunchOfBullets;
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
