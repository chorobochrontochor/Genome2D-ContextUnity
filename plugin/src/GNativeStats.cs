namespace Genome2DNativePlugin
{
    using UnityEngine;
    using UnityEngine.UI;
    
    public class GNativeStats
    {
        public bool autoScale = false;
        
        private MonoBehaviour _stage;
        private Canvas _canvas;
        private CanvasScaler _canvasScaler;
        private GameObject _canvasObject;

        private int g2d_frames = 0;
        private float g2d_previousTime = 0;

        private Text g2d_statsText;

        static public int fps = 0;
        static public int drawCalls = 0;

        public GNativeStats(MonoBehaviour p_stage)
        {
            _stage = p_stage;

            g2d_previousTime = Time.time;
            
            ConfigureCanvas();
            ConfigureLabel();
        }

        public void Update(bool p_visible)
        {
            _canvasObject.SetActive(p_visible);
            if (p_visible)
            {
                g2d_frames++;
                float time = Time.time;

                if (time > g2d_previousTime + 1)
                {
                    fps = (int) Mathf.Round((g2d_frames) / (time - g2d_previousTime));

                    g2d_statsText.text = "<b><color=#55da55ff>FPS: " + fps + " </color> <color=#5555daff>Drawcalls: " +
                                         drawCalls + "</color></b>";
                    g2d_previousTime = time;
                    g2d_frames = 0;
                }
                else if (time < 1)
                {
                    fps = (int) Mathf.Round(1 / Time.deltaTime);
                    g2d_statsText.text = "<b><color=#55da55ff>FPS: " + fps + " </color> <color=#5555daff>Drawcalls: " +
                                         drawCalls + "</color></b>";
                }

                /**/
            }
        }
        
        private void ConfigureCanvas()
        {
            _canvasObject = new GameObject("GStats", typeof(Canvas));
            //canvasObject.tag = gameObject.tag;
            //canvasObject.layer = gameObject.layer;
            _canvasObject.transform.SetParent(_stage.transform, false);

            _canvas = _canvasObject.GetComponent<Canvas>();

            RectTransform canvasRectTransform =  _canvasObject.GetComponent<RectTransform>();

            ResetRectTransform(canvasRectTransform);

            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            //canvas.pixelPerfect = pixelPerfect;
            //canvas.sortingOrder = sortingOrder;

            _canvasScaler = _canvasObject.AddComponent<CanvasScaler>();

            if (autoScale)
            {
                _canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            }
            else
            {
                _canvasScaler.scaleFactor = 1;
            }
        }

        private void ConfigureLabel()
        {
            GameObject gameObject = new GameObject("FPS", typeof(RectTransform));
            ContentSizeFitter fitter = gameObject.AddComponent<ContentSizeFitter>();
            RectTransform transform = gameObject.GetComponent<RectTransform>();
            transform.anchorMin = Vector2.up;
            transform.anchorMax = Vector2.up;
            transform.pivot = new Vector2(0,1);

            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            HorizontalLayoutGroup group = gameObject.AddComponent<HorizontalLayoutGroup>();

            gameObject.transform.SetParent(_canvas.transform, false);

            GameObject statsObject = new GameObject("Text", typeof(Text));
            statsObject.transform.SetParent(gameObject.transform, false);

            /* create UI Text component and apply settings */
            g2d_statsText = statsObject.GetComponent<Text>();
            g2d_statsText.alignment = TextAnchor.UpperLeft;

            g2d_statsText.horizontalOverflow = HorizontalWrapMode.Overflow;
            g2d_statsText.verticalOverflow = VerticalWrapMode.Overflow;
            g2d_statsText.supportRichText = true;
            g2d_statsText.font = Font.CreateDynamicFontFromOSFont("Arial", 20);
            g2d_statsText.text = "Initializing...";
            
            Image backgroundImage = gameObject.AddComponent<Image>();
            backgroundImage.color = new Color(0,0,0);
          
            group.padding.top = 4;
            group.padding.left = 4;
            group.padding.right = 4;
            group.padding.bottom = 4;

            group.SetLayoutHorizontal();
        }
        
        private void ResetRectTransform(RectTransform p_rectTransform)
        {
            p_rectTransform.localRotation = Quaternion.identity;
            p_rectTransform.localScale = Vector3.one;
            p_rectTransform.pivot = new Vector2(0.5f, 0.5f);
            p_rectTransform.anchorMin = Vector2.zero;
            p_rectTransform.anchorMax = Vector2.one;
            p_rectTransform.anchoredPosition3D = Vector3.zero;
            p_rectTransform.offsetMin = Vector2.zero;
            p_rectTransform.offsetMax = Vector2.zero;
        }
    }
    
    
}