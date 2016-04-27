using UnityEngine;
using System.Collections;

public class GameParameters {
    public int GameBoardWidth { get; private set; }
    public int GameBoardHeight { get; private set; }

    public GameParameters(int newGameBoardWidth, int newGameBoardHeight) {
        GameBoardWidth = newGameBoardWidth;
        GameBoardHeight = newGameBoardHeight;
    }
}
