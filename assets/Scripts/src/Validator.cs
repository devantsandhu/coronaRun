using System;
using first.ast;
using System.Collections.Generic;

namespace first
{

    public class Validator
    {
        private readonly int max_height = 100;
        private readonly int max_width = 100;
        private Maze m;

        public Validator(Maze maze)
        {
            m = maze;
        }

        public bool Validate()
        {
            Coord dim = m.getDimensions();
            if (dim.x < 0 || dim.x > max_width || dim.y < 0 || dim.y > max_height)
            {
                Console.WriteLine("Invalid maze dimensions");
                return false;
            }

            Coord start = m.getStart();
            Coord finish = m.getFinish();

            if (start.equals(finish))
            {
                Console.WriteLine("Start can't be the same as finish");
                return false;
            }

            if (!inMaze(start) || !inMaze(finish))
            {
                Console.WriteLine("Start and finish have to be within maze");
                return false;
            }

            if (!validatePaths()) { return false; }
            if (!validateItems()) { return false; }
            if (!validateEnemies()) { return false;  }
            if (!solutionExists()) { return false; }

            return true;
        }

        private bool inMaze(Coord c)
        {
            return c.x >= 0 && c.y >= 0 && c.x <= max_width && c.y <= max_height;
        }

        private bool validatePaths()
        {
            //List<Coord> paths = m.getPaths();
            //int length = paths.Count;
            //if (length == 0 || length == 1)
            //{
            //    Console.WriteLine("There must be at least two coordinates for a path");
            //} 
            //for(int i = 0; i < length - 1; i++)
            //{
            //    Coord a = paths[i];
            //    Coord b = paths[i + 1];
            //    if (a.equals(b)) {
            //        Console.WriteLine("Path Coordinates must be distinct");
            //        return false;
            //    }
            //    if (!((a.x == b.x) || (a.y == b.y))) {
            //        Console.WriteLine("Path Coordinates must line up vertically or horizontally");
            //        return false;
            //    }
            //}
            return true;
        }

        private bool validateItems()
        {
            List<Item> items = m.getItems();
            for (int i = 0; i < items.Count; i++)
            {
                if(!inMaze(items[i].GetLocation()))
                {
                    Console.WriteLine("Item must be in maze");
                    return false;
                }


            }


            List<Coord> l = new List<Coord>();
            foreach (Item i in items)
            {
                l.Add(i.GetLocation());
            }

            //no two items can have the same coordinate
            if (!pointsDistinct(l))
            {
                Console.WriteLine("Items points must be distinct");
                return false;
            }
            return true;
        }

        private bool validateEnemies()
        {
            //two enemy patrol paths can't intersect
            
            //patrol path must be a valid maze path
            return true;
        }

        private bool solutionExists()
        {
            //player has at least one route from start to finish
            return true;
        }

        private bool validateOther()
        {
            //item coordinates can't intersect with enemy patrol path
            return true;
        }

        private bool pointsDistinct(List<Coord> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                for (int j = i + 1; j < l.Count; j++)
                {
                    if (l[i].equals(l[j]))
                    {
                        Console.WriteLine(l[i] + ", " + l[j]);
                        return false;
                    }
                }
            }
            return true;
        }
    }

}
