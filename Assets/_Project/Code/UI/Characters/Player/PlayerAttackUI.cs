using System;
using UnityEngine;

public class PlayerAttackUI : MonoBehaviour, IBulletUI
{
    private IBulletsAction playerAttack;

    public event Action<int, int> OnSetBullets;
    public event Action<int> OnSetBunchOfBullets;

    private void OnEnable()
    {
        playerAttack.OnSetBullets += UpdateUIBullets;
        playerAttack.OnSetBunchOfBullets += UpdateUIBunchOfBullets;
    }

    private void OnDisable()
    {
        playerAttack.OnSetBullets -= UpdateUIBullets;
        playerAttack.OnSetBunchOfBullets -= UpdateUIBunchOfBullets;
    }

    private void Awake()
    {
        playerAttack = this.GetComponent<IBulletsAction>();
    }

    private void UpdateUIBullets(int bullets, int maxBullets)
    {
        OnSetBullets.Invoke(bullets, maxBullets);
    }

    private void UpdateUIBunchOfBullets(int bunchOfBullets)
    {
        OnSetBunchOfBullets.Invoke(bunchOfBullets);
    }
}
