using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameLibrary {
	/// <summary>
	/// An actual entity that is placed in the game world.
	/// </summary>
	public class Entity : Updatable, Drawable {
		/// <summary>
		/// The parent Entity that this is a child of.
		/// </summary>
		public Entity Parent;
		/// <summary>
		/// A list of child Entities.
		/// </summary>
		public ReadOnlyCollection<Entity> Children {
			get {
				return _Children.AsReadOnly();
			}
		}
		/// <summary>
		/// The position of the Entity, relative to its parent's position.
		/// </summary>
		public Vector2 Position;
		/// <summary>
		/// The position of the Entity in global space.
		/// </summary>
		public Vector2 GlobalPosition {
			get {
				if(Parent == null) {
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
		/// The velocity of the Entity, relative to its parent's velocity.
		/// </summary>
		public Vector2 Velocity;
		/// <summary>
		/// The velocity of the Entity in global space.
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

		private GameHandler _GameHandler;
		private List<Entity> _Children;

		/// <summary>
		/// Creates a new Entity.
		/// </summary>
		/// <param name="gameHandler">The GameHandler.</param>
		public Entity(GameHandler gameHandler) {
			_GameHandler = gameHandler;

			Parent = null;
			_Children = new List<Entity>();

			Position = Vector2.Zero;
			Size = Vector2.Zero;
			Velocity = Vector2.Zero;
			Texture = _GameHandler.GraphicsHandler.GetColoredTexture(Color.Transparent);
		}

		/// <summary>
		/// Handles input.
		/// </summary>
		public void HandleInput() {
			// TODO: Handle input
		}

		public void Update() {
			// Move
			Position += GlobalVelocity;
        }
		
		public void Draw() {
			// TODO: Draw
		}
	}
}
