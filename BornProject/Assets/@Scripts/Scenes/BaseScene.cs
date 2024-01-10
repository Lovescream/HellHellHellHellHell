using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour {

    private bool _initialized;

    void Start() {
        Initialize();
    }

    protected virtual bool Initialize() {
        if (_initialized) return false;
        // ���� �ʱ�ȭ �Լ�.
        // DataManager �ʱ�ȭ
        // GameManager �ʱ�ȭ

        _initialized = true;
        return true;
    }

}