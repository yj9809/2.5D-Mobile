using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] private Button leaderBoardButton;

    private void Start()
    {
        if (leaderBoardButton != null)
        {
            leaderBoardButton.onClick.AddListener(ShowLeaderBoardUI);
        }
    }

    // �α��� ���� Ȯ��
    private bool IsAuthenticated()
    {
        return Social.localUser.authenticated;
    }

    // �������� UI ǥ��
    public void ShowLeaderBoardUI()
    {
        if (IsAuthenticated())
        {
            LoadTopScores(); // �������� ���� ���� ���� ���� �ҷ�����
            UpdateAndRecordScore(); // �������� ���� ���� ���� ����
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard); // �������� UI ǥ��
        }
        else
        {
            // �α��ε��� ���� ��� �α��� ��û �߰� (Ȥ�� �˸�)
            Debug.LogWarning("����ڰ� �������� �ʾҽ��ϴ�.");
        }
    }

    // �������� ���� ���� �ε�
    public void LoadTopScores()
    {
        if (IsAuthenticated())
        {
            PlayGamesPlatform.Instance.LoadScores(
                GPGSIds.leaderboard, // �������� ID
                LeaderboardStart.TopScores, // ���� �������� ����
                10, // �ҷ��� ������ ����
                LeaderboardCollection.Public, // ���� ��������
                LeaderboardTimeSpan.AllTime, // ��ü �Ⱓ
                (LeaderboardScoreData data) => {
                    if (data.Valid)
                    {
                        int numScores = data.Scores.Length;

                        // ���� ���� ����� ó���ϴ� �ڵ�
                        for (int i = 0; i < numScores; i++)
                        {
                            string playerId = data.Scores[i].userID;
                            long playerScore = data.Scores[i].value;

                            // �ʿ��� ó�� ���� �ۼ�
                        }
                    }
                });
        }
        else
        {
            Debug.LogWarning("����ڰ� �������� �ʾҽ��ϴ�.");
        }
    }

    // ���� ��ŷ�� ���� ����
    public void myRank()
    {
        if (IsAuthenticated())
        {
            PlayGamesPlatform.Instance.LoadScores(
                GPGSIds.leaderboard, // �������� ID
                LeaderboardStart.PlayerCentered, // �÷��̾� �߽� ���
                1, // 1���� ������ �ҷ�����
                LeaderboardCollection.Public, // ���� ��������
                LeaderboardTimeSpan.AllTime, // ��ü �Ⱓ
                (LeaderboardScoreData data) => {
                    if (data.Valid && data.Scores.Length > 0)
                    {
                        long playerScore = data.PlayerScore.value;

                        // ���� ���� Ȯ��
                        if (playerScore != DataManager.Instance.baseCost.playerData["gold"])
                        {
                            if (DataManager.Instance.baseCost.playerData["gold"] < playerScore)
                            {
                                DataManager.Instance.baseCost.playerData["gold"] = (int)playerScore;
                            }
                            RecordScore(true); // ���� ���� �� UI ����
                        }
                    }
                });
        }
        else
        {
            Debug.LogWarning("����ڰ� �������� �ʾҽ��ϴ�.");
        }
    }

    // ���� ���
    private void RecordScore(bool UI = false)
    {
        // gold ������ MaxScore ����
        int MaxScore = (int)DataManager.Instance.baseCost.playerData["gold"];

        // gold ���� ����� �����Ǿ����� Ȯ��
        if (MaxScore > 0)
        {
            PlayGamesPlatform.Instance.ReportScore(MaxScore, GPGSIds.leaderboard, (bool success) => {
                if (success && UI)
                {
                    myRank(); // �������� ����
                }
                else
                {
                    Debug.LogWarning("���� ��Ͽ� �����߽��ϴ�.");
                }
            });
        }
        else
        {
            Debug.LogWarning("Gold ���� �������� �ʾҰų� ��ȿ���� �ʽ��ϴ�.");
        }
    }

    // ���� ���� �� ���
    private void UpdateAndRecordScore()
    {
        PlayGamesPlatform.Instance.LoadScores(
            GPGSIds.leaderboard,
            LeaderboardStart.PlayerCentered,
            1,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (LeaderboardScoreData data) =>
            {
                if (data.Valid && data.PlayerScore != null)
                {
                    long leaderboardScore = data.PlayerScore.value;
                    int playerScore = (int)DataManager.Instance.baseCost.playerData["gold"];

                    // ���ο� ������ �������� �������� Ŭ ��� ������Ʈ
                    if (playerScore > leaderboardScore)
                    {
                        RecordScore(false); // ������ ����ϰ� UI�� �������� ����
                    }
                    else
                    {
                        Debug.LogWarning("�÷��̾� ������ �������� �������� ���� �ʽ��ϴ�.");
                    }
                }
                else
                {
                    Debug.LogWarning("�������� ���� �����͸� �ҷ����� �� �����߽��ϴ�.");
                }
            });
    }
}
