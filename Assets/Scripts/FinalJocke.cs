using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalJocke : MonoBehaviour {

    public Transform player;
    public SceneFinalManager sceneManager;
    public SpriteRenderer playerSprite;
	// Update is called once per frame
	void Update () {
		if(transform.localScale.x > 1f)
        {
            player.localScale = new Vector3(2f, 2f, 2f);
            player.position = new Vector3(-0.7f, -5.91f, player.position.z);
            sceneManager.wrongAnswer = true;
            sceneManager.quizGameDone = true;
            playerSprite.sortingOrder = 10;
        }
	}
}
