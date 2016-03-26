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

        public Train train;

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
            train = new Train();

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

            if (MathHelper.Distance(player.Position.Y, train.Position.Y) < 300)
                train.StartBool = true;

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
            train = new Train();


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
                //Building blocks
                case 'W':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.wood);
                case 'B':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.buildingblock);
                case '*':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.black);
                case 'G':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.ground);
                case 'S':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.street);
                case '-':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.etc);

                //Objects used in multiple chapters
                case 'T':
                    return tree;
                case 'P':
                    return player;
                case 'E':
                    return teddy;
                case 'q':
                    return new GuitarShotgun(LevelNumber);
                case 'b': switch (LevelNumber)
                    {
                        case 1: return new TrampolineBedTile();
                        default: return new BedTile(LevelNumber);
                    }
                case 'D':
                    return new PCDesk();
                case 'H':
                    return new HighSchool(LevelNumber);


                case '#':
                    return new GoalTile();
                case '@':
                //return new StartingTile();

                //Chapter 1   
                case 'n': //Nightstand
                    return new CollisionObject("chapter1/nightstand");
                case 'r':
                    return new TrainTracks();
                case 'k':
                    return new TrainStopBlock();
                case 't':
                    return new Train();
                case 'd': //Uithangborden
                    return new CollisionObject("chapter1/uithangborden/bord" + GameHandler.Random.Next(4));
                    //return new NoCollisionObject("chapter1/uithangborden/bordpaal1");
                    //return new NoCollisionObject("chapter1/uithangborden/bordpaal2");
                case 'F': //Flower
                    return new NoCollisionObject("chapter1/flower");
                case 'M':
                    return new Mother();
                    //return new Umbrella();
                case 'w':
                    return new WaterTile();

                //Chapter 3

                //Chapter 4
                case 'c': //Closet
                    return new NoCollisionObject("chapter4/closet");

                case '$':
                case '%':
                    DefaultPlatform defaultPlatform = new DefaultPlatform();
                    if (character == '$')
                        defaultPlatform.Velocity.X = 100F;
                    else if (character == '%')
                        defaultPlatform.Velocity.X = -100F;
                    return defaultPlatform;

                case '/':
                case '>':
                    DefaultWall defaultWall = new DefaultWall();
                    if (character == '/')
                        defaultWall.Velocity.X = 100F;
                    else if (character == '>')
                        defaultWall.Velocity.X = -100F;
                    return defaultWall;

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