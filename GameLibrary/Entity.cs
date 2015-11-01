using GameLibrary.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameLibrary {
	/// <summary>
	/// An actual entity that is placed in the game world.
	/// </summary>
	public class Entity {
		public String Name;
		/// <summary>
		/// The parent Entity that this is a child of.
		/// </summary>
		public Entity Parent {
			get {
				return _Parent;
			}
		}
		/// <summary>
		/// The parent that is at the root of the Entity tree.
		/// </summary>
		public Entity RootParent {
			get {
				if (Parent == null) {
					return this;
				} else {
					return Parent.RootParent;
				}
			}
		}
		/// <summary>
		/// A list of child Entities.
		/// </summary>
		public ReadOnlyCollection<Entity> Children {
			get {
				// Only get entities with a parent
				List<Entity> result = new List<Entity>();
				
				foreach(Entity entity in _Children) {
					if(entity.Parent != null) {
						result.Add(entity);
					}
				}

				return result.OrderBy(o => o.Layer).ToList().AsReadOnly();
			}
		}
		/// <summary>
		/// A recursive list of child Entities.
		/// </summary>
		public List<Entity> GlobalChildren {
			get {
				List<Entity> result = new List<Entity>();

				foreach (Entity entity in Children) {
					result.Add(entity);
					result.AddRange(entity.GlobalChildren);
				}

				return result;
			}
		}
		/// <summary>
		/// The layer to draw at.
		/// </summary>
		public int Layer;
		/// <summary>
		/// The position of the Entity, relative to its parent's position.
		/// </summary>
		public Vector2 Position;
		/// <summary>
		/// The position of the Entity relative to the upper-left corner of the game window.
		/// </summary>
		public Vector2 GlobalPosition {
			get {
				if (Parent == null) {
					return Position;
				} else {
					return Position + Parent.GlobalPosition;
				}
			}
		}
		/// <summary>
		/// The rotation of the Entity in radians, relative to its parent's rotation.
		/// </summary>
		public float Rotation;
		/// <summary>
		/// The rotation of the Entity, relative to the game.
		/// </summary>
		public float GlobalRotation {
			get {
				if (Parent == null) {
					return Rotation;
				} else {
					return Rotation + Parent.GlobalRotation;
				}
			}
		}
		/// <summary>
		/// The size of the Entity.
		/// </summary>
		public Vector2 Size;
		/// <summary>
		/// Whether the Entity can collide with other Entities.
		/// </summary>
		public bool CanCollide;
		/// <summary>
		/// The bounding box of the Entity, relative to its parent's bounding box.
		/// </summary>
		public RectangleF BoundingBox {
			get {
				return new RectangleF(Position.X - Origin.X, Position.Y - Origin.Y, Size.X, Size.Y);
			}
		}
		/// <summary>
		/// The bounding box of the Entity relative to the upper-left corner of the game window.
		/// </summary>
		public RectangleF GlobalBoundingBox {
			get {
				return new RectangleF(GlobalPosition.X - GlobalOrigin.X, GlobalPosition.Y - GlobalOrigin.Y, BoundingBox.Width, BoundingBox.Height);
			}
		}
		/// <summary>
		/// The collision box of the Entity, relative to its parent's bounding box.
		/// </summary>
		public RectangleF CollisionBox {
			get {
				if (_CollisionBox == null) {
					return new RectangleF(0, 0, Size.X, Size.Y);
				} else {
					return new RectangleF(_CollisionBox.X, _CollisionBox.Y, _CollisionBox.Width, _CollisionBox.Height);
				}
			}
			set {
				_CollisionBox = value;
			}
		}
		/// <summary>
		/// The collision box of the Entity, relative to the upper-left corner of the game window.
		/// </summary>
		public RectangleF GlobalCollisionBox {
			get {
				if (_CollisionBox == null) {
					return GlobalBoundingBox;
				} else {
					return new RectangleF(CollisionBox.X + GlobalBoundingBox.X, CollisionBox.Y + GlobalBoundingBox.Y, CollisionBox.Width, CollisionBox.Height);
				}
			}
		}
		/// <summary>
		/// The velocity of the Entity, relative to its parent's velocity.
		/// </summary>
		public Vector2 Velocity;
		/// <summary>
		/// The velocity of the Entity relative to the upper-left corner of the game window.
		/// </summary>
		public Vector2 GlobalVelocity {
			get {
				if (Parent == null) {
					return Velocity;
				} else {
					return Velocity + Parent.GlobalVelocity;
				}
			}
		}
		/// <summary>
		/// The origin point of the Entity, relative to its parent's origin.
		/// </summary>
		public Vector2 Origin;
		/// <summary>
		/// The origin point of the Entity relative to the upper-left corner of the game window.
		/// </summary>
		public Vector2 GlobalOrigin {
			get {
				if(Parent == null) {
					return Origin;
				} else {
					return Origin + Parent.GlobalOrigin;
				}
			}
		}
		/// <summary>
		/// The origin used for drawing.
		/// </summary>
		public Vector2 DrawOrigin;
		/// <summary>
		/// The origin used for drawing.
		/// </summary>
		public Vector2 GlobalDrawOrigin {
			get {
				if(Parent == null) {
					return DrawOrigin;
				} else {
					return DrawOrigin + Parent.GlobalDrawOrigin;
				}
			}
		}
		/// <summary>
		/// Whether the entity is updated.
		/// </summary>
		public bool Active;
		/// <summary>
		/// Whether the entity is drawn.
		/// </summary>
		public bool Visible;

		private Entity _Parent;
		private List<Entity> _Children;
		private RectangleF _CollisionBox;

		/// <summary>
		/// Creates a new Entity.
		/// </summary>
		/// <param name="gameHandler">The GameHandler.</param>
		public Entity(String name = "") {
			_Parent = null;
			_Children = new List<Entity>();

			Name = name;
			Layer = 0;
			Position = Vector2.Zero;
			Rotation = 0F;
			Size = Vector2.Zero;
			CanCollide = false;
			Origin = Vector2.Zero;
			Velocity = Vector2.Zero;
			Active = true;
			Visible = true;
		}

		public virtual void Update() {
			// Move
			Position += GlobalVelocity * (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds; // TODO: Arbitraty multiplication. ElapsedGameTime.TotalSeconds always amounts to 0.01667, because update is called 60 times per second by any means necessary. Remove in future versions.

			// Update child entities
			foreach (Entity entity in Children) {
				if (entity.Active) {
					entity.Update();
				}
			}

			Cleanup();
		}

		public virtual void Draw() {
			// Draw child entities
			foreach (Entity entity in Children) {
				if (entity.Visible) {
					entity.Draw();
				}
			}

			Cleanup();
		}

		/// <summary>
		/// Removes orphaned entities.
		/// </summary>
		private void Cleanup() {
			List<Entity> clean = new List<Entity>(Children);

			while (clean.Count > 0) {
				if (clean[0].Parent == null) {
					_Children.Remove(clean[0]);
				}
				clean.Remove(clean[0]);
			}
		}

		/// <summary>
		/// Resizes the Entity to the size of its contents.
		/// </summary>
		/// <param name="minimumSize">The minimum size to resize to.</param>
		public virtual void ResizeToContents(Vector2? minimumSize = null) {
			Vector2 newSize = minimumSize ?? Vector2.Zero;

			foreach (Entity entity in Children) {
				if (entity.BoundingBox.Right > newSize.X) {
					newSize.X = entity.BoundingBox.Right;
				}
				if (entity.BoundingBox.Bottom > newSize.Y) {
					newSize.Y = entity.BoundingBox.Bottom;
				}
			}

			Size = newSize;
		}

		/// <summary>
		/// Adds a child Entity.
		/// </summary>
		/// <param name="entity">The entity to add.</param>
		public void AddChild(Entity entity) {
			_Children.Add(entity);
			entity._Parent = this;
		}

		/// <summary>
		/// Removes a child Entity.
		/// </summary>
		/// <param name="entity">The entity to remove.</param>
		public void RemoveChild(Entity entity) {
			entity._Parent = null;
		}

		/// <summary>
		/// Gets a list of children with the specified name.
		/// </summary>
		/// <param name="name">The name to look for.</param>
		/// <param name="recursive">Whether to search recursively (to the deepest level).</param>
		/// <returns>A list of children with the specified name.</returns>
		public List<Entity> FindChildrenByName(String name, bool recursive) {
			List<Entity> result = new List<Entity>();

			foreach (Entity entity in Children) {
				if (entity.Name == name) {
					result.Add(entity);
				}

				if (recursive) {
					result.AddRange(entity.FindChildrenByName(name, true));
				}
			}

			return result;
		}

		/// <summary>
		/// Gets a list of Entities that are colliding with this Entity.
		/// </summary>
		/// <param name="entities">The entities to check.</param>
		/// <param name="offset">The amount to offset the current position when checking collisions.</param>
		/// <param name="entityOffset">The amount to offset other entities when checking collisions.</param>
		/// <returns>A list of entities that intersect.</returns>
		public List<Entity> GetCollidingEntities(List<Entity> entities, Vector2 offset, Vector2 entityOffset) {
			RectangleF boundingBox = new RectangleF(GlobalCollisionBox.X + offset.X, GlobalCollisionBox.Y + offset.Y, GlobalCollisionBox.Width, GlobalCollisionBox.Height);
			List<Entity> result = new List<Entity>();

			foreach (Entity entity in entities) {
				RectangleF entityBoundingBox = new RectangleF(entity.GlobalCollisionBox.X + entityOffset.X, entity.GlobalCollisionBox.Y + entityOffset.Y, entity.GlobalCollisionBox.Width, entity.GlobalCollisionBox.Height);
				if (entity != this && entity.CanCollide && boundingBox.Intersects(entityBoundingBox)) {
					// INTERSECTION!
					result.Add(entity);
				}
			}

			return result;
		}

		/// <summary>
		/// Gets a list of entities at the specified position.
		/// </summary>
		/// <param name="entities">The entities to check.</param>
		/// <param name="position">The position.</param>
		/// <returns>A list of entities at the specified position.</returns>
		public List<Entity> GetEntitiesAtPosition(List<Entity> entities, Vector2 position) {
			List<Entity> result = new List<Entity>();

			foreach (Entity entity in entities) {
				if (entity.GlobalBoundingBox.Contains(position)) {
					result.Add(entity);
				}
			}

			return result;
		}

		/// <summary>
		/// Loads a grid of Entities from a string, where characters in the supplied dictionary are converted to actual Entities.
		/// </summary>
		/// <param name="tet">The string to read.</param>
		/// <param name="converter">A method which takes a character and returns the corresponding Entity.</param>
		/// <param name="startingPosition">The position at which to start adding Entities.</param>
		/// <param name="cellSize">The size of a single cell. Set to -1 to autofill based on Entity width and height.</param>
		/// <param name="maxDimensions">The maximum amount of rows and columns. Set to -1 to load all.</param>
		public void LoadGridEntities(string text, Func<char, Entity> converter, Vector2 startingPosition, Point cellSize, Point maxDimensions) {
			String[] grid = text.Split('\n');

			Vector2 currentPosition = Vector2.Zero; // Position for the next item
			Vector2 nextPosition = Vector2.Zero;        // Y value used for the next row
			for (int row = 0; row < grid.Length && (maxDimensions.Y == -1 || row < maxDimensions.Y); row++) {
				for (int column = 0; column < grid[row].Length && (maxDimensions.X == -1 || column < maxDimensions.X); column++) {
					Entity entity = converter(grid[row][column]);

					// Set X position
					if (cellSize.X == -1) {
						entity.Position.X = currentPosition.X;

						// Increase X
						currentPosition.X += entity.Size.X;
					} else {
						entity.Position.X = column * cellSize.X;
					}

					// Set Y position
					if (cellSize.Y == -1) {
						entity.Position.Y = currentPosition.Y;

						// Increase Y of next row if needed
						if (currentPosition.Y + entity.Size.Y > nextPosition.Y) {
							nextPosition.Y = currentPosition.Y + entity.Size.Y;
						}
					} else {
						entity.Position.Y = row * cellSize.Y;
					}

					entity.Position += startingPosition;

					AddChild(entity);
				}

				// Reset;
				currentPosition = nextPosition;
				nextPosition = Vector2.Zero;
			}
		}
	}
}
