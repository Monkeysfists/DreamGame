using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameLibrary
{
    class LevelReader
    {
        //Define Lists
        protected List<Add> Object1;
        protected List<Add> Object2;
        protected List<Add> Object3;
        protected List<Add> Object4;

        public LevelReader(int levelIndex, StreamReader reader)
        {
            //Make lists
            Object1 = new List<Add>();

            //Add Background

            //Read info about level.
            string levelTitle = reader.ReadLine();
            string[] stringList = reader.ReadLine().Split();
            int width = int.Parse(stringList[0]);
            int height = int.Parse(stringList[1]);

            //Add Playingfield

            //Read level.

            for (int row = 0; row < height; row++)
            {
                string currentRow = reader.ReadLine();
                for (int column = 0; column < width; column++)
                {
                    //Add TileEntity
                    switch (currentRow[column])
                    {
                        //Add cases

                    }

                }
            }
        }
            

            //Add MenuButton



            public bool Menu
        {
            get { return Menu; }
            set { Menu = value; }
        }
    }
}
