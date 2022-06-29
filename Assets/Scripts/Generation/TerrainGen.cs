using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerrainGen : MonoBehaviour {
    [SerializeField] private int chunkSize;
    [SerializeField] private int maxBlockSize;
    [SerializeField] private int roughness;
    [SerializeField] private int enemyAmount;

    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject CubePrefab;
    [SerializeField] private GameObject ChunkPrefab;
    [SerializeField] private GameObject DebugPrefab;

    [SerializeField] private GameObject player;

    private Vector2Int currentChunk;
    private bool cooldown = true;
    private bool generateMesh = false;

    [HideInInspector] public Dictionary<Vector2Int, GameObject> chunks = new();

    [HideInInspector] public GameObjectPool ChunkPool;
    [HideInInspector] public GameObjectPool CubePool;
    [HideInInspector] public GameObjectPool EnemyPool;
    [HideInInspector] public GameObjectPool DebugPool;

    private void Start() {
        ChunkPool = new(ChunkPrefab);
        CubePool = new(CubePrefab);
        EnemyPool = new(EnemyPrefab);
        DebugPool = new(DebugPrefab);

        CreateFirstTile();
        StartCoroutine(Cooldown(chunkSize / 20));
    }

    private void Update() {
        if (generateMesh) {
            GetComponent<NavMeshSurface>().BuildNavMesh();
            generateMesh = false;

            foreach (var chunk in chunks.Values) {
                StartCoroutine(SpawnEnemies(chunk));
            }
        }

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
            StartCoroutine(Cooldown(chunkSize / 10));
        }
    }

    private void CreateFirstTile() {
        StartCoroutine(CreateChunk(Vector2Int.zero));
        currentChunk = Vector2Int.zero;

        GenerateNeighbours();
    }

    private void GenerateNeighbours() {
        List<Vector2Int> loadedList = new();

        loadedList.Add(currentChunk);

        //check neighbours for tile existance
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0)
                    continue;

                Vector2Int neighbour = new(currentChunk.x + x, currentChunk.y + y);

                //generate new tile if there isn't one
                if (!chunks.ContainsKey(neighbour)) {
                    StartCoroutine(CreateChunk(new Vector2Int((currentChunk.x + x) * (chunkSize * 2), (currentChunk.y + y) * (chunkSize * 2))));
                }

                loadedList.Add(neighbour);
            }
        }

        StartCoroutine(RemoveTiles(loadedList));
    }

    private IEnumerator RemoveTiles(List<Vector2Int> stayList) {
        //list of tiles to actually be deleted
        List<Vector2Int> toBeDeletedTiles = new();

        //get all tiles
        foreach (Vector2Int pos in chunks.Keys) {
            toBeDeletedTiles.Add(pos);
        }

        //remove tiles that need to stay from the list
        for (int i = 0; i < stayList.Count; i++) {
            if (toBeDeletedTiles.Contains(stayList[i])) {
                toBeDeletedTiles.Remove(stayList[i]);
            }
        }

        //remove one chunk every frame - probably most intensive atm
        var count = toBeDeletedTiles.Count;
        for (int i = 0; i < count; i++) {
            for (int j = chunks[toBeDeletedTiles[0]].transform.childCount; j > 1; j--) {
                chunks[toBeDeletedTiles[0]].transform.GetChild(1).gameObject.SetActive(false);
                CubePool.ReturnObjectToInactivePool(chunks[toBeDeletedTiles[0]].transform.GetChild(1).gameObject);
                chunks[toBeDeletedTiles[0]].transform.GetChild(1).parent = null;
                yield return new WaitForEndOfFrame();
            }

            chunks[toBeDeletedTiles[0]].SetActive(false);
            ChunkPool.ReturnObjectToInactivePool(chunks[toBeDeletedTiles[0]]);

            chunks.Remove(toBeDeletedTiles[0]);
            toBeDeletedTiles.RemoveAt(0);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CreateChunk(Vector2Int _StartingPos) {
        var Chunk = ChunkPool.GetObjectFromPool();
        Chunk.transform.parent = transform;

        //move chunk to final position
        Chunk.transform.localPosition = new Vector3(_StartingPos.x, 0, _StartingPos.y);

        //Add chunk to list of chunks
        chunks.Add(_StartingPos / (chunkSize * 2), Chunk);

        //Ground Plane First
        GameObject plane = Chunk.transform.GetChild(0).gameObject; 
        float halfSize = chunkSize / 5;
        plane.transform.localPosition = new Vector3(0, 1, 0);
        plane.transform.localScale = new Vector3(halfSize, 1, halfSize);

        //grid defining
        List<Vector2Int> gridPositions = new();
        List<Vector2Int> takenPositions = new();

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

            yield return new WaitForEndOfFrame();

            GameObject tmp = SpawnCube((_StartingPos / 2) + gridPositions[i], Chunk, tmpSize);

            for (int X = 0; X < tmpSize; X++) {
                for (int Y = 0; Y < tmpSize; Y++) {
                    takenPositions.Add(new Vector2Int(gridPositions[i].x + X, gridPositions[i].y + Y));
                }
            }
        }
    }

    private GameObject SpawnCube(Vector2Int _spawnPos, GameObject parent, float _tmpSize) {
        //Create and position Cube
        var tmp = CubePool.GetObjectFromPool();
        tmp.transform.parent = parent.transform;
        tmp.transform.position = CalcWorldPos(_spawnPos, _tmpSize * 2);

        //change size to predefined float
        Vector3 newSize = new Vector3(_tmpSize, _tmpSize, _tmpSize) * 2;
        tmp.transform.localScale = newSize;

        //change color to lighter as the cube is bigger
        float colorChange = _tmpSize / 50;
        tmp.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(colorChange, colorChange, colorChange, 1));

        if (tmp.GetComponent<NavMeshObstacle>() == null)
            tmp.AddComponent<NavMeshObstacle>().carving = true;

        return tmp;
    }

    private IEnumerator SpawnEnemies(GameObject Chunk) {
        for (int i = 0; i < enemyAmount; i++) {
            var tmp = EnemyPool.GetObjectFromPool();
            var tmpScript = tmp.GetComponent<AIController>();

            tmpScript.Init(this);
            tmpScript.player = player;
            tmp.transform.position = tmpScript.SetRandomDestination(Chunk.transform.position, chunkSize);

            yield return new WaitForEndOfFrame();
        }
    }

    public void DespawnEnemy(GameObject enemy) {
        enemy.GetComponent<AIController>().OnDisableObject();
        EnemyPool.ReturnObjectToInactivePool(enemy);
    }

    IEnumerator Cooldown(float _seconds) {
        yield return new WaitForSeconds(_seconds / 2);
        generateMesh = true;
        yield return new WaitForSeconds(_seconds / 2);
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