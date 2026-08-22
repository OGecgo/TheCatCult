using System;

public interface IDialogShow
{
    public bool isShowedText {get;}
    public void ShowMessage(string text);
    public void ShowSmoothText(string text);
    public void CleanMessage();
}
