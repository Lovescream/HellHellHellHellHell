using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Entity {

    public CreatureData Data { get; protected set; }

    public float hpMax = 0;
    public float damage = 0;
    public float defense = 0;

    public float Hp {
        get => _hp;
        set {
            if (_hp == value) return;
            if (value <= 0) {
                _hp = 0;
                // TODO:: 죽었을 때 처리.
                Main.Resource.Destroy(this.gameObject);
            }
            else if (value >= hpMax) {
                _hp = hpMax;
            }
            else _hp = value;
            OnChangedHp?.Invoke(_hp);
        }
    }

    private float _hp;
    private SpriteRenderer _spriter;

    public event Action<float> OnChangedHp;

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            Hp -= 10;
        }
    }

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        OnChangedHp += OnChangeHp;

        _spriter = this.GetComponent<SpriteRenderer>();

        return true;
    }

    public void SetInfo(CreatureData data) {
        Initialize();

        Data = data;

        hpMax = data.HpMax;
        damage = data.Damage;
        defense = data.Defense;

        if (hpMax < 150) _spriter.sprite = Main.Resource.LoadSprite("SilverSword");

        Hp = hpMax;
    }

    private void OnChangeHp(float hp) {
        Debug.Log($"현재 Hp는: {hp}");
    }

}