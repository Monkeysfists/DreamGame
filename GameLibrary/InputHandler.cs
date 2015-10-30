using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameLibrary {
	/// <summary>
	/// Handles all input.
	/// </summary>
	public class InputHandler : Updatable {
		/// <summary>
		/// All available mouse buttons.
		/// </summary>
		public enum MouseButton {
			Left,
			Middle,
			Right
		}
		/// <summary>
		/// The current position of the mouse.
		/// </summary>
		public Vector2 MousePosition {
			get {
				// We'll need to correct for any scaling, and thus we divide by the drawing scale
				return new Vector2(_CurrentMouseState.X, _CurrentMouseState.Y) / GameHandler.GraphicsHandler.Scale;
			}
		}
		/// <summary>
		/// A list of mouse buttons that are down.
		/// </summary>
		public List<MouseButton> MouseButtonsDown {
			get {
				List<MouseButton> result = new List<MouseButton>();

				if(MouseButtonDown(MouseButton.Left)) {
					result.Add(MouseButton.Left);
				}
				if (MouseButtonDown(MouseButton.Middle)) {
					result.Add(MouseButton.Middle);
				}
				if (MouseButtonDown(MouseButton.Right)) {
					result.Add(MouseButton.Right);
				}

				return result;
			}
		}
		/// <summary>
		/// A list of keys that are down.
		/// </summary>
		public List<Keys> KeysDown {
			get {
				return new List<Keys>(_CurrentKeyboardState.GetPressedKeys());
			}
		}

		private MouseState _CurrentMouseState;
		private MouseState _PreviousMouseState;
		private KeyboardState _CurrentKeyboardState;
		private KeyboardState _PreviousKeyboardState;
		
		public void Update() {
			_PreviousMouseState = _CurrentMouseState;
			_PreviousKeyboardState = _CurrentKeyboardState;
			_CurrentMouseState = Mouse.GetState();
			_CurrentKeyboardState = Keyboard.GetState();
		}

		/// <summary>
		/// Gets whether the MouseButton was just pressed down.
		/// </summary>
		/// <param name="button">The MouseButton to check.</param>
		/// <returns>Whether the MouseButton was just pressed down.</returns>
		public bool OnMouseButtonDown(MouseButton button) {
			switch (button) {
				case MouseButton.Left:
					return MouseButtonDown(button) && _PreviousMouseState.LeftButton != ButtonState.Pressed;
				case MouseButton.Middle:
					return MouseButtonDown(button) && _PreviousMouseState.MiddleButton != ButtonState.Pressed;
				default:
					return MouseButtonDown(button) && _PreviousMouseState.RightButton != ButtonState.Pressed;
			}
		}

		/// <summary>
		/// Gets whether the MouseButton is being held down.
		/// </summary>
		/// <param name="button">The MouseButton to check.</param>
		/// <returns>Whether the MouseButton is being held down.</returns>
		public bool OnMouseButtonHold(MouseButton button) {
			switch (button) {
				case MouseButton.Left:
					return MouseButtonDown(button) && _PreviousMouseState.LeftButton == ButtonState.Pressed;
				case MouseButton.Middle:
					return MouseButtonDown(button) && _PreviousMouseState.MiddleButton == ButtonState.Pressed;
				default:
					return MouseButtonDown(button) && _PreviousMouseState.RightButton == ButtonState.Pressed;
			}
		}

		/// <summary>
		/// Gets whether the MouseButton was just released.
		/// </summary>
		/// <param name="button">The MouseButton to check.</param>
		/// <returns>Whether the MouseButton was just released.</returns>
		public bool OnMouseButtonUp(MouseButton button) {
			switch (button) {
				case MouseButton.Left:
					return !MouseButtonDown(button) && _PreviousMouseState.LeftButton == ButtonState.Pressed;
				case MouseButton.Middle:
					return !MouseButtonDown(button) && _PreviousMouseState.MiddleButton == ButtonState.Pressed;
				default:
					return !MouseButtonDown(button) && _PreviousMouseState.RightButton == ButtonState.Pressed;
			}
		}

		/// <summary>
		/// Gets whether the MouseButton is down.
		/// </summary>
		/// <param name="button">The MouseButton to check.</param>
		/// <returns>Whether the MouseButton is down.</returns>
		public bool MouseButtonDown(MouseButton button) {
			switch (button) {
				case MouseButton.Left:
					return _CurrentMouseState.LeftButton == ButtonState.Pressed;
				case MouseButton.Middle:
					return _CurrentMouseState.MiddleButton == ButtonState.Pressed;
				default:
					return _CurrentMouseState.RightButton == ButtonState.Pressed;
			}
		}

		/// <summary>
		/// Gets whether the MouseButton is up.
		/// </summary>
		/// <param name="button">The MouseButton to check.</param>
		/// <returns>Whether the MouseButton is up.</returns>
		public bool MouseButtonUp(MouseButton button) {
			switch (button) {
				case MouseButton.Left:
					return _CurrentMouseState.LeftButton != ButtonState.Pressed && _PreviousMouseState.LeftButton == ButtonState.Pressed;
				case MouseButton.Middle:
					return _CurrentMouseState.MiddleButton != ButtonState.Pressed && _PreviousMouseState.MiddleButton == ButtonState.Pressed;
				default:
					return _CurrentMouseState.RightButton != ButtonState.Pressed && _PreviousMouseState.RightButton == ButtonState.Pressed;
			}
		}

		/// <summary>
		/// Gets whether the key was just pressed down.
		/// </summary>
		/// <param name="button">The key to check.</param>
		/// <returns>Whether the key was just pressed down.</returns>
		public bool OnKeyDown(Keys key) {
			return KeyDown(key) && _PreviousKeyboardState.IsKeyUp(key);
		}

		/// <summary>
		/// Gets whether the key is being held down.
		/// </summary>
		/// <param name="button">The key to check.</param>
		/// <returns>Whether the key is being held down.</returns>
		public bool OnKeyHold(Keys key) {
			return KeyDown(key) && _PreviousKeyboardState.IsKeyDown(key);
		}

		/// <summary>
		/// Gets whether the key was just released.
		/// </summary>
		/// <param name="button">The key to check.</param>
		/// <returns>Whether the key was just released.</returns>
		public bool OnKeyUp(Keys key) {
			return KeyUp(key) && _PreviousKeyboardState.IsKeyDown(key);
		}

		/// <summary>
		/// Gets whether the key is down.
		/// </summary>
		/// <param name="button">The key to check.</param>
		/// <returns>Whether the key is down.</returns>
		public bool KeyDown(Keys key) {
			return _CurrentKeyboardState.IsKeyDown(key);
		}

		/// <summary>
		/// Gets whether the key is up.
		/// </summary>
		/// <param name="button">The key to check.</param>
		/// <returns>Whether the key is up.</returns>
		public bool KeyUp(Keys key) {
			return _CurrentKeyboardState.IsKeyUp(key);
		}
	}
}
