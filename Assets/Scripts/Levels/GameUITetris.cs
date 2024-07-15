using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LevelUnlockSystem
{
    public class GameUITetris : MonoBehaviour
    {
        [SerializeField] public GameObject blockPrefab;
        [SerializeField] public Sprite[] blockSprite;
        [SerializeField] private Image[] starsArray;
        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject gameOverPanel; // Panel de "Game Over"
        [SerializeField] private TextMeshProUGUI[] levelStatusText; // Arreglo para almacenar los textos hijos del objeto padre
        [SerializeField] private Color lockColor, unlockColor;  //ref to colors
        public struct Block
        {
            public int x;
            public int y;
            public GameObject ob;

            public Block(int x, int y, GameObject ob)
            {
                this.x = x;
                this.y = y;
                this.ob = ob;
            }
        }

        public Block[] piece = new Block[4]
        {
            new Block(),
            new Block(),
            new Block(),
            new Block()
        };

        public int W = 10;
        public int H = 20;
        public Block[,] block;

        public int[,] shapes = new int[,]
        {
            {1,3,5,7}, //I
            {2,4,5,7}, //Z
            {3,4,5,6}, //S
            {3,4,5,7}, //T
            {2,3,5,7}, //L
            {3,5,6,7}, //J
            {2,3,4,5}, //O
        };

        public float moveTime = 0;
        public float moveSpeed = 0.06f;
        public float time = 0;
        public float dropSpeed = 0.4f;
        public int pointsPerLine = 100;
        private int totalPoints = 0;

        void Start()
        {
            Time.timeScale = 1f; // Restablecer la velocidad del juego al valor normal
            block = new Block[W, H];
            Generate();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                Hold(-1, 0);
            else if (Input.GetKey(KeyCode.RightArrow))
                Hold(1, 0);
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                Rotate();   //rotate
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                dropSpeed = 0.05f;

            time += Time.deltaTime;
            if (time > dropSpeed)
            {
                if (!Move(0, -1))
                {
                    for (int i = 0; i < 4; i++)
                        block[piece[i].x, -piece[i].y] = piece[i];
                    Generate();
                    Clear();
                }
                time = 0;
            }
        }

        public void Generate()
        {
            dropSpeed = 0.4f;
            int n = Random.Range(0, shapes.GetLength(0));
            for (int i = 0; i < 4; i++)
            {
                piece[i].x = shapes[n, i] % 2;
                piece[i].y = -shapes[n, i] / 2;
            }

            // Verificar si hay algún bloque en la parte superior del área de juego
            if (CheckGameOver())
            {
                GameOver();
                return;
            }

            Sprite sprite = blockSprite[Random.Range(0, blockSprite.Length)];
            for (int i = 0; i < 4; i++)
            {
                piece[i].ob = Instantiate(blockPrefab, new Vector2(piece[i].x, piece[i].y), Quaternion.identity);
                SpriteRenderer sr = piece[i].ob.GetComponent<SpriteRenderer>();
                sr.sprite = sprite;
            }
            Move(4, 0);
        }

        private bool CheckGameOver()
        {
            for (int i = 0; i < W; i++)
            {
                if (block[i, 0].ob != null)
                {
                    return true;
                }
            }
            return false;
        }

        private void GameOver()
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        public void Hold(int dx, int dy)
        {
            moveTime += Time.deltaTime;
            if (moveTime > moveSpeed)
            {
                Move(dx, dy);
                moveTime = 0;
            }
        }

        public bool Move(int dx, int dy)
        {
            Block[] origin = piece.Clone() as Block[];
            for (int i = 0; i < 4; i++)
            {
                piece[i].x += dx;
                piece[i].y += dy;
            }

            return CheckAndSet(origin);
        }

        public void Rotate()
        {
            Block[] origin = piece.Clone() as Block[];
            Block p = piece[1]; //center of rotation
            for (int i = 0; i < 4; i++)
            {
                int x = piece[i].y - p.y;
                int y = piece[i].x - p.x;
                piece[i].x = p.x - x;
                piece[i].y = p.y + y;
            }
            CheckAndSet(origin);
        }

        private bool CheckAndSet(Block[] ori)
        {
            bool set = true;
            for (int i = 0; i < 4; i++)
            {
                if (piece[i].x < 0 || piece[i].x >= W || piece[i].y <= -H)
                    set = false;
                else if (block[piece[i].x, -piece[i].y].ob != null)
                    set = false;
            }
            if (set)
                for (int i = 0; i < 4; i++)
                    piece[i].ob.transform.position = new Vector2(piece[i].x, piece[i].y);
            else
                piece = ori;
            return set;
        }

        public void Clear()
        {
            List<Block> blockToClear = new List<Block>();
            int k = H - 1;
            int dy = 0;
            for (int i = H - 1; i > 0; i--)
            {
                blockToClear.Clear();
                int count = 0;
                for (int j = 0; j < W; j++)
                {
                    if (block[j, i].ob != null)
                        count++;

                    block[j, i].y += dy;
                    blockToClear.Add(block[j, i]);
                    block[j, k] = block[j, i];
                }
                if (count < W)
                    k--;
                else
                {
                    dy += -1;
                    for (int n = 0; n < blockToClear.Count; n++)
                        Destroy(blockToClear[n].ob);
                    LineCompleted(); // Llamar al método LineCompleted cuando se complete una línea                    
                }

                for (int j = 0; j < W; j++) //
                {
                    if (block[j, i].ob != null)
                        block[j, i].ob.transform.position = new Vector2(block[j, i].x, block[j, i].y);
                }
            }
        }

        void LineCompleted()
        {
            totalPoints += pointsPerLine;
            if (pointsPerLine >= 1)
            {
                Debug.Log($"HAS GANADO {totalPoints} PUNTOS");

                // Cambiar el texto de todos los elementos en levelStatusText
                foreach (var textElement in levelStatusText)
                {
                    textElement.text = "Level Complete " + (LevelSystemManager.Instance.CurrentLevel + 1);
                }

                LevelSystemManager.Instance.LevelComplete(3);   //send the information to LevelSystemManager    
                SetStar(3);                                 //set the stars

                panel.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        private void SetStar(int starAchieved)
        {
            Debug.Log(" stars " + starAchieved);
            for (int i = 0; i > starsArray.Length - 1; i++) //loop through entire star array
            {
                Debug.Log(" # " + i);
                // if i is less than starAchieved
                // Eg: if 2 stars are achieved we set the start at index 0 and 1 color to unlockColor, as array start from 0 element
                if (i < starAchieved)
                {
                    starsArray[i].color = unlockColor; //set its color to unlockColor
                    Debug.Log(" unlockColor " + unlockColor);
                }
                else
                {
                    starsArray[i].color = lockColor; //else set its color to lockColor
                    Debug.Log(" lockColor " + lockColor);
                }
            }
        }

        public void OkBtn() //method called by ok button
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Levels");
        }

    }
}
