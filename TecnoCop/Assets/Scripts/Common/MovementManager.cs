using UnityEngine;
using System.Collections;

namespace TecnoCop{
	public interface MovementManager {
		/// <summary>
		/// Tenta sobreescrever a velocidade no eixo X
		/// Caso uma tentativa de sobreescrita ja tenha sido realizada neste frame, prevalece a com maior prioridade
		/// </summary>
		void setVelocity_x(float velocity, int priority);
		
		/// <summary>
		/// Tenta sobreescrever a velocidade no eixo y
		/// Caso uma tentativa de sobreescrita ja tenha sido realizada neste frame, prevalece a com maior prioridade
		/// </summary>
		void setVelocity_y(float velocity, int priority);
	}

}
