using UnityEngine;
using System.Collections;
/// <summary>
/// A class for determining points in 2D space.
/// Primarily for drawing 2D grids.
/// </summary>
public class GameSettings : MonoBehaviour {
    private Vector2[] gameboardTileCoordinates;

    public int GridWidth { get; set; }
    public int GridHeight { get; set; }

    public Vector2 GameboardCenterPoint {
        get {
            int numCoordinates = gameboardTileCoordinates.Length;
            Vector2 sum = new Vector2(0,0);

            foreach ( Vector2 coor in gameboardTileCoordinates ) {
                sum += coor;
            }

            float centerX = sum.x;
            float centerY = sum.y;
            centerX = centerX / numCoordinates;
            centerY = centerY / numCoordinates;

            return new Vector2 ( centerX , centerY );
        }
    }

    public Vector2[] DetermineCoordinates () {
        if ( gameboardTileCoordinates == null) {
            gameboardTileCoordinates = new Vector2[GridWidth * GridHeight];
            for ( int i = 0, x = 0; x < GridWidth; x++ ) {
                for ( int y = 0; y < GridHeight; y++, i++ ) {
                    gameboardTileCoordinates[i] = new Vector2 ( x , y );
                }
            }
        }

        return gameboardTileCoordinates;
    }
}
