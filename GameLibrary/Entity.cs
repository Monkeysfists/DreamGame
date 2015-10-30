using GameLibrary.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameLibrary {
	/// <summary>
	/// An actual entity that is placed in the game world.
	/// </summary>
	public class Entity : Updatable, Drawable {
		public string Name;
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
				return _Children.AsReadOnly();
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
		/// The size of the Entity.
		/// </summary>
		public Vector2 Size;
		/// <summary>
		/// The hitbox of the Entity, relative to its parent's hitbox.
		/// </summary>
		public RectangleF Hitbox {
			get {
				return new RectangleF(Position.X, Position.Y, Size.X, Size.Y);
			}
		}
		/// <summary>
		/// The hitbox of the Entity relative to the upper-left corner of the game window.
		/// </summary>
		public RectangleF GlobalHitbox {
			get {
				if (Parent == null) {
					return Hitbox;
				} else {
					return new RectangleF(Hitbox.X - Parent.GlobalHitbox.X, Hitbox.Y - Parent.GlobalHitbox.Y, Hitbox.Width, Hitbox.Height);
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
		/// The texture of the Entity.
		/// </summary>
		public Texture2D Texture;
		/// <summary>
		/// The tint that will be applied to the texture.
		/// </summary>
		public Color Tint;
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

		/// <summary>
		/// Creates a new Entity.
		/// </summary>
		/// <param name="gameHandler">The GameHandler.</param>
		public Entity(string name = "") {
			_Parent = null;
			_Children = new List<Entity>();

			Name = name;
			Position = Vector2.Zero;
			Size = Vector2.Zero;
			Velocity = Vector2.Zero;
			Texture = GameHandler.GraphicsHandler.GetColoredTexture(Color.Transparent, new Point(1, 1));
			Tint = Color.White;
			Active = true;
			Visible = true;
		}

		public virtual void Update() {
			if(Active) {
				// Move
				Position += GlobalVelocity;

				// Update child entities
				foreach (Entity entity in Children) {
					entity.Update();
				}
			}
		}

		public virtual void Draw() {
			if(Visible) {
				GameHandler.GraphicsHandler.DrawTexture(GlobalPosition, Size, Texture, Tint);

				// Draw child entities
				foreach (Entity entity in Children) {
					entity.Draw();
				}
			}
		}

		/// <summary>
		/// Resizes the Entity to the size of its contents.
		/// </summary>
		/// <param name="checkTexture">Whether to use the size of the texture in the check.</param>
		/// <param name="shrinkWidth">Whether to allow the resize to shrink in width.</param>
		/// <param name="shrinkHeight">Whether to allow the resize to shrink in height.</param>
		/// <param name="growWidth">Whether to allow the resize to grow in width.</param>
		/// <param name="growHeight">Whether to allow the resize to grow in height.</param>
		public void ResizeToContents(bool checkTexture = true, bool shrinkWidth = true, bool shrinkHeight = true, bool growWidth = true, bool growHeight = true) {
			Vector2 newSize = Vector2.Zero;

			// If checking the texture, use its size
			if (checkTexture) {
				newSize = new Vector2(Texture.Width, Texture.Height);
			}

			foreach (Entity entity in Children) {
				if (entity.Hitbox.Right > newSize.X) {
					newSize.X = entity.Hitbox.Right;
				}
				if (entity.Hitbox.Bottom > newSize.Y) {
					newSize.Y = entity.Hitbox.Bottom;
				}
			}

			// Only grow or shrink if allowed
			if ((newSize.X < Size.X && shrinkWidth) || (newSize.X > Size.X && growWidth)) {
				Size.X = newSize.X;
			}
			if ((newSize.Y < Size.Y && shrinkHeight) || (newSize.Y > Size.Y && growHeight)) {
				Size.Y = newSize.Y;
			}
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
			_Children.Remove(entity);
		}

		/// <summary>
		/// Gets a list of children with the specified name.
		/// </summary>
		/// <param name="name">The name to look for.</param>
		/// <param name="recursive">Whether to search recursively (to the deepest level).</param>
		/// <returns>A list of children with the specified name.</returns>
		public List<Entity> FindChildrenByName(string name, bool recursive) {
			List<Entity> result = new List<Entity>();

			foreach(Entity entity in Children) {
				if(entity.Name == name) {
					result.Add(entity);
				}

				if(recursive) {
					result.AddRange(entity.FindChildrenByName(name, true));
				}
			}

			return result;
		}

		/// <summary>
		/// Gets a list of Entities that are colliding with this Entity.
		/// </summary>
		/// <param name="entities"></param>
		/// <returns>A list of entities that intersect.</returns>
		public List<Entity> GetCollidingEntities(List<Entity> entities) {
			List<Entity> result = new List<Entity>();

			foreach (Entity entity in entities) {
				if (GlobalHitbox.Intersects(entity.GlobalHitbox)) {
					// INTERSECTION!
					result.Add(entity);
				}
			}

			return result;
		}
	}
}
