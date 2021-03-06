﻿using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
    public static void SetCameraPosition( Grid2DComponent grid ) {
        Grid2DComponent g = grid;
        bool hasBeenSet = false;
        if (!hasBeenSet) {
            // get references
            GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
            //RectTransform uiCanvas = GameObject.FindGameObjectWithTag(Tags.ui_battle).GetComponent<RectTransform>(); // canvas
            //RectTransform uiImage = GameObject.FindGameObjectWithTag(Tags.ui_battleBottomHUD).GetComponent<RectTransform>(); // bottom ui container (an image element)
            // extract values from references
            float boardMidpointx = g.Grid.CenterPoint.x;
            float boardMidpointy = g.Grid.CenterPoint.y;
            float boardHeight = g.Grid.yDimension;
            //float uiCanvasHeight = uiCanvas.sizeDelta.y;
            //float uiImageHeight = uiImage.sizeDelta.y;
            // calculate positions
            //float uiHeightAsRatio = uiCanvasHeight / uiImageHeight;
            float cameraXPosition = boardMidpointx;
            //float cameraYPosition = boardHeight / uiHeightAsRatio;
            float cameraYPosition = boardMidpointy;
            const float cameraZPosition = -10.0f;
            float cameraOrthographicSize = boardHeight - cameraYPosition;
            // create position and input calculated values
            Vector3 cameraPosition = new Vector3(cameraXPosition, cameraYPosition, cameraZPosition);
            mainCam.transform.position = cameraPosition;
            //mainCam.GetComponent<Camera>( ).orthographicSize = cameraOrthographicSize;
            // set bool
            hasBeenSet = true;
        } else {
            Debug.Log( "[MainCamera][SetCameraPosition] camera already set!" );
        }
    }
}
