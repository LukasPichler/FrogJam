using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveFile 
{
    public static List<List<SaveMovement.Tupel>> saveMovementsJump= new List<List<SaveMovement.Tupel>>();
    public static List<List<SaveMovement.Tupel>> saveMovementsRotate = new List<List<SaveMovement.Tupel>>();
    public static int currentPlayer = 0;
    
    public static void DeleteInput()
    {
        saveMovementsJump.Clear();
        saveMovementsRotate.Clear();
        currentPlayer = 0;
    }
}
