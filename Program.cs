using System;
//using System.Text;
using System.Threading;

class Program
{
    //GLOBAL VARIABLES

    public static int sizeGameField = 20; //размер поля
    public static int[] snakeBody = new int[sizeGameField]; //матрица длины тела змейки
    public static int snakeTotalLengthBody = sizeGameField; //максимальная длина змейки
    public static int snakeLengthBody; //длина тела змейки
    public static int[,] gameField = new int[sizeGameField + 2, sizeGameField + 2]; //матрица игрового поля
    public static int head_X, head_Y; //координаты головы Змейки
    public static int scorePoints;
    public static int segment_X, segment_Y; //временные координаты сегментов Змейки при сдвиге этих сегментов
    public static bool isFood = false; //проверка, есть ли еда
    public static int food_X, food_Y; //координаты еды
    public static Random rand = new Random(); //рандомайзер
    public static Thread backgroundGame = new Thread(backgroundSnake); //инициализация второга потока
    public static string directionKeyTemp = "UpArrow"; // направление движения головы по-умолчанию

    //MAIN PROGRAMM
    static void Main(string[] args)
    {
        Console.Title = "Snake in Console. v.1.0"; //Надпись вверху консольного заглавия
        Console.SetWindowSize(Console.WindowWidth, 30); //задать размер консольного окна




        startTheGame();

        newGame();

        backgroundGame.Start();
        backgroundGame.IsBackground = true;
        //backgroundSnake();    //не нужно, фоновый поток работает сам

        inputMoving();
        //DrawingGame();
        pauseBeforeExiting();



    }


    //FUNCTIONS

    static void newGame()
    {
        snakeLengthBody = 3;
        head_X = 0;
        head_Y = 0;
        directionKeyTemp = "UpArrow";
        scorePoints = 0;

        initializingEmptyField();

        intializationSnakeBody();

        InitializationSnake2gameField();

        generatingFood();
    }

    static void DrawingGame() //прорисовка игрового "кадра"
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("+");
        for (int i = 1; i < sizeGameField; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine("+");
        Console.ForegroundColor = ConsoleColor.White;

        for (int j = 1; j < sizeGameField; j++)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("|");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 1; i < sizeGameField; i++)
            {
                if (gameField[i, j] == 0)
                {
                    Console.Write(" ");
                }
                if (gameField[i, j] > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    //Console.Write(gameField[i, j]); //временная замена изображения змейки, для дебага
                    Console.Write("o");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (gameField[i, j] == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("@");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("|");
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("+");
        for (int charLine = 1; charLine < sizeGameField; charLine++)
        {
            Console.Write("-");
        }
        Console.WriteLine("+");
        Console.ForegroundColor = ConsoleColor.White;

        debugGame();
    }

    static void debugGame() //ф-ция просмотра текущих значений во время игры
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("  Lenght of Snake: {0}", snakeLengthBody);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nDebug:");
        Console.WriteLine("directionKeyTemp = {0}", directionKeyTemp);
        Console.WriteLine("head_X = {0}, head_Y = {1}", head_X, head_Y);
        Console.WriteLine("snakeLengthBody = {0}", snakeLengthBody);
        Console.WriteLine("isFood = {0}, food_X = {1}, food_Y = {2}", isFood, food_X, food_Y);
    }

    static void pauseBeforeExiting()
    {
        Console.WriteLine("Size of Game Field: {0}", sizeGameField);
        Console.WriteLine("The size of Snake: {0}", snakeTotalLengthBody);
        Console.WriteLine("Coordinates of Snake head: X = {0}, Y = {1}.", head_X, head_Y);
        Console.ReadLine();
        Console.Clear();
    }

    static void initializingEmptyField()
    {
        for (int i = 0; i <= sizeGameField + 1; i++) // заполнение границ поля большими значениями
        {
            gameField[i, 0] = 999;
            gameField[0, i] = 999;
            gameField[i, sizeGameField + 1] = 999;
            gameField[sizeGameField, i] = 999;
        }

        for (int i = 1; i <= sizeGameField; i++)
        {
            for (int j = 1; j <= sizeGameField; j++)
            {
                gameField[i, j] = 0;
            }
        }
    }

    static void intializationSnakeBody()
    {
        for (int i = 1; i <= snakeLengthBody; i++)
        {
            snakeBody[i - 1] = i;
        }
        for (int i = snakeLengthBody + 1; i < snakeTotalLengthBody; i++)
        {

        }
    }

    static void InitializationSnake2gameField()
    {
        head_Y = sizeGameField - snakeLengthBody;
        head_X = sizeGameField / 2;

        for (int i = 0; i < snakeLengthBody; i++)
        {
            gameField[sizeGameField / 2, sizeGameField - snakeLengthBody + i] = snakeBody[i];
        }
    }

    static void startTheGame()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("     ***************");
        Console.Write("     *****");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Snake");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("*****");
        Console.WriteLine("     ***************");
        Thread.Sleep(1000); //задержка потока в милисекундах
        Console.WriteLine("     ***************");
        Console.Write("     ***");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("The Game");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("****");
        Console.WriteLine("     ***************");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadLine();
        Console.Clear();
    }

    static void GameOver()
    {
        backgroundGame.Abort();//прерываем поток
        backgroundGame.Join(300);//таймаут на завершение
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine();
        Console.WriteLine("     +-----------+");
        Console.WriteLine("     | GAME OVER |");
        Console.WriteLine("     +-----------+");
        Console.ReadLine();
        Environment.Exit(0);  //закрытие консольной программы

    }

    static void inputMoving()
    {
        directionKeyTemp = "UpArrow";

        while (true)
        {

            ConsoleKeyInfo presskey = new ConsoleKeyInfo();
            presskey = Console.ReadKey();

            //выбор направления через нажатия
            if (presskey.Key.ToString() == "LeftArrow" && directionKeyTemp != "RightArrow")
            {
                if (gameField[head_X - 1, head_Y] > 0)
                {
                    GameOver();
                }
                directionKeyTemp = presskey.Key.ToString();
            }

            if (presskey.Key.ToString() == "RightArrow" && directionKeyTemp != "LeftArrow")
            {
                if (gameField[head_X + 1, head_Y] > 0)
                {
                    GameOver();
                }
                directionKeyTemp = presskey.Key.ToString();
            }

            if (presskey.Key.ToString() == "UpArrow" && directionKeyTemp != "DownArrow")
            {
                if (gameField[head_X, head_Y - 1] > 0)
                {
                    GameOver();
                }
                directionKeyTemp = presskey.Key.ToString();
            }

            if (presskey.Key.ToString() == "DownArrow" && directionKeyTemp != "UpArrow")
            {
                if (gameField[head_X, head_Y + 1] > 0)
                {
                    GameOver();
                }
                directionKeyTemp = presskey.Key.ToString();
            }
            if (presskey.Key.ToString() == "Escape")
            {
                directionKeyTemp = "Escape";
                Console.Clear();
                GameOver();
            }

            //directionKeyTemp = presskey.Key.ToString();

        } // конец WHILE
    } //конец функции inputMoving()

    static void Moving()
    {
        //eatingFood();

        if (directionKeyTemp == "UpArrow") //поворот головы в указаном игроком  направлении
        {
            head_Y--;
            isEmptyTile();
            isFoodTile();
            gameField[head_X, head_Y] = 1;
        }
        else
        {
            if (directionKeyTemp == "LeftArrow")
            {
                head_X--;
                isEmptyTile();
                isFoodTile();
                gameField[head_X, head_Y] = 1;
            }
            else
            {
                if (directionKeyTemp == "DownArrow")
                {
                    head_Y++;
                    isEmptyTile();
                    isFoodTile();
                    gameField[head_X, head_Y] = 1;
                }
                else
                {
                    if (directionKeyTemp == "RightArrow")
                    {
                        head_X++;
                        isEmptyTile();
                        isFoodTile();
                        gameField[head_X, head_Y] = 1;
                    }
                }
            }
        }

        segment_X = head_X;
        segment_Y = head_Y;
        // Подтягивание остальных сегментов Змейки
        for (int i = 2; i <= snakeLengthBody + 1; i++)
        {
            if (gameField[segment_X - 1, segment_Y] == gameField[segment_X, segment_Y])
            {
                segment_X--;
                gameField[segment_X, segment_Y]++;
            } 
            if (gameField[segment_X + 1, segment_Y] == gameField[segment_X, segment_Y])
            {
                segment_X++;
                gameField[segment_X, segment_Y]++;
            }
            if (gameField[segment_X, segment_Y - 1] == gameField[segment_X, segment_Y])
            {
                segment_Y--;
                gameField[segment_X, segment_Y]++;
            }
            if (gameField[segment_X, segment_Y + 1] == gameField[segment_X, segment_Y])
            {
                segment_Y++;
                gameField[segment_X, segment_Y]++;
            }
        }
        if (isFood == false)
        {
            gameField[segment_X, segment_Y] = snakeLengthBody;
        } else
        {
            gameField[segment_X, segment_Y] = 0;
        }



       


    } //конец фу-ции Moving()

    static void backgroundSnake() //(!!!) не работает/Работает даже если не запускать
    {
        while (true)
        {
            DrawingGame();
            Thread.Sleep(400);

            Moving();

            if (isFood == false)
            {
                generatingFood();
            }

        }
    }

    static void generatingFood()
    {
        food_X = rand.Next(2, sizeGameField);
        food_Y = rand.Next(2, sizeGameField);
        if (gameField[food_X, food_Y] > 0)
        {
            generatingFood();
        }
        gameField[food_X, food_Y] = -1;
        isFood = true;
    }

    static void eatingFood()
    {
        if (directionKeyTemp == "UpArrow" && gameField[head_X, head_Y - 1] == -1)
        {
            gameField[head_X, head_Y - 1] = 1;
            snakeLengthBody++;
            scorePoints++;
            isFood = false;

        }
        if (directionKeyTemp == "DownArrow" && gameField[head_X, head_Y + 1] == -1)
        {
            gameField[head_X, head_Y + 1] = 1;
            snakeLengthBody++;
            scorePoints++;
            isFood = false;
        }
        if (directionKeyTemp == "LeftArrow" && gameField[head_X - 1, head_Y] == -1)
        {
            gameField[head_X - 1, head_Y] = 1;
            snakeLengthBody++;
            scorePoints++;
            isFood = false;
        }
        if (directionKeyTemp == "RightArrow" && gameField[head_X + 1, head_Y] == -1)
        {
            gameField[head_X + 1, head_Y] = 1;
            snakeLengthBody++;
            scorePoints++;
            isFood = false;
        }
}

    static void isEmptyTile()
    {
        if (gameField[head_X, head_Y] > 0)
        {
            GameOver();
        }
    }

    static void isFoodTile()
    {
        if (gameField[head_X, head_Y] == -1)
        {
            snakeLengthBody++;
            scorePoints++;
            isFood = false;
        }
    }



} //конец функции Main()