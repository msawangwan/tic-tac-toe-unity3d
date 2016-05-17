using UnityEngine;
using System.Collections.Generic;

public class ResourcePath {
    //Players
    public const string playerHuman = "Player/PlayerHuman";
    public const string playerAI = "Player/PlayerAI";

    //UI
    public const string mainMenu = "UI/Panel_MainMenu";
    public const string roundOverMenu = "UI/Panel_RoundOverMenu";

    //Grid
    public const string grid2DTileBasic = "Grid2D/Sprite/grid_tile_128x128_blue_white_border";

    //Tictactoe
    public const string x = "TicTacToe/x_128x128";
    public const string o = "TicTacToe/o_128x128";

    //Material
    public const string lineMat = "Materials/line-material";

    //Audio
    public const string mainTheme = "Audio/Music/menu-theme";
    public const string playTheme = "Audio/Music/play-theme";

    public const string btn1 = "Audio/SFX/button-1";
    public const string btn2 = "Audio/SFX/button-2";
    public const string btn3 = "Audio/SFX/button-3";

    public const string scribble1 = "Audio/SFX/scribble-1";
    public const string scribble2 = "Audio/SFX/scribble-2";
    public const string scribble3 = "Audio/SFX/scribble-3";
    public const string scribble4 = "Audio/SFX/scribble-4";
    public const string scribble5 = "Audio/SFX/scribble-5";
    public const string scribble6 = "Audio/SFX/scribble-6";
    public const string scribble7 = "Audio/SFX/scribble-7";

    public static List<string> GetScribbleFilepaths() {
        List<string> paths = new List<string>() {
            scribble1,
            scribble2,
            scribble3,
            scribble4,
            scribble5,
            scribble6,
            scribble7,
        };
        return paths;
    }

    public const string blip1 = "Audio/SFX/blip-1";
    public const string blip2 = "Audio/SFX/blip-2";
    public const string blip3 = "Audio/SFX/blip-3";
    public const string blip4 = "Audio/SFX/blip-4";
    public const string blip5 = "Audio/SFX/blip-5";
    public const string blip6 = "Audio/SFX/blip-6";

    public static List<string> GetBlipFilepaths() {
        List<string> paths = new List<string>() {
            blip1,
            blip2,
            blip3,
            blip4,
            blip5,
            blip6,
        };
        return paths;
    }

    public const string onWinSoundClip = "Audio/SFX/sfx-victory-delayed";
    public const string sfxReady = "Audio/SFX/Announcer/Ready";
    public const string sfxGameOver = "Audio/SFX/Announcer/GameOver";
    public const string sfxWinner = "Audio/SFX/Announcer/Winner";
    public const string sfxContinue = "Audio/SFX/Announcer/Continue";
    public const string sfxNoMercy = "Audio/SFX/Announcer/ShowNoMercy";
}
