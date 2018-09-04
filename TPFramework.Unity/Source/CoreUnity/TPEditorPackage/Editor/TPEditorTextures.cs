/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEngine;

namespace TPFramework.Unity.Editor
{
    public static class TPEditorTextures
    {
        private static Texture2D whiteTexture;
        public static Texture2D WhiteTexture {
            get {
                if (whiteTexture == null)
                {
                    whiteTexture = new Texture2D(1, 1);
                    whiteTexture.SetPixel(0, 0, Color.white);
                    whiteTexture.Apply();
                }
                return whiteTexture;
            }
        }

        private static Texture2D blackTexture;
        public static Texture2D BlackTexture {
            get {
                if (blackTexture == null)
                {
                    blackTexture = new Texture2D(1, 1);
                    blackTexture.SetPixel(0, 0, Color.black);
                    blackTexture.Apply();
                }
                return blackTexture;
            }
        }

        private static Texture2D greyTexture;
        public static Texture2D GreyTexture {
            get {
                if (greyTexture == null)
                {
                    greyTexture = new Texture2D(1, 1);
                    greyTexture.SetPixel(0, 0, Color.grey);
                    greyTexture.Apply();
                }
                return greyTexture;
            }
        }

        private static Texture2D redTexture;
        public static Texture2D RedTexture {
            get {
                if (redTexture == null)
                {
                    redTexture = new Texture2D(1, 1);
                    redTexture.SetPixel(0, 0, Color.red);
                    redTexture.Apply();
                }
                return redTexture;
            }
        }

        private static Texture2D cyanTexture;
        public static Texture2D CyanTexture {
            get {
                if (cyanTexture == null)
                {
                    cyanTexture = new Texture2D(1, 1);
                    cyanTexture.SetPixel(0, 0, Color.cyan);
                    cyanTexture.Apply();
                }
                return cyanTexture;
            }
        }

        private static Texture2D blueTexture;
        public static Texture2D BlueTexture {
            get {
                if (blueTexture == null)
                {
                    blueTexture = new Texture2D(1, 1);
                    blueTexture.SetPixel(0, 0, Color.blue);
                    blueTexture.Apply();
                }
                return blueTexture;
            }
        }

        private static Texture2D greenTexture;
        public static Texture2D GreenTexture {
            get {
                if (greenTexture == null)
                {
                    greenTexture = new Texture2D(1, 1);
                    greenTexture.SetPixel(0, 0, Color.green);
                    greenTexture.Apply();
                }
                return greenTexture;
            }
        }

        private static Texture2D magentaTexture;
        public static Texture2D MagentaTexture {
            get {
                if (magentaTexture == null)
                {
                    magentaTexture = new Texture2D(1, 1);
                    magentaTexture.SetPixel(0, 0, Color.magenta);
                    magentaTexture.Apply();
                }
                return magentaTexture;
            }
        }

        private static Texture2D yellowTexture;
        public static Texture2D YellowTexture {
            get {
                if (yellowTexture == null)
                {
                    yellowTexture = new Texture2D(1, 1);
                    yellowTexture.SetPixel(0, 0, Color.yellow);
                    yellowTexture.Apply();
                }
                return yellowTexture;
            }
        }
    }
}
