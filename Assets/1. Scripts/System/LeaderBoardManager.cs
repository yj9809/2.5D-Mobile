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

    // 로그인 상태 확인
    private bool IsAuthenticated()
    {
        return Social.localUser.authenticated;
    }

    // 리더보드 UI 표시
    public void ShowLeaderBoardUI()
    {
        if (IsAuthenticated())
        {
            LoadTopScores(); // 리더보드 열기 전에 상위 점수 불러오기
            UpdateAndRecordScore(); // 리더보드 열기 전에 점수 갱신
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard); // 리더보드 UI 표시
        }
        else
        {
            // 로그인되지 않은 경우 로그인 요청 추가 (혹은 알림)
            Debug.LogWarning("사용자가 인증되지 않았습니다.");
        }
    }

    // 리더보드 상위 점수 로드
    public void LoadTopScores()
    {
        if (IsAuthenticated())
        {
            PlayGamesPlatform.Instance.LoadScores(
                GPGSIds.leaderboard, // 리더보드 ID
                LeaderboardStart.TopScores, // 상위 점수부터 시작
                10, // 불러올 점수의 개수
                LeaderboardCollection.Public, // 공개 리더보드
                LeaderboardTimeSpan.AllTime, // 전체 기간
                (LeaderboardScoreData data) => {
                    if (data.Valid)
                    {
                        int numScores = data.Scores.Length;

                        // 상위 점수 목록을 처리하는 코드
                        for (int i = 0; i < numScores; i++)
                        {
                            string playerId = data.Scores[i].userID;
                            long playerScore = data.Scores[i].value;

                            // 필요한 처리 로직 작성
                        }
                    }
                });
        }
        else
        {
            Debug.LogWarning("사용자가 인증되지 않았습니다.");
        }
    }

    // 나의 랭킹과 점수 보기
    public void myRank()
    {
        if (IsAuthenticated())
        {
            PlayGamesPlatform.Instance.LoadScores(
                GPGSIds.leaderboard, // 리더보드 ID
                LeaderboardStart.PlayerCentered, // 플레이어 중심 모드
                1, // 1개의 점수만 불러오기
                LeaderboardCollection.Public, // 공개 리더보드
                LeaderboardTimeSpan.AllTime, // 전체 기간
                (LeaderboardScoreData data) => {
                    if (data.Valid && data.Scores.Length > 0)
                    {
                        long playerScore = data.PlayerScore.value;

                        // 점수 갱신 확인
                        if (playerScore != DataManager.Instance.baseCost.playerData["gold"])
                        {
                            if (DataManager.Instance.baseCost.playerData["gold"] < playerScore)
                            {
                                DataManager.Instance.baseCost.playerData["gold"] = (int)playerScore;
                            }
                            RecordScore(true); // 점수 저장 후 UI 갱신
                        }
                    }
                });
        }
        else
        {
            Debug.LogWarning("사용자가 인증되지 않았습니다.");
        }
    }

    // 점수 등록
    private void RecordScore(bool UI = false)
    {
        // gold 값으로 MaxScore 설정
        int MaxScore = (int)DataManager.Instance.baseCost.playerData["gold"];

        // gold 값이 제대로 설정되었는지 확인
        if (MaxScore > 0)
        {
            PlayGamesPlatform.Instance.ReportScore(MaxScore, GPGSIds.leaderboard, (bool success) => {
                if (success && UI)
                {
                    myRank(); // 리더보드 갱신
                }
                else
                {
                    Debug.LogWarning("점수 등록에 실패했습니다.");
                }
            });
        }
        else
        {
            Debug.LogWarning("Gold 값이 설정되지 않았거나 유효하지 않습니다.");
        }
    }

    // 점수 갱신 후 등록
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

                    // 새로운 점수가 리더보드 점수보다 클 경우 업데이트
                    if (playerScore > leaderboardScore)
                    {
                        RecordScore(false); // 점수만 등록하고 UI는 갱신하지 않음
                    }
                    else
                    {
                        Debug.LogWarning("플레이어 점수가 리더보드 점수보다 높지 않습니다.");
                    }
                }
                else
                {
                    Debug.LogWarning("리더보드 점수 데이터를 불러오는 데 실패했습니다.");
                }
            });
    }
}
