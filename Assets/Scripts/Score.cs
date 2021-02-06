using UnityEngine;

public class Score : MonoBehaviour
{
    public int Level { get; private set; } = 0;

    [SerializeField] private FontRenderer scoreRenderer;
    [SerializeField] private FontRenderer linesRenderer;
    [SerializeField] private Spawn spawn;

    private int score = 0;
    private DropletSpawner dropletSpawner;

    void Start()
    {
        dropletSpawner = GameObject.Find(GlobalNames.dropletSpawner).GetComponent<DropletSpawner>();
    }

    public void UpdateScoreRowClear(int numLines)
    {
        if (numLines == 1)
        {
            score += 40 * (numLines + 1);
        }
        else if (numLines == 2)
        {
            score += 100 * (numLines + 1);
        }
        else if (numLines == 3)
        {
            score += 300 * (numLines + 1);
        }
        else if (numLines == 4)
        {
            score += 1200 * (numLines + 1);
        }
        else
        {
            score += 2000 * (numLines + 1);
        }

        UpdateScoreDisplay();
    }

    public void UpdateLevel(int numLines)
    {
        const int additionalLinesPerLevel = 2;
        if (numLines >= (additionalLinesPerLevel / 2.0f) * ((Level + 1) * (Level + 1) + Level + 1))
        {
            ++Level;
            dropletSpawner.StartDrop();
            spawn.ShouldGenerateXanax = true;
        }

        linesRenderer.SetText(numLines.ToString());
    }

    private void UpdateScoreDisplay()
    {
        scoreRenderer.SetText(score.ToString());
    }
}
