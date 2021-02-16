using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIExtendComponent : MonoBehaviour
{
    [Header("UI Data")]
    [SerializeField] public TextMeshProUGUI txt;
    [SerializeField] public RectTransform rect;
    [SerializeField] public Image img;
    [SerializeField] public CanvasGroup canvasGroup;
    [SerializeField] public Button button;
    [SerializeField] public PointerEventsController pointerEvent;
}
