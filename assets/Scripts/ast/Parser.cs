using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace first.ast
{
    public class Parser : MonoBehaviour
    {
        private static readonly string _NUM = "[0-9]+";
        private static readonly string _ITEMTYPE = "bomb|gold";
        
        private readonly Tokenizer _tokenizer;
        private Maze _maze;

        public GameObject mazePrefab;
        //private mapMaker script;

        public static int row;
        public static int col;
        public static Vector2 startP;
        public static Vector2 finishP;
        public static List<Vector2> bombLocations = new List<Vector2>();
        public static List<Vector2> goldLocations = new List<Vector2>();
        public static List<List<Vector3>> enemies = new List<List<Vector3>>();
        public static List<List<Vector3>> paths = new List<List<Vector3>>();
        public GameObject inputBox;

        public void Compile()
        {
            //Debug.Log()
            DSLTokenizer t = new DSLTokenizer(inputBox.GetComponent<InputField>().text);
            //DSLTokenizer t = new DSLTokenizer("createMap (5,5); setStart (0,0); setFinish (4,4); placeItem (gold, [(1,2)]); placeItem (bomb, [(1,1),(2,2)]); placeEnemy [(2,3)-(3,3)]; drawPath [(0,4)-(4,4)];");
            Parser p = new Parser(t);
            Maze m = p.ParseInput();

            //GameObject map = Instantiate(mazePrefab, Vector3.zero, Quaternion.identity);
            //DontDestroyOnLoad(map);
            //script = map.GetComponent<mapMaker>();

            for (int i = 0; i < m.getItems().Count; i++ )
            {
                if (m.getItems()[i].GetItemType() == "bomb")
                {
                    bombLocations.Add(new Vector2(m.getItems()[i].GetLocation().x, m.getItems()[i].GetLocation().y));
                }
                if (m.getItems()[i].GetItemType() == "gold")
                {
                    goldLocations.Add(new Vector2(m.getItems()[i].GetLocation().x, m.getItems()[i].GetLocation().y));
                }
            }
            for (int i = 0; i < m.getEnemies().Count; i++)
            {
                List<Vector3> patrolPath = new List<Vector3>();
                for (int j = 0; j < m.getEnemies()[i].GetPatrolPath().Count; j++)
                {
                    patrolPath.Add(new Vector3(m.getEnemies()[i].GetPatrolPath()[j].x, m.getEnemies()[i].GetPatrolPath()[j].y, 0));
                }
                enemies.Add(patrolPath);
            }
            for (int i = 0; i < m.getPaths().Count; i++)
            {
                List<Vector3> path = new List<Vector3>();
                for (int j = 0; j < m.getPaths()[i].Count; j++)
                {
                    path.Add(new Vector3(m.getPaths()[i][j].x, m.getPaths()[i][j].y, 0));
                }
                paths.Add(path);
            }
            row = m.getDimensions().x;
            col = m.getDimensions().y;
            startP = new Vector2(m.getStart().x, m.getStart().y);
            finishP = new Vector2(m.getFinish().x, m.getFinish().y);
            SceneManager.LoadScene("game");
        }


        public static Parser GetParser(Tokenizer tokenizer)
        {
            return new Parser(tokenizer);
        }
        
        private Parser(Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        private Maze ParseInput()
        {
            _maze = new Maze();
            ParseCreateMap();
            while (_tokenizer.MoreTokens())
            {
                ParseStatement();
            }
            return _maze;
        }

        // STMT ::= CreateMap | DrawPath | PlaceItem | PlaceEnemy
        private void ParseStatement()
        {
            if (_tokenizer.CheckToken("drawPath"))
            {
                ParseDrawPath();
            }
            else if (_tokenizer.CheckToken("placeItem"))
            {
                ParsePlaceItem();
            }
            else if (_tokenizer.CheckToken("placeEnemy"))
            {
                ParsePlaceEnemy();
            }
            else
            {
                throw new Exception("Error: Unexpected token " + _tokenizer.GetNext());
            }
        }

        private void ParseCreateMap()
        {
            _tokenizer.GetAndCheckNext("createMap");
            _maze.SetDimensions(ParseCoord());
            _tokenizer.GetAndCheckNext(";");
            _maze.SetStart(ParseStart());
            _tokenizer.GetAndCheckNext(";");
            _maze.SetFinish(ParseFinish());
            _tokenizer.GetAndCheckNext(";");
        }
        
        private void ParseDrawPath()
        {
            Range range;
            _tokenizer.GetAndCheckNext("drawPath");
            range = ParseRange();
            _tokenizer.GetAndCheckNext(";");
            _maze.AddPaths(range.GetRange());
        }

        private Range ParseRange()
        {
            List<Coord> coords = new List<Coord>();
            _tokenizer.GetAndCheckNext("\\[");
            coords.Add(ParseCoord());
            while (_tokenizer.CheckToken("-"))
            {
                _tokenizer.GetAndCheckNext("-");
                coords.Add(ParseCoord());
            }
            _tokenizer.GetAndCheckNext("\\]");
            return new Range(coords);
        }
        
        private void ParsePlaceItem()
        {
            string itemType;
            List<Coord> coords;
            _tokenizer.GetAndCheckNext("placeItem");
            _tokenizer.GetAndCheckNext("\\(");
            itemType = ParseItemType();
            _tokenizer.GetAndCheckNext(",");
            coords = ParseCoordList();
            _tokenizer.GetAndCheckNext("\\)");
            _tokenizer.GetAndCheckNext(";");

            foreach (var coord in coords)
            {
                _maze.AddItem(new Item(itemType, coord));
            }
        }

        private List<Coord> ParseCoordList()
        {
            List<Coord> coords = new List<Coord>();
            _tokenizer.GetAndCheckNext("\\[");
            coords.Add(ParseCoord());
            while (_tokenizer.CheckToken(","))
            {
                _tokenizer.GetAndCheckNext(",");
                coords.Add(ParseCoord());
            }
            _tokenizer.GetAndCheckNext("\\]");
            return coords;
        }

        private string ParseItemType()
        {
            return _tokenizer.GetAndCheckNext(_ITEMTYPE);
        }
        
        private void ParsePlaceEnemy()
        {
            _tokenizer.GetAndCheckNext("placeEnemy");
            _maze.AddEnemy(new Enemy(ParseRange().GetRange()));
            _tokenizer.GetAndCheckNext(";");
        }

        private Coord ParseStart()
        {
            _tokenizer.GetAndCheckNext("setStart");
            return ParseCoord();
        }

        private Coord ParseFinish()
        {
            _tokenizer.GetAndCheckNext("setFinish");
            return ParseCoord();
        }

        private Coord ParseCoord()
        {
            int x, y;
            _tokenizer.GetAndCheckNext("\\(");
            x = int.Parse(_tokenizer.GetAndCheckNext(_NUM));
            _tokenizer.GetAndCheckNext(",");
            y = int.Parse(_tokenizer.GetAndCheckNext(_NUM));
            _tokenizer.GetAndCheckNext("\\)");
            return new Coord(x, y);
        }
    }
}