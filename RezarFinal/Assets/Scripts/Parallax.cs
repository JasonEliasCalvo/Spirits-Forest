using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public GameObject[] layers;
        public float parallaxSpeed;
    }

    public ParallaxLayer[] parallaxLayers;
    public Camera mainCamera;

    private Vector3 previousCameraPosition;
    private float backgroundWidth;

    void Start()
    {
        previousCameraPosition = mainCamera.transform.position;
        backgroundWidth = parallaxLayers[0].layers[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            float parallax = (previousCameraPosition.x - mainCamera.transform.position.x) * layer.parallaxSpeed;

            foreach (GameObject background in layer.layers)
            {
                float layerTargetPosX = background.transform.position.x + parallax;
                background.transform.position = new Vector3(layerTargetPosX, background.transform.position.y, background.transform.position.z);
            }
        }

        previousCameraPosition = mainCamera.transform.position;

        foreach (ParallaxLayer layer in parallaxLayers)
        {
            for (int i = 0; i < layer.layers.Length; i++)
            {
                if (mainCamera.transform.position.x > layer.layers[i].transform.position.x + backgroundWidth)
                {
                    RepositionBackground(layer.layers, i, 1);
                }
                else if (mainCamera.transform.position.x < layer.layers[i].transform.position.x - backgroundWidth)
                {
                    RepositionBackground(layer.layers, i, -1);
                }
            }
        }
    }

    private void RepositionBackground(GameObject[] backgrounds, int index, int direction)
    {
        int nextIndex = (index + direction + backgrounds.Length) % backgrounds.Length;
        backgrounds[index].transform.position = new Vector3(backgrounds[nextIndex].transform.position.x + backgroundWidth * direction,
                                                            backgrounds[index].transform.position.y,
                                                            backgrounds[index].transform.position.z);
    }
}
