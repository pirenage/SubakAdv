using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsTile
{
    public int growTimer;
    public int GrowStage;
    public crop Crop;
    public SpriteRenderer renderer;
    public float damage;
    public Vector3Int position;

    public bool Complete
    {
        get
        {
            if (Crop == null) { return false; }
            return growTimer >= Crop.timeToGrow;
        }
    }

    internal void Harvested()
    {
        growTimer = 0;
        GrowStage = 0;
        Crop = null;
        renderer.gameObject.SetActive(false);
        damage = 0;
    }
}


public class CropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] GameObject cropsSpritePrefab;

    Dictionary<Vector2Int, CropsTile> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropsTile>();
        onTimeTick += Tick;
        init();
    }

    public void Tick()
    {
        foreach (CropsTile cropsTile in crops.Values)
        {
            if (cropsTile.Crop == null) { continue; }

            cropsTile.damage += 0.02f;

            if (cropsTile.damage > 1f)
            {
                cropsTile.Harvested();
                targetTilemap.SetTile(cropsTile.position, plowed);
                continue;
            }

            if (cropsTile.Complete)
            {
                Debug.Log("I'm done Grow");
                continue;
            }

            cropsTile.growTimer += 1;

            if (cropsTile.growTimer >= cropsTile.Crop.GrowthStageTime[cropsTile.GrowStage])
            {
                cropsTile.renderer.gameObject.SetActive(true);
                cropsTile.renderer.sprite = cropsTile.Crop.sprites[cropsTile.GrowStage];

                cropsTile.GrowStage += 1;
            }


        }
    }

    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }

    public void plow(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
        {
            return;
        }
        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, crop toSeed)
    {
        targetTilemap.SetTile(position, seeded);
        crops[(Vector2Int)position].Crop = toSeed;
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        CropsTile crop = new CropsTile();
        crops.Add((Vector2Int)position, crop);

        GameObject go = Instantiate(cropsSpritePrefab);
        go.transform.position = targetTilemap.CellToWorld(position);
        go.transform.position = Vector3.forward * 0.01f;
        go.SetActive(false);
        crop.renderer = go.GetComponent<SpriteRenderer>();

        crop.position = position;

        targetTilemap.SetTile(position, plowed);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        if (crops.ContainsKey(position) == false) { return; }

        CropsTile cropsTile = crops[position];
        if (cropsTile.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), cropsTile.Crop.yield, cropsTile.Crop.count);
            targetTilemap.SetTile(gridPosition, plowed);
            cropsTile.Harvested();
        }
    }
}
