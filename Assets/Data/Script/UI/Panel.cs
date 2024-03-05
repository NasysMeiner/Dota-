using UnityEngine;

public class Panel : MonoBehaviour
{
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }
}
