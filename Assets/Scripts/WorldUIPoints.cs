using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WorldUIPoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    Transform playerCamera;
    public void SetPoints(int points, int team)
    {
        if (team == 1)
        {
            pointsText.color = Color.yellow;

        }
        else if (team == 2)
        {
            pointsText.color = new Color(221,160,221);
        }
        pointsText.text = $"+{points.ToString()}";

        transform.DOMoveY(transform.position.y + 3f, 2f);
        
        playerCamera = FindObjectsOfType<PlayerTeam>().First(p => p.Team == team).GetComponentInChildren<Camera>().transform;
        
        StartCoroutine(FadeAfterTime(1.5f));
    }
    IEnumerator FadeAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        pointsText.DOFade(0, .5f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void Update()
    {
        if (playerCamera == null) return;
        transform.LookAt(playerCamera);
    }
}
