public class Triggers
{
    public bool EnterRoom = false;
    public bool Game_PBPushSun = false;
    public bool Game_PAPutIdol1 = false;
    public bool Game_PAPutIdol2 = false;
    public bool Game_PAPutIdol3 = false;
    public ItemName Game_PACurrentIdol1 = ItemName.None;
    public ItemName Game_PACurrentIdol2 = ItemName.None;
    public ItemName Game_PACurrentIdol3 = ItemName.None;

    public bool Game_PAPutFeather = false;
    public bool Game_PBPutRedHeart = false;
    public bool Game_PBPutBlueHeart = false;
    public bool Game_PBPutGreenHeart = false;
    public bool Game_PBPutYellowHeart = false;
    public float Game_PABalanceAngle = 0;
    public float Game_PBBalanceAngle = 0;

    public bool Game_Wheel_IsDone = false;
    public bool Game_ButtonGameCompleted = false;
    public bool Game_NumGameCompleted = false;
    public bool Game_Coffin_IsDone = false;
    public bool Game_BoxOpen = false;
    public bool Game_Balance_IsDone = false;
    public bool Game_OtherBalance_IsDone = false;
    public bool Game_Maze_IsDone = false;
    public bool Game_IdolsDone = false;
    public bool Game_BoxGetPieces = false;
    public bool Game_SignGame = false;
    public bool Game_PAPutPuzzlePieces = false;
    public bool Game_PBPutPuzzlePieces1 = false;
    public bool Game_PBPutPuzzlePieces2 = false;
    public bool Game_PuzzleGameCompleted = false;
    public bool Game_PuzzleGameOtherCompleted = false;

    public Heart GetHeart()
    {
        if (Game_PBPutRedHeart) return Heart.Red;
        if (Game_PBPutBlueHeart) return Heart.Blue;
        if (Game_PBPutYellowHeart) return Heart.Yellow;
        if (Game_PBPutGreenHeart) return Heart.Green;
        return Heart.None;
    }
    public ItemName GetHeartItem()
    {
        if (Game_PBPutRedHeart) return ItemName.红色心脏;
        if (Game_PBPutBlueHeart) return ItemName.蓝色心脏;
        if (Game_PBPutYellowHeart) return ItemName.黄色心脏;
        if (Game_PBPutGreenHeart) return ItemName.绿色心脏;
        return ItemName.None;
    }
    public void PutHeart(Heart h)
    {
        switch (h)
        {
            case Heart.None:
                Game_PBPutBlueHeart = false;
                Game_PBPutYellowHeart = false;
                Game_PBPutRedHeart = false;
                Game_PBPutGreenHeart = false;
                break;
            case Heart.Blue:
                Game_PBPutBlueHeart = true;
                Game_PBPutYellowHeart = false;
                Game_PBPutRedHeart = false;
                Game_PBPutGreenHeart = false;
                break;
            case Heart.Red:
                Game_PBPutBlueHeart = false;
                Game_PBPutYellowHeart = false;
                Game_PBPutRedHeart = true;
                Game_PBPutGreenHeart = false;
                break;
            case Heart.Green:
                Game_PBPutBlueHeart = false;
                Game_PBPutYellowHeart = false;
                Game_PBPutRedHeart = false;
                Game_PBPutGreenHeart = true;
                break;
            case Heart.Yellow:
                Game_PBPutBlueHeart = false;
                Game_PBPutYellowHeart = true;
                Game_PBPutRedHeart = false;
                Game_PBPutGreenHeart = false;
                break;
            default:
                break;
        }
    }
}


