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
using TickTick.Entities.Tiles.Platforms.Chapter1;
using TickTick.Entities.Tiles.Platforms.Chapter2;
using TickTick.Entities.Tiles.Platforms.Chapter3;

namespace TickTick.Entities
{
    public class LevelEntity : Entity
    {
        /// <summary>
        /// Player changes handled here
        /// </summary>
        public playerCreature player;

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

            player = new playerCreature(level);
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
            switch (LevelNumber)
            {
                case 3:
                case 4:
                    if(state.background.Texture != GameHandler.AssetHandler.GetTexture("background/dark_sky"))
                        state.background.Texture = GameHandler.AssetHandler.GetTexture("background/dark_sky");
                    break;
                case 5:
                case 6:
                    break;
                default:
                    if (state.background.Texture != GameHandler.AssetHandler.GetTexture("background/sky"))
                        state.background.Texture = GameHandler.AssetHandler.GetTexture("background/sky");
                    break;



                
            }

            if (MathHelper.Distance(teddy.Position.X, player.Position.X) > 20)
            {
                state.Hint.Text = "robbert is gaaf";
                

                state.GameOver = false;

                ResizeToContents();
                Size.X += 500;
            }

            //TODO: verander de achtergrond afhankelijk vh level

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

            player = new playerCreature(LevelNumber);
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
                    return new CollisionObject("wood", new Vector2(20,100));
                case 'B':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.buildingblock);
                case '*':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.black);
                case 'G':
                    return new CollisionObject("grond", new Vector2(80,1000));
                case 'S':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.street);
                case '-':
                    return new BuildingBlockWall(BuildingBlockWall.BuildingBlocks.etc);
                case '+':
                    return new CollisionObject("WoodenFloorTile", new Vector2(100, 100));
                case '=':
                    return new CollisionObject("WoodenFloorTile", new Vector2(20, 20));
                case '_':
                    return new CollisionObject("InfiniteWood", new Vector2(80,1000));

                //Objects used in multiple chapters
                case 'T':
                    return tree;
                case 'P':
                    return player;
                case 'E':
                    return teddy;
                case 'q':
                    return new GuitarShotgun(LevelNumber);
                case 'b': 
                    switch (LevelNumber)
                    {
                        case 1: return new TrampolineBedTile();
                        default: return new BedTile(LevelNumber);
                    }
                case 'p':
                    return new PCDesk(LevelNumber);
                case 'H':
                    return new HighSchool(LevelNumber);
                case 'h':
                    return new NoCollisionObject("achtergrond huis", new Vector2(500,500));


                case '#':
                    return new GoalTile(LevelNumber);
                case '@':
                   return new BlockBoat();

                //Chapter 1   
                case 'n':
                    return new CollisionObject("chapter1/nightstand", new Vector2(120, 100));
                case 'r':
                    return new TrainTracks();
                case 'k':
                    return new TrainStopBlock();
                case 't':
                    return new Train();
                case 'D': 
                    return new Tunnel();
                case 'd':
                    return new Board();
                case 'F': 
                    return new NoCollisionObject("chapter1/flower", new Vector2(20,20));
                case 'M':
                    return new Mother();
                case 'w':
                    return new WaterTile();
                case '^':
                    //return new Paperboat();

                //Chapter 2
                case '2':
                    return new Dinosaur();
                case '3':
                    return new Ghost();
                case 'I':
                    return new CollisionObject("chapter2/castle_wall", new Vector2(20,20));
                case 'J':
                    return new CollisionObject("chapter2/castle_wall_top", new Vector2(20, 20));
                case 'j':
                    return new DefaultPlatform(new Vector2(20, 20));
                case 'i':
                    return new NoCollisionObject("chapter2/castle_wall_background", new Vector2(20,20));
                case 'K':
                    return new LegoBlock();
                case '(':
                    return new LightSwitch();
                case 'L':
                    return new LavaTile();
                case 'l':
                    return new UnderLavaTile();
                case 'z':
                    return new CollisionObject("chapter2/zebrawit", new Vector2(20, 20));
                case 'Z':
                    return new CollisionObject("chapter2/zebrazwart", new Vector2(20, 20));

                //Chapter 3
                case '0':
                    return new Bully();

                case '9':
                    return new CollisionObjectWall("WoodenFloorTile", new Vector2(20, 20));

                case '8':
                    return new FlyingCarCreature();

                case '7':
                    return new Asteroid();

                case '6':
                    return new BigAsteroid();
                case '5':
                    return new Road();
                case '4':
                    return new PlayerBike();
                case ':':
                    return new Obstacle();               

                //Chapter 4
                case 'c':
                    return new NoCollisionObject("chapter4/closet", new Vector2(40,80));
                case 'a':
                    return new NoCollisionObject("chapter4/autoleeg", new Vector2(80,60));
                case '!':
                    return new CollisionObject("chapter4/door_closed", new Vector2(20, 100));

                case '$':
                    return new Raindrop();
                case '%':
                    DefaultPlatform defaultPlatform = new DefaultPlatform(new Vector2(20,20));
                    if (character == '$')
                        defaultPlatform.Velocity.X = 100F;
                    else if (character == '%')
                        defaultPlatform.Velocity.X = -100F;
                    return defaultPlatform;

                case '/':
                    return new Raindrop();
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