using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPointer : MonoBehaviour
{
    // Start is called before the first frame update

    public OVRHand rightHand;
    public GameObject CurrentTarget { get; private set; }

    [SerializeField] private bool showRaycast = true;
    [SerializeField] private Color highlightColor = Color.red;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LineRenderer lineRenderer;

    private Color _originalColor;
    private Renderer _currentRenderer;

    void Update() => CheckHandPointer(rightHand);

    void CheckHandPointer(OVRHand hand) {
        if (Physics.Raycast(hand.PointerPose.position, hand.PointerPose.forward, out RaycastHit hit, Mathf.Infinity, targetLayer)){
            if (CurrentTarget != hit.transform.gameObject){
                CurrentTarget = hit.transform.gameObject;
                _currentRenderer = CurrentTarget.GetComponent<Renderer>();
                _originalColor = _currentRenderer.material.color;
                _currentRenderer.material.color = highlightColor;
            }

            UpdateRayVisual(hand.PointerPose.position, hit.point, true);
        } else {
            if (CurrentTarget != null){
                _currentRenderer.material.color = _originalColor;
                CurrentTarget = null;
            }
            UpdateRayVisual(hand.PointerPose.position, hand.PointerPose.position + hand.PointerPose.forward * 1000, false);
        }
    }

    void UpdateRayVisual(Vector3 startPoint, Vector3 endPoint, bool hitted){
       if (showRaycast && lineRenderer != null) {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
            lineRenderer.material.color = hitted ? Color.green : Color.red;
        }
       else if (lineRenderer != null) lineRenderer.enabled = false;
    }
}
