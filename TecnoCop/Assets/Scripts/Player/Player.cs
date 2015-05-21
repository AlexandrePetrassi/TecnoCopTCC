using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TecnoCop{
	namespace PlayerControl{
		/// <summary>
		/// Player.
		/// Classe responsavel por gerenciar o jogador.
		/// Chama o metodo update de todos os playerTriggers associados a ele
		/// </summary>
		public class Player : ModuleManager, IUpdatable{

			static public Direction bornPosition = Direction.left;
			private List<PlayerTrigger> myUpdateables = new List<PlayerTrigger>();
			public static bool doubleJumpSkill = false;
			public static bool wallJumpSkill = false;
			public static bool airDashSkill = false;
			public static bool fastChargeSkill = false;
			public static bool fastDashSkill = false;
			public static bool fastWalkSkill = false;

			/// <summary>
			/// Awake this instance.
			/// </summary>
			void Awake () {
				myPlayer = this;
				GetComponents<PlayerTrigger>(myUpdateables);
				myUpdateables.Reverse(); // Isso eh temporario. Implementar algoritmo para ordenar a lista de forma correta
				DontDestroyOnLoad(gameObject);
			}
			
			/// <summary>
			/// Update this instance.
			/// </summary>
			public void update () {
				foreach(PlayerTrigger updateable in myUpdateables)
					updateable.update();
			}

			//----------------------------------//

			// triggerables
			static private Player           myPlayer;
			static private Move             myMove;
			static private Jump             myJump;
			static private Dash             myDash;
			static private WallStick        myWall;
			static private WallJump         myWallJump;

			static public Player player{
				get{
					return myPlayer;
				}
				set{
					if(myPlayer!=null){
						Destroy(myPlayer.gameObject);
					}
					myPlayer = value;
				}
			}
			
			static public Jump jump{
				get{
					if(myJump == null)
						myJump = player.GetComponent<Jump>();
					return myJump;
				}
				set{
					myJump = value;
				}
			}
			
			static public WallStick wallStick{
				get{
					if(myWall == null)
						myWall = player.GetComponent<WallStick>();
					return myWall;
				}
				set{
					myWall = value;
				}
			}
			
			static public WallJump wallJump{
				get{
					if(myWallJump == null)
						myWallJump = player.GetComponent<WallJump>();
					return myWallJump;
				}
				set{
					myWallJump = value;
				}
			}
			
			static public Dash dash{
				get{
					if(myDash == null)
						myDash = player.GetComponent<Dash>();
					return myDash;
				}
				set{
					myDash = value;
				}
			}
		}
	}
	public enum Direction{
		left,
		right,
		up,
		down
	}
}