using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsCounter : MonoBehaviour
{
    // FPS 업데이트 간격 (초 단위)
    public float updateInterval = 0.5F;

    private float accum = 0; // 주기별 누적 FPS
    private int frames = 0; // 주기별 그려진 프레임 수
    private float timeleft; // 현재 주기의 남은 시간

    // FPS를 표시할 텍스트
    TextMeshProUGUI textFpsCounter;

    void Start()
    {
        textFpsCounter = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

        // 남은 시간 갱신
        timeleft -= Time.deltaTime;
        // 주기별 FPS 누적
        accum += Time.timeScale / Time.deltaTime;
        // 프레임 수 증가
        ++frames;

        // 주기가 끝나면 GUI 텍스트 업데이트하고 새 주기 시작
        if (timeleft <= 0.0)
        {
            // 소수점 2자리로 표시
            float fps = accum / frames;
            string format = System.String.Format("FPS : {0:F2}", fps);
            // FPS 텍스트 설정
            textFpsCounter.text = format;

            // 다음 주기를 위한 초기화
            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }

    }
}
