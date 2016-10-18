using System;

class Program
{
    //GLOBAL VARIABLES

    public static int sizeGameField = 20; //размер поля
    public static int[] snakeBody = new int[sizeGameField]; //массив для теля Змейки
    public static int snakeTotalLengthBody = sizeGameField; //максимальный размер Змейки
    public static int snakeLengthBody = 4; //текущий размер Змейки
    public static int[,] gameField = new int[sizeGameField, sizeGameField]; //массив значений поля
    public static int head_X = 0, head_Y = 0;

    public static string directionKey = "8";

    //MAIN PROGRAMM
    static void Main(string[] args)
    {
        Console.Title ="Snake in Console. v.1.0";

        initializingEmptyField();

        intializationSnakeBody();

        InitializationSnake2gameField();

        Moving();

    }


    //FUNCTIONS

    static void DrawingGame()
    {
        //Прорисовка верхней линии
        Console.Write("+");
        for (int i = 1; i <= sizeGameField; i++ )
        {
            Console.Write("-");
        }
        Console.WriteLine("+");

        //Прорисовка основного поля, левого и правого края
        for (int i = 0; i < sizeGameField; i++ )
        {
            Console.Write("|");
            for (int j = 0; j < sizeGameField; j++)
            {
                if (gameField[i, j] == 0)
                {
                    Console.Write(" ");
                }
                if (gameField[i, j] > 0)
                {
                    Console.Write("o");
                }
                if (gameField[i, j] == -1)
                {
                    Console.Write("%");
                }
                //Console.Write(gameField[i, j]);
            }
            Console.WriteLine("|");
        }
        //Прорисовка нижней линии
        Console.Write("+");
        for (int charLine = 1; charLine <= sizeGameField; charLine++)
        {
            Console.Write("-");
        }
        Console.Write("+");

        Console.WriteLine();


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
        for (int i = 0; i < sizeGameField; i++)
        {
            for (int j = 0; j < sizeGameField; j++ )
            {
                gameField[i, j] = 0;
            }
        }

    }

    static void intializationSnakeBody()
    {
        for (int i = 1; i <= snakeLengthBody; i++ )
        {
            snakeBody[i - 1] = i;
        }
        for (int i = snakeLengthBody + 1; i < snakeTotalLengthBody; i++ )
        {

        }
    }

    static void drawingSnakeBody()
    {
        for (int i = 0; i < snakeTotalLengthBody; i++)
        {
            Console.Write(snakeBody[i]);
        }
        Console.WriteLine();
    }
    
    static void InitializationSnake2gameField()
    {
        head_X = sizeGameField / 2;
        head_Y = sizeGameField - snakeLengthBody;

        for (int i = 0; i < snakeLengthBody; i++ )
        {
            gameField[sizeGameField - snakeLengthBody + i, sizeGameField / 2] = snakeBody[i];
        }
    }

    static void GameOver()
    {
        Console.Clear();
        Console.WriteLine("+-----------+");
        Console.WriteLine("| GAME OVER |");
        Console.WriteLine("+-----------+");
        Console.ReadLine();
        directionKey = "q";
    }

    static void Moving()
    {
        string directionKeyTemp = "UpArrow";
        while (true)
        {
            Console.Clear();
            DrawingGame();
            //directionKey = Console.ReadLine();
            ConsoleKeyInfo presskey = new ConsoleKeyInfo();
            presskey = Console.ReadKey(false);

            //Задать направление движения
            if (presskey.Key.ToString() == "LeftArrow" && directionKeyTemp != "RightArrow")
            {
                if (gameField[head_Y, head_X - 1] > 0)
                {
                    GameOver();
                }
                gameField[head_Y, head_X - 1] = gameField[head_Y, head_X];
                head_X--;
            }

            if (presskey.Key.ToString() == "RightArrow" && directionKeyTemp != "LaftArrow")
            {
                if (gameField[head_Y, head_X + 1] > 0)
                {
                    GameOver();
                }
                gameField[head_Y, head_X + 1] = gameField[head_Y, head_X];
                head_X++;
            }

            if (presskey.Key.ToString() == "UpArrow" && directionKeyTemp != "DownArrow")
            {
                if (gameField[head_Y - 1, head_X] > 0)
                {
                    GameOver();
                }
                gameField[head_Y - 1, head_X] = gameField[head_Y, head_X];
                head_Y--;
            }

            if (presskey.Key.ToString() == "DownArrow" && directionKeyTemp != "UpArrow")
            {
                if (gameField[head_Y + 1, head_X] > 0)
                {
                    GameOver();
                }
                gameField[head_Y + 1, head_X] = gameField[head_Y, head_X];
                head_Y++;
            }
            if (presskey.Key.ToString() == "Escape")
            {
                break;
            }

            directionKeyTemp = presskey.Key.ToString();

        } // закрывается WHILE
    } //закрывается фу-я Moving()

  

} //закрывается класс Main()


