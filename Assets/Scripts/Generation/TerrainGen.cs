using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour {
    [SerializeField] private int chunkSize;
    [SerializeField] private int maxBlockSize;
    [SerializeField] private int roughness;

    [SerializeField] private GameObject player;

    private Vector2Int currentChunk;
    private bool cooldown = true;

    [HideInInspector] public List<GameObject> cubes = new List<GameObject>();
    [HideInInspector] public Dictionary<Vector2Int, GameObject> chunks = new Dictionary<Vector2Int, GameObject>();

    private void Start() {
        CreateFirstTile();
        StartCoroutine(Cooldown());
    }

    private void Update() {
        if (player == null || cooldown)
            return;

        float closestDistance = Mathf.Infinity;
        Vector2Int closestTile = Vector2Int.zero;

        foreach (Vector2Int pos in chunks.Keys) {
            float dis = Vector3.Distance(player.transform.position, chunks[pos].transform.position);
            if (dis < closestDistance) {
                closestDistance = dis;
                closestTile = pos;
            }
        }

        if (closestTile != currentChunk) {
            currentChunk = closestTile;
            GenerateNeighbours();
            cooldown = true;
            StartCoroutine(Cooldown());
        }
    }

    private void CreateFirstTile() {
        StartCoroutine(CreateChunk(Vector2Int.zero));
        currentChunk = Vector2Int.zero;

        GenerateNeighbours();
    }

    private void GenerateNeighbours() {
        var loadedList = new List<Vector2Int>();

        loadedList.Add(currentChunk);

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0)
                    continue;

                Vector2Int neighbour = new Vector2Int(currentChunk.x + x, currentChunk.y + y);

                if (!chunks.ContainsKey(neighbour)) {
                    StartCoroutine(CreateChunk(new Vector2Int((currentChunk.x + x) * (chunkSize * 2), (currentChunk.y + y) * (chunkSize * 2))));
                }

                loadedList.Add(neighbour);
            }
        }

        StartCoroutine(RemoveTiles(loadedList));
    }

    private IEnumerator RemoveTiles(List<Vector2Int> stayList) {
        List<Vector2Int> toBeDeletedTiles = new List<Vector2Int>();

        foreach (Vector2Int pos in chunks.Keys) {
            toBeDeletedTiles.Add(pos);
        }

        for (int i = 0; i < stayList.Count; i++) {
            if (toBeDeletedTiles.Contains(stayList[i])) {
                toBeDeletedTiles.Remove(stayList[i]);
            }
        }

        var count = toBeDeletedTiles.Count;
        for (int i = 0; i < count; i++) {
            Destroy(chunks[toBeDeletedTiles[0]]);
            chunks.Remove(toBeDeletedTiles[0]);
            toBeDeletedTiles.RemoveAt(0);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CreateChunk(Vector2Int _StartingPos) {
        GameObject Chunk = new GameObject();
        Chunk.transform.parent = gameObject.transform;

        //Ground Plane First
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        float halfSize = chunkSize / 5;
        plane.transform.position = new Vector3(0, 1, 0);
        plane.transform.localScale = new Vector3(halfSize, 1, halfSize);

        plane.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);
        plane.transform.parent = Chunk.transform;

        List<Vector2Int> gridPositions = new List<Vector2Int>();
        List<Vector2Int> takenPositions = new List<Vector2Int>();

        //Allocate data for Positions
        for (int X = 0; X < chunkSize; X++) {
            for (int Y = 0; Y < chunkSize; Y++) {
                gridPositions.Add(new Vector2Int(X, Y));
            }
        }

        yield return new WaitForEndOfFrame();

        //Create Cubes
        for (int i = 0; i < gridPositions.Count; i++) {
            if (takenPositions.Contains(gridPositions[i])) {
                takenPositions.Remove(gridPositions[i]);
                continue;
            }

            int tmpSize = SkewedRandomRange(1, maxBlockSize + 1, maxBlockSize * roughness + 1);

            bool check = false;
            for (int X = 0; X < tmpSize; X++) {
                for (int Y = 0; Y < tmpSize; Y++) {
                    if (takenPositions.Contains(gridPositions[i] + new Vector2Int(X, Y)) || !gridPositions.Contains(gridPositions[i] + new Vector2Int(X, Y)))
                        check = true;
                }
            }

            if (check || tmpSize == 1)
                continue;

            GameObject tmp = SpawnCube(gridPositions[i], Chunk, tmpSize);
            yield return new WaitForEndOfFrame();

            for (int X = 0; X < tmpSize; X++) {
                for (int Y = 0; Y < tmpSize; Y++) {
                    cubes.Add(tmp);
                    takenPositions.Add(new Vector2Int(gridPositions[i].x + X, gridPositions[i].y + Y));
                }
            }
        }

        //move chunk to final position
        Chunk.transform.position = new Vector3(_StartingPos.x, 0, _StartingPos.y);

        //Add chunk to list of chunks
        chunks.Add(_StartingPos / (chunkSize * 2), Chunk);
    }

    private GameObject SpawnCube(Vector2Int _spawnPos, GameObject parent, float _tmpSize) {
        GameObject tmp = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tmp.transform.parent = parent.transform;
        tmp.transform.position = CalcWorldPos(_spawnPos, _tmpSize * 2);

        Vector3 newSize = new Vector3(_tmpSize, _tmpSize, _tmpSize) * 2;
        tmp.transform.localScale = newSize;

        float colorChange = _tmpSize / 50;
        tmp.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(colorChange, colorChange, colorChange, 1));

        return tmp;
    }

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(2);
        cooldown = false;
    }

    private Vector3 CalcWorldPos(Vector2Int _pos, float _blockSize) {
        float halfSize = _blockSize / 2;
        return new Vector3((_pos.x * 2) + halfSize - chunkSize, halfSize, (_pos.y * 2) + halfSize - chunkSize);
    }

    private int SkewedRandomRange(float _min, float _max, float _p) {
        return Mathf.RoundToInt(Mathf.Pow(Random.value, _p) * (_max - _min) + _min);
    }
}