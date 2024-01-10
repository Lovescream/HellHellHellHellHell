using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene {

    public GameObject prefab1;
    public GameObject prefab2;

    protected override bool Initialize() {
        if (!base.Initialize()) return false;

        // GameScene�� �ʱ�ȭ �ڵ�.
        // Ex) Player ����
        // Ex) Map ����

        GameObject obj1 = Main.Pool.Pop(prefab1);
        GameObject obj2 = Main.Pool.Pop(prefab1);
        GameObject obj3 = Main.Pool.Pop(prefab1);
        GameObject obj4 = Main.Pool.Pop(prefab1);
        GameObject obj5 = Main.Pool.Pop(prefab1);

        Main.Pool.Push(obj2);
        Main.Pool.Push(obj4);



        return true;
    }

}