using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestText : MonoBehaviour
{
    [SerializeField] private TMP_Text _questName;

    [SerializeField] private Slider _crossSlider;
    [SerializeField] private float _crossSpeed;

    public ItemForCollection ItemForCollection { get; private set; }

    public void Initialize(ItemForCollection item)
    {
        ItemForCollection = item;
        _questName.text = item.Name;

        _crossSlider.value = _crossSlider.minValue;
    }

    public void Complete()
    {
        if (_crossSlider.value == _crossSlider.minValue)
            StartCoroutine(CrossAnimation());
    }

    private IEnumerator CrossAnimation()
    {
        while (_crossSlider.value < _crossSlider.maxValue)
        {
            yield return Time.deltaTime;

            _crossSlider.value += Time.deltaTime * _crossSpeed; 
        }
    }
}