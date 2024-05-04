using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : Panel
{
    public override void OpenPanel()
    {
        base.OpenPanel();
        Time.timeScale = 0;
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        Time.timeScale = 1;
    }
}
