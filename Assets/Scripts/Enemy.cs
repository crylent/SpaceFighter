using System;
using UnityEngine;

public class Enemy : SpaceShip
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material outlineMaterial;

    private Renderer _renderer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _renderer = GetComponent<Renderer>();
    }

    public void SetThreatened(bool isThreatened = true)
    {
        _renderer.material = isThreatened ? outlineMaterial : defaultMaterial;
    }
}
