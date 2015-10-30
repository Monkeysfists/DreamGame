using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameLibrary {
	public class StateHandler : Updatable, Drawable {
		/// <summary>
		/// The state that is currently active. Set to null to disable.
		/// </summary>
		public Entity CurrentState {
			get {
				return _CurrentState;
			}
			set {
				// Only set the state if it has been loaded.
				if(States.ContainsValue(value)) {
					_CurrentState = value;
				} else {
					throw new KeyNotFoundException("Could not find game state: " + value);
				}
			}
		}
		/// <summary>
		/// A list of all loaded states.
		/// </summary>
		public Dictionary<string, Entity> States {
			get {
				return new Dictionary<string, Entity>(_States);
			}
		}

		private Entity _CurrentState;
		private Dictionary<string, Entity> _States;

		/// <summary>
		/// Creates a new StateHandler.
		/// </summary>
		public StateHandler() {
			_States = new Dictionary<string, Entity>();
		}

		public void Update() {
			if(CurrentState != null) {
				CurrentState.Update();
			}
		}

		public void Draw() {
			if (CurrentState != null) {
				CurrentState.Draw();
			}
		}

		/// <summary>
		/// Adds a state to the StateHandler.
		/// </summary>
		/// <param name="id">The ID of the state to add.</param>
		/// <param name="state">The state to add.</param>
		public void AddState(string id, Entity state) {
			_States.Add(id, state);
		}

		/// <summary>
		/// Removes a state from the StateHandler.
		/// </summary>
		/// <param name="id">The ID of the state to remove.</param>
		public void RemoveState(string id) {
			_States.Remove(id);
		}
	}
}
