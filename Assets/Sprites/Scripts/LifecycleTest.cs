using UnityEngine;

public class LifecycleTest : MonoBehaviour
{
    int updates, fixedUpdates;

    void Awake() => Debug.Log("1. Awake");
    void OnEnable() => Debug.Log("2. OnEnable");
    void Start() => Debug.Log("3. Start");
    void FixedUpdate() { if (fixedUpdates++ < 3) Debug.Log("FixedUpdate " + fixedUpdates); }
    void Update() { if (updates++ < 3) Debug.Log("Update " + updates); }
    void LateUpdate() { if (updates <= 3) Debug.Log("LateUpdate"); }
    void OnDisable() => Debug.Log("OnDisable");
    void OnDestroy() => Debug.Log("OnDestroy");
}