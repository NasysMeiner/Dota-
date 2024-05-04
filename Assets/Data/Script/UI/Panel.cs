using UnityEngine;

public class Panel : MonoBehaviour
{
    public virtual void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public virtual void OpenPanel()
    {
        gameObject.SetActive(true);
    }
}
