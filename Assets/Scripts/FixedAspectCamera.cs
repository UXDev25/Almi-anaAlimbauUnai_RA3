using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedAspectCamera : MonoBehaviour
{
    [SerializeField] private Vector2 targetAspect = new Vector2(16f, 9f);
    private Camera cam;
    private int lastW, lastH;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        Apply();
    }

    private void Update()
    {
        if (Screen.width != lastW || Screen.height != lastH) Apply();
    }

    private void Apply()
    {
        lastW = Screen.width;
        lastH = Screen.height;

        float target = targetAspect.x / targetAspect.y;
        float window = (float)Screen.width / Screen.height;
        float scale = window / target;

        if (scale < 1f)
        {
            cam.rect = new Rect(0f, (1f - scale) / 2f, 1f, scale);
        }
        else 
        {
            float w = 1f / scale;
            cam.rect = new Rect((1f - w) / 2f, 0f, w, 1f);
        }
    }
}
