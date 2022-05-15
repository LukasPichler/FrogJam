using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CalculateScore
{
    public static int Score = 0;
    
    //time isch di zeit de jemand fürs level gebraucht hot, deaths wie oft jemand des level gstorbm isch
    public static void CalculateNewScore( float time)
    {
        Score += (int)(time*20);
    }

    public static void Death()
    {
        Score -= 5;
        Score = Mathf.Max(0, Score);
    }
}
