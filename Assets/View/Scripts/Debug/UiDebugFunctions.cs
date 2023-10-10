using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CardGame.View.DebugSystem
{
    public static class UiDebugFunctions
    {
        public static void CreateDebugUIButton(Canvas c, UnityAction action, int priority = 0,
            string btnText = "Debug Button")
        {
            GameObject gameObject = new GameObject();
            gameObject.name = "DebugButton";
            gameObject.transform.SetParent(c.transform);

            UnityEngine.UI.Image img = gameObject.AddComponent<Image>();
            img.color = new Color(1, 1, 1, 0.647f);
            Button btn = gameObject.AddComponent<Button>();
            btn.onClick.AddListener(action);

            GameObject childObject = new GameObject();
            childObject.transform.SetParent(gameObject.transform);
            TextMeshProUGUI text = childObject.AddComponent<TextMeshProUGUI>();
            text.SetText(btnText);
            text.enableAutoSizing = true;
            text.alignment = TextAlignmentOptions.Center;
            text.color = new Color(0.3409f, 0.3962f, 0.3226f);

            RectTransform rectTransform = btn.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(60, -120 + (-90 * priority));
            rectTransform.sizeDelta = new Vector2(225f, 80f);
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 1);

            gameObject.transform.localScale = Vector3.one;
        }
    }
}