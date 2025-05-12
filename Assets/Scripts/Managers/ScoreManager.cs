using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private RectTransform scoreRect;

    private int currentScore = 0;
    private int displayedScore = 0;
    private Tween scoreTween;

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount = 10)
    {
        currentScore += amount;

        // Stop previous tween if active
        if (scoreTween != null && scoreTween.IsActive())
            scoreTween.Kill();

        // Animate the displayed score to current score
        scoreTween = DOTween.To(() => displayedScore, x => {
            displayedScore = x;
            UpdateScoreText();
        }, currentScore, 0.5f).SetEase(Ease.OutQuad);

        // Add punch scale effect (shake/pop)
        scoreRect.DOKill(); // Kill any ongoing tween on Rect
        scoreRect.DOPunchScale(Vector3.one * 0.2f, 0.3f, 10, 1f);
        scoreRect.DOShakeAnchorPos(0.3f, 10f, 10, 90f, false, true);
    }

    private void UpdateScoreText()
    {
        scoreText.text = displayedScore.ToString();
    }
}