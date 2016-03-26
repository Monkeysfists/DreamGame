using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using TickTick.Entities.Buttons;
using TickTick.Entities.States;
using TickTick.Entities.Tiles;
using TickTick.Entities.Tiles.Creatures;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities
{
    public class LevelEntity : Entity
    {
        /// <summary>
        /// Player changes handled here
        /// </summary>
        public PlayerCreature player;

        /// <summary>
        /// Teddy handled here
        /// </summary>
        public TeddyBear teddy;

        public TreeTile tree;

        /// <summary>
        /// Whether the level is still locked.
        /// </summary>
        public bool Locked
        {
            get
            {
                return _Locked;
            }
            set
            {
                _Locked = value;
                SaveStatus();
            }
        }
        /// <summary>
        /// Whether the level is solved.
        /// </summary>
        public bool Solved
        {
            get
            {
                return _Solved;
            }
            set
            {
                _Solved = value;
                SaveStatus();
            }
        }

        public int LevelNumber;
        private bool _Locked;
        private bool _Solved;

        /// <summary>
        /// Creates a new LevelEntity.
        /// </summary>
        /// <param name="level">The integer of the level.</param>
        public LevelEntity(int level)
        {
            LevelNumber = level;

            // Get level status
            String[] levelStatuses = GameHandler.AssetHandler.ReadFile("Levels/levels_status.txt").Split('\n');
            String[] levelStatus = levelStatuses[LevelNumber - 1].Split(',');
            _Locked = bool.Parse(levelStatus[0]);
            _Solved = bool.Parse(levelStatus[1]);

            player = new PlayerCreature(level);
            teddy = new TeddyBear(level);
            tree = new TreeTile(level);

        }

        public override void Update()
        {
            PlayingState state = (PlayingState)Parent;
            if (state.GameOver)
            {
                if (GameHandler.InputHandler.OnAKeyDown())
                {
                    Reset();
                }
            }
            if (state.Won)
            {
                if (GameHandler.InputHandler.OnAKeyDown())
                {
                    Solved = true;
                    state.Won = false;
                    state.GoToNextLevel();
                }
            }

            if (MathHelper.Distance(teddy.Position.X, player.Position.X) > 20)
            {
                state.Hint.Text = "robbert is gaaf";

                state.GameOver = false;

                ResizeToContents();
                Size.X += 500;
            }

            if (player.item == "sword")
                tree.Texture = GameHandler.AssetHandler.GetTexture("chapter3/ch3_tree_swordless");
                
            base.Update();
        }

        /// <summary>
        /// Resets the level.
        /// </summary>
        public void Reset()
        {
            // Clear entities
            foreach (Entity entity in Children)
            {
                RemoveChild(entity);
            }

            // Load tiles
            String level = GameHandler.AssetHandler.ReadFile("Levels/" + LevelNumber + ".txt");
            String[] splitLevel = level.Split('\n');
            LoadGridEntities(level, ConvertTiles, new Vector2(500, 500), new Point(20, 20), new Point(-1, splitLevel.Length - 2));

            // Layer entities
            foreach (Entity entity in Children)
            {
                entity.Layer = 10;
            }

            player = new PlayerCreature(LevelNumber);
            teddy = new TeddyBear(LevelNumber);
            tree = new TreeTile(LevelNumber);


            PlayingState state = (PlayingState)Parent;
           // state.Timer.TimeLeft = TimeSpan.FromSeconds(double.Parse(splitLevel[splitLevel.Length - 2]));
            state.Hint.Text = splitLevel[splitLevel.Length - 1];

            state.GameOver = false;

            ResizeToContents();
            Size.X += 500;
        }

        /// <summary>
        /// Converts characters to their corresponding Entities.
        /// </summary>
        /// <param name="character">The character to convert.</param>
        /// <returns>The corresponding Entity.</returns>
        private Entity ConvertTiles(char character)
        {
            switch (character)
            {
                //Tree in size matching with level
                case '-':
                    return tree;
                case '$':
                case '%':
                    DefaultPlatform defaultPlatform = new DefaultPlatform();
                    if (character == '$')
                    {
                        defaultPlatform.Velocity.X = 100F;
                    }
                    else if (character == '%')
                    {
                        defaultPlatform.Velocity.X = -100F;
                    }
                    return defaultPlatform;
                case '+':
                case '&':
                case '(':
                case '@':
                case '!':
                case '~':

                case 'E':
                    return new GoalTile();
                case 'W':
                case 'P':
                    return player;
                case '#':
                    return teddy;
                case '/':
                case '>':
                    DefaultWall defaultWall = new DefaultWall();
                    if (character == '/')
                    {
                        defaultWall.Velocity.X = 100F;
                    }
                    else if (character == '>')
                    {
                        defaultWall.Velocity.X = -100F;
                    }
                    return defaultWall;

                    //Below here are all the buildingblocks
                case '^':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.wood);
                case 'X':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.brick);
                case '\\':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.black);
                case '*':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.ground);
                case '[':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.street);
                case ']':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.etc);


                case 'T':
                    return new TrampolineBedTile();

                    //Below here is chapter 3.1
                case 'R':
                    return new BedTile();
                case 'r':
                    return new PlantPot();
                case 'S':
                    return new PCDesk();
                case 'A':
                    return new GuitarShotgun();

                case 'C':

                    //Below here is Train from chapter1.1
                case 'B':
                    return new PlayingBlocks();
                case 't':
                    //return new TrainTracks();
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.ground);
                case 'k':
                    return new TrainStopBlock();
                case 'z':
                    return new Train();
                case '1':
                    return new Chapter11startTile();

                    //Default
                default:
                    TileEntity tile = new TileEntity();
                    tile.CanCollide = false;
                    return tile;
            }
        }

        /// <summary>
        /// Saves the specified status.
        /// </summary>
        public void SaveStatus()
        {
            String[] levelStatuses = GameHandler.AssetHandler.ReadFile("Levels/levels_status.txt").Split('\n');
            levelStatuses[LevelNumber - 1] = Locked.ToString().ToLower() + "," + Solved.ToString().ToLower();
            GameHandler.AssetHandler.WriteFile("Levels/levels_status.txt", String.Join(('\n').ToString(), levelStatuses), false);
        }
    }
}