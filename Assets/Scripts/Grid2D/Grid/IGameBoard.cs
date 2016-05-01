using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IGameBoard {
    Dictionary<Vector2 , Grid2DTile> TileTable { get; }
    Vector2 BoardCenterPoint { get; }

    int BoardWidth { get; }
    int BoardHeight { get; }

    bool HasTiles { get; }
    bool IsDrawnToScreen { get; }

    Vector2[] CreateTileWorldPositions ( );
    GameObject CreateBoard ( Vector2[] tilePositions );

    IEnumerable DrawTiles ( IFadeableGameObject fadingTiles, float fadeMultiplier );

    void DestroyBoardGameObject ( );
}
