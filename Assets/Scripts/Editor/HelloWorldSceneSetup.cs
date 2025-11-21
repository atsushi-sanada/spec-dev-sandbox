#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

/// <summary>
/// Hello Worldシーンのセットアップを行うエディタスクリプト
/// CanvasとButtonを自動的に作成します
/// </summary>
public class HelloWorldSceneSetup
{
    [MenuItem("Tools/Setup Hello World Scene")]
    public static void SetupHelloWorldScene()
    {
        // 現在のシーンを取得
        Scene currentScene = SceneManager.GetActiveScene();
        
        // Canvasが既に存在するか確認
        Canvas existingCanvas = Object.FindObjectOfType<Canvas>();
        GameObject canvasObject;
        
        if (existingCanvas != null)
        {
            canvasObject = existingCanvas.gameObject;
            UnityEngine.Debug.Log("既存のCanvasを使用します");
        }
        else
        {
            // Canvasを作成
            canvasObject = new GameObject("Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvasObject.layer = LayerMask.NameToLayer("UI");
            
            // EventSystemが存在しない場合は作成
            if (Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
            {
                GameObject eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            }
            
            UnityEngine.Debug.Log("Canvasを作成しました");
        }
        
        // Buttonが既に存在するか確認
        Button existingButton = canvasObject.GetComponentInChildren<Button>();
        GameObject buttonObject;
        
        if (existingButton != null)
        {
            buttonObject = existingButton.gameObject;
            UnityEngine.Debug.Log("既存のButtonを使用します");
        }
        else
        {
            // Buttonを作成
            buttonObject = new GameObject("Button");
            buttonObject.transform.SetParent(canvasObject.transform, false);
            
            // RectTransformを設定
            RectTransform rectTransform = buttonObject.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(200, 50);
            
            // Buttonコンポーネントを追加
            Button button = buttonObject.AddComponent<Button>();
            
            // Imageコンポーネントを追加（Buttonの背景）
            UnityEngine.UI.Image image = buttonObject.AddComponent<UnityEngine.UI.Image>();
            image.color = new Color(0.2f, 0.6f, 1f, 1f); // 青色
            
            // Textコンポーネントを追加（Buttonのテキスト）
            GameObject textObject = new GameObject("Text");
            textObject.transform.SetParent(buttonObject.transform, false);
            RectTransform textRectTransform = textObject.AddComponent<RectTransform>();
            textRectTransform.anchorMin = Vector2.zero;
            textRectTransform.anchorMax = Vector2.one;
            textRectTransform.sizeDelta = Vector2.zero;
            textRectTransform.anchoredPosition = Vector2.zero;
            
            UnityEngine.UI.Text text = textObject.AddComponent<UnityEngine.UI.Text>();
            text.text = "Click Me";
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = 20;
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleCenter;
            
            // ButtonのtargetGraphicを設定
            button.targetGraphic = image;
            
            UnityEngine.Debug.Log("Buttonを作成しました");
        }
        
        // シーンを保存
        EditorSceneManager.MarkSceneDirty(currentScene);
        EditorSceneManager.SaveScene(currentScene);
        
        UnityEngine.Debug.Log("Hello Worldシーンのセットアップが完了しました");
    }
}
#endif

