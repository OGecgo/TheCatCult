using System.Collections;
using TMPro;
using UnityEngine;

public class DialogShowTextMeshPro : MonoBehaviour, IDialogShow
{
    [SerializeField] private float timeShowText = 2f;

    private TextMeshProUGUI textDialog;
    private Coroutine coroutine;

    public bool isShowedText {get; private set;}
    public void ShowMessage(string text)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        CleanMessage();
        textDialog.text = text;
        isShowedText = true;
    }
    public void ShowSmoothText(string text)
    {
        CleanMessage();
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(CoroutineShowDialog(text));
    }
    public void CleanMessage()
    {
        textDialog.text = "";
        isShowedText = false;
    }
    private void Awake()
    {
        textDialog = this.GetComponent<TextMeshProUGUI>();
        textDialog.text = "";
        isShowedText = false;
    }

    private IEnumerator CoroutineShowDialog(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            textDialog.text += text[i];
            yield return new WaitForSeconds(timeShowText);
        }
        isShowedText = true;
    }

}
