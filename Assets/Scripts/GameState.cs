using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public static bool titleScreenComplete = false;
    public static bool doorsSetUp = false;

    public static bool cluckingtonFed = false;
    public static bool puzzleOneSolved = false;
    public static bool puzzleTwoSolved = false;
    public static bool dayTime = false;
    public static bool sunPlaced = false;
    public static int chickenFed = 0;
    public static int eggsCollected = 0;
    public static bool eggSold = false;
    public static bool dogEating = false;
    public static int itemsTaken = 0;
    public static bool endGame = false;
    public static bool paused = false;

    public static void reset()
    {
        titleScreenComplete = false;
        doorsSetUp = false;

        cluckingtonFed = false;
        puzzleOneSolved = false;
        puzzleTwoSolved = false;
        dayTime = false;
        sunPlaced = false;
        chickenFed = 0;
        eggsCollected = 0;
        eggSold = false;
        dogEating = false;
        itemsTaken = 0;
        endGame = false;
        paused = false;
        Piano.correct = false;
    }
}
