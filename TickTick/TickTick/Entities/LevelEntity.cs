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
            LoadGridEntities(level, ConvertTiles, new Vector2(500, 500), new Point(72, 55), new Point(-1, splitLevel.Length - 2));

            // Layer entities
            foreach (Entity entity in Children)
            {
                entity.Layer = 10;
            }

            PlayingState state = (PlayingState)Parent;
            state.Timer.TimeLeft = TimeSpan.FromSeconds(double.Parse(splitLevel[splitLevel.Length - 2]));
            state.Hint.Text = splitLevel[splitLevel.Length - 1];

            state.GameOver = false;

            ResizeToContents();
            Size.X += 500;

            // Add random mountains
            for (int i = 0; i < 7; i++)
            {
                MountainEntity mountain = new MountainEntity();
                mountain.Layer = i + 1;
                AddChild(mountain);
                mountain.Initialize();
            }

            // Add random clouds
            for (int i = 0; i < 7; i++)
            {
                CloudEntity cloud = new CloudEntity();
                cloud.Layer = i + 1;
                AddChild(cloud);
                cloud.Initialize();
            }
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
                case '-':
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
                    HotPlatform hotPlatform = new HotPlatform();
                    if (character == '&')
                    {
                        hotPlatform.Velocity.X = 100F;
                    }
                    else if (character == '(')
                    {
                        hotPlatform.Velocity.X = -100F;
                    }
                    return hotPlatform;
                case '@':
                case '!':
                case '~':
                    IcePlatform icePlatform = new IcePlatform();
                    if (character == '!')
                    {
                        icePlatform.Velocity.X = 100F;
                    }
                    else if (character == '~')
                    {
                        icePlatform.Velocity.X = -100F;
                    }
                    return icePlatform;
                case 'X':
                    return new GoalTile();
                case 'W':
                    return new WaterTile();
                case '1':
                    return new PlayerCreature();
                case '#':
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
                case '^':
                case '<':
                case '\\':
                    HotWall hotWall = new HotWall();
                    if (character == '<')
                    {
                        hotWall.Velocity.X = 100F;
                    }
                    else if (character == '\\')
                    {
                        hotWall.Velocity.X = -100F;
                    }
                    return hotWall;
                case '*':
                case '[':
                case ']':
                    IceWall iceWall = new IceWall();
                    if (character == '[')
                    {
                        iceWall.Velocity.X = 100F;
                    }
                    else if (character == ']')
                    {
                        iceWall.Velocity.X = -100F;
                    }
                    return iceWall;
                case 'T':
                    return new TurtleCreature();
                case 'R':
                    RocketCreature rocketLeft = new RocketCreature();
                    rocketLeft.MoveSpeed = new Vector2(-600F, 0F);
                    return rocketLeft;
                case 'r':
                    RocketCreature rocketRight = new RocketCreature();
                    rocketRight.MoveSpeed = new Vector2(600F, 0F);
                    return rocketRight;
                case 'S':
                    return new SparkyCreature();
                case 'A':
                    return new WildFlame();
                case 'B':
                    return new StalkerFlame();
                case 'C':
                    return new FlameCreature();
                case 'k':
                    return new ShieldTile();
                case 'z':
                    return new BananaTile();
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