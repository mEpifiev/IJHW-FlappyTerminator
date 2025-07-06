using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _scoreCounter.Changed += UpdateSocre;

        UpdateSocre(_scoreCounter.Score);
    }

    private void OnDisable()
    {
        _scoreCounter.Changed -= UpdateSocre;
    }

    private void UpdateSocre(int score)
    {
        _text.text = score.ToString();
    }
}
