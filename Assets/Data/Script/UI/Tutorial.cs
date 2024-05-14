using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<GameObject> _images;
    [SerializeField] private int _page;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _backButton;

    public event UnityAction Close;

    private void Start()
    {
        foreach (var image in _images)
            image.SetActive(false);

        _images[_page - 1].SetActive(true);

        if(_page == 1)
            _backButton.gameObject.SetActive(false);
        else if(_page == _images.Count)
            _nextButton.gameObject.SetActive(false);
    }

    public void OpenTutorial()
    {
        Time.timeScale = 0;
    }

    public void CloseTutorial()
    {
        Close?.Invoke();
        Time.timeScale = 1.0f;
    }

    public void NextPage()
    {
        if(_page <= _images.Count)
        {
            if(_backButton.gameObject.activeSelf == false)
                _backButton.gameObject.SetActive(true);

            _images[_page - 1].SetActive(false);
            _images[_page++].SetActive(true);

            if(_page == _images.Count)
                _nextButton.gameObject.SetActive(false);
        }
    }

    public void BackPage()
    {
        if(_page - 2 >= 0)
        {
            if(_nextButton.gameObject.activeSelf == false)
                _nextButton.gameObject.SetActive(true);

            _images[_page - 1].SetActive(false);
            _images[_page - 2].SetActive(true);

            _page -= 1;

            if (_page == 1)
                _backButton.gameObject.SetActive(false);
        }
    }
}
