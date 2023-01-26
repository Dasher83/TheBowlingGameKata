using UnityEngine;
using TMPro;


public class TotalScoreComponent : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    private int score;

    public int Score { set { score = value; } }

    private void OnEnable()
    {
        scoreText.text = $"{score.ToString()} points";
    }
}
