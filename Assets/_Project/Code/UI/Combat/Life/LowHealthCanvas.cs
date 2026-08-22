using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LowHealthCanvas : MonoBehaviour
{
    [SerializeField] private MonoBehaviour player;
    [SerializeField] private int maxAlphaValue = 40;
    [SerializeField] private float timeToShow = 1f;

    private ILowHealthUI lowHealthUI;
    private RawImage rawImage;
    private float maxAlphaValueNormilized;
    private float timeToShowNormilized;

    private Coroutine coroutine;
    private void OnEnable()
    {
        lowHealthUI.OnIsLowHealth += IsLowHealthShow;
        lowHealthUI.OnIsNotLowhealth += IsNotLowHealthShow;
    }

    private void OnDisable()
    {
        lowHealthUI.OnIsLowHealth -= IsLowHealthShow;
        lowHealthUI.OnIsNotLowhealth -= IsNotLowHealthShow;
    }

    private void Awake()
    {
        lowHealthUI = player.GetComponent<ILowHealthUI>();
        rawImage = this.GetComponent<RawImage>();
        rawImage.enabled = false;   
        maxAlphaValueNormilized = maxAlphaValue / 255f;
        timeToShowNormilized = timeToShow / 100f;
    }

    private void IsLowHealthShow()
    {
        rawImage.enabled = true;
        if (coroutine == null) coroutine = StartCoroutine(CoroutineShowLowHealth());
    }
    private void IsNotLowHealthShow()
    {
        rawImage.enabled = false;
        if (coroutine != null) StopCoroutine(coroutine);
    }

    private IEnumerator CoroutineShowLowHealth()
    {
        float moveAdd = 0.01f;
        Color color;
        while (true)
        {
            color = rawImage.color;
            color.a += moveAdd;
            if (color.a >= maxAlphaValueNormilized)
            {
                color.a = maxAlphaValueNormilized;
                moveAdd = -moveAdd;
            }
            else if (color.a <= 0f)
            {
                color.a = 0;
                moveAdd = -moveAdd;
            }
            rawImage.color = color;

            yield return new WaitForSeconds(timeToShowNormilized);
        }
    }
}
