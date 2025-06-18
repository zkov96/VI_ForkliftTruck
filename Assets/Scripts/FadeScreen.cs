using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeScreen : MonoBehaviour
{

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.color = Color.black;
    }

    private void Start()
    {
        DOTween.ToAlpha(() => _image.color, value => _image.color = value, 0, 2f).SetDelay(1);
    }
}