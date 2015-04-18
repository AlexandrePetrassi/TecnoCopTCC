using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TecnoCop{
	public class Character : ModuleManager {
		
		private List<IUpdatable> mySubcomponents = new List<IUpdatable>();

		/// <summary>
		/// Awake this instance.
		/// </summary>
		void Awake () {
			updater = this; 
			GetComponents<IUpdatable>(mySubcomponents);
			mySubcomponents.Reverse(); // Isso eh temporario. Implementar algoritmo para ordenar a lista de forma correta
		}
		
		/// <summary>
		/// Update this instance.
		/// </summary>
		void FixedUpdate () {
			foreach(IUpdatable updateable in mySubcomponents)
				updateable.update();
		}
	}
}

