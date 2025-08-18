using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{


    public TMP_Text X_ScoreText; 
    public TMP_Text O_ScoreText;



    void Start()
    {
        X_ScoreText.text = "";
        O_ScoreText.text = "";
    }

    public void UpdateScoreDisplay(int x, int o)
    {
        X_ScoreText.text = "" + x;
        O_ScoreText.text = "" + o;
    }
}
