using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene {

    protected override bool Initialize() {
        if (!base.Initialize()) return false;


        GameObject player = Main.Resource.Instantiate("Player", pooling: true);
        player.GetComponent<SpriteRenderer>().sprite = Main.Resource.LoadSprite("SilverAxe");

        return true;
    }

}