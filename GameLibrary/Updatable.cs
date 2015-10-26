using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary {
	/// <summary>
	/// Represents a game object that is updatable.
	/// </summary>
	interface Updatable {
		/// <summary>
		/// Performs per-frame updates. Executed 60 times per second. Will catch up if the computer is running slowly.
		/// </summary>
		void Update();
	}
}
