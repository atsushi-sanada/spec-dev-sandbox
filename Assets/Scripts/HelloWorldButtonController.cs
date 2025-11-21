using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ボタンクリック時に「Hello World」をログ出力するコントローラー
/// </summary>
public class HelloWorldButtonController : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    private void Awake()
    {
        // Buttonコンポーネントの取得
        if (_button == null)
        {
            _button = GetComponent<Button>();
        }
    }
    
    private void OnEnable()
    {
        // ボタンクリックイベントの登録
        if (_button != null)
        {
            _button.onClick.AddListener(OnButtonClicked);
        }
    }
    
    private void OnDisable()
    {
        // ボタンクリックイベントの解除
        if (_button != null)
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }
    }
    
    /// <summary>
    /// ボタンがクリックされた際の処理
    /// </summary>
    private void OnButtonClicked()
    {
        UnityEngine.Debug.Log("Hello World");
    }
}

