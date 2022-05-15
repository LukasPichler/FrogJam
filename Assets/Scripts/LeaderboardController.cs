using System;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    public TMP_InputField MemberID;
    public TMP_Text PlayerScore;
    public int ID;
    private int MaxScores = 10;
    public TMP_Text[] Entries;
    private void Start()
    {
        PlayerScore.text = CalculateScore.Score+"";

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session"+ response.ToString());
                
                return;
            }
            ShowScores();
            Debug.Log("successfully started LootLocker session");
        });
    }

    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(ID, MaxScores, (response) =>
        {
            if (response.success)
            {
                Debug.Log("show scores");
                LootLockerLeaderboardMember[] scores = response.items;
                for (int i = 0; i < scores.Length; i++)
                {
                    Entries[i].text = (scores[i].rank + " " + scores[i].member_id + ".   " + scores[i].score);
                }

                if (scores.Length < MaxScores)
                {
                    for (int i = scores.Length; i < MaxScores; i++)
                    {
                        Entries[i].text = (i + 1).ToString() + ".   none";
                    }
                }
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }
    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(MemberID.text, int.Parse(PlayerScore.text), ID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");
                ShowScores();
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }
}
