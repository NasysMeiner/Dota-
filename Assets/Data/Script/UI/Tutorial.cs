using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<GameObject> _images;
    [SerializeField] private int _page;
    [SerializeField] private GameObject _tutor;
    [SerializeField] private GameObject _block;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _backButton;

    public event UnityAction Close;
    public event UnityAction Open;

    private void Start()
    {
        DataScene data = Repository.GetData<DataScene>();

         if (data.IsTutor == false)
         {
            _tutor.SetActive(true);
            data.IsTutor = true;
            _block.gameObject.SetActive(true);
            Repository.SetData(data);
         }

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
        Open?.Invoke();
        _tutor.gameObject.SetActive(true);
        _block.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseTutorial()
    {
        Close?.Invoke();
        _tutor.gameObject.SetActive(false);
        _block.gameObject.SetActive(false);
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

[System.Serializable]
public class TutorialData
{
    public bool IsTrue;
}
