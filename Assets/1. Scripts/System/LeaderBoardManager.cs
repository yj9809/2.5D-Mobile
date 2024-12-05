using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using TMPro;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] private Button leaderBoardButton;
    [SerializeField] private TextMeshProUGUI leaderBoardDebugText;

    private void Start()
    {
        if (leaderBoardButton != null)
        {
            leaderBoardButton.onClick.AddListener(ShowLeaderBoardUI);
        }

        // �ڵ� �α��� �õ�
        AuthenticateUser();
    }

    // ����� ����
    private void AuthenticateUser()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            UpdateDebugText(success ? "�α��� ����" : "�α��� ����");
        });
    }

    // �α��� ���� Ȯ��
    private bool IsAuthenticated() => Social.localUser.authenticated;

    // �������� UI ǥ��
    public void ShowLeaderBoardUI()
    {
        if (IsAuthenticated())
        {
            LoadTopScores(); // ���� ���� �ҷ�����
            UpdateAndRecordScore(); // ���� ����
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_one); // �������� UI ǥ��
        }
        else
        {
            UpdateDebugText("����ڰ� �������� �ʾҽ��ϴ�.");
        }
    }

    // �������� ���� ���� �ε�
    public void LoadTopScores()
    {
        if (IsAuthenticated())
        {
            PlayGamesPlatform.Instance.LoadScores(
                GPGSIds.leaderboard_one, // �������� ID
                LeaderboardStart.TopScores, // ���� �������� ����
                10, // �ҷ��� ������ ����
                LeaderboardCollection.Public, // ���� ��������
                LeaderboardTimeSpan.AllTime, // ��ü �Ⱓ
                (LeaderboardScoreData data) => {
                    if (data.Valid)
                    {
                        foreach (var score in data.Scores)
                        {
                            UpdateDebugText($"�÷��̾� ID: {score.userID}, ����: {score.value}");
                        }
                    }
                    else
                    {
                        UpdateDebugText("�������� �����Ͱ� ��ȿ���� �ʽ��ϴ�.");
                    }
                });
        }
        else
        {
            UpdateDebugText("����ڰ� �������� �ʾҽ��ϴ�.");
        }
    }

    // ���� ��ŷ�� ���� ����
    public void MyRank()
    {
        if (IsAuthenticated())
        {
            PlayGamesPlatform.Instance.LoadScores(
                GPGSIds.leaderboard_one, // �������� ID
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
                            DataManager.Instance.baseCost.playerData["gold"] = (int)playerScore;
                            RecordScore(true); // ���� ���� �� UI ����
                        }
                    }
                });
        }
        else
        {
            UpdateDebugText("����ڰ� �������� �ʾҽ��ϴ�.");
        }
    }

    // ���� ���
    private void RecordScore(bool UI = false)
    {
        int MaxScore = (int)DataManager.Instance.baseCost.playerData["gold"];

        if (MaxScore > 0)
        {
            PlayGamesPlatform.Instance.ReportScore(MaxScore, GPGSIds.leaderboard_one, (bool success) => {
                if (success && UI)
                {
                    MyRank(); // �������� ����
                }
                else
                {
                    UpdateDebugText("���� ��Ͽ� �����߽��ϴ�.");
                }
            });
        }
        else
        {
            UpdateDebugText("Gold ���� �������� �ʾҰų� ��ȿ���� �ʽ��ϴ�.");
        }
    }

    // ���� ���� �� ���
    private void UpdateAndRecordScore()
    {
        PlayGamesPlatform.Instance.LoadScores(
            GPGSIds.leaderboard_one,
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

                    if (playerScore > leaderboardScore)
                    {
                        RecordScore(false); // ������ ����ϰ� UI�� �������� ����
                    }
                    else
                    {
                        UpdateDebugText("�÷��̾� ������ �������� �������� ���� �ʽ��ϴ�.");
                    }
                }
                else
                {
                    UpdateDebugText("�������� ���� �����͸� �ҷ����� �� �����߽��ϴ�.");
                }
            });
    }

    // ����� �޽����� UI�� ������Ʈ
    private void UpdateDebugText(string message)
    {
        if (leaderBoardDebugText != null)
        {
            leaderBoardDebugText.text = message;
        }
    }
}
