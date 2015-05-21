using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;
using TecnoCop.Collisions;

namespace TecnoCop{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(CollisionManager))]
	//[RequireComponent(typeof(DamageManager))]

	public abstract class ModuleManager : MonoBehaviour {
		
		// not triggerables
		//private static Player    myPlayer;
		private Character        myUpdater;
		private Rigidbody2D      myRB;
		private Animator         myAnim;
		private SpriteRenderer   mySprite;
		private AnimationManager myAnimMan; 
		private CollisionManager myCollision;
		private DamageManager    myDamager;
		private Knockback        myKnockback;
		private MovementManager  myMove;
		private AudioSource      myAudio;

		public Character updater{
			get{
				if(myUpdater == null)
					myUpdater = GetComponent<Character>();
				return myUpdater;
			}
			set{
				myUpdater = value;
			}
		}
		
		public Rigidbody2D ribo{
			get{
				if(myRB == null)
					myRB = GetComponent<Rigidbody2D>();
				return myRB;
			}
			set{
				myRB = value;
			}
		}
		
		public Animator animator{
			get{
				if(myAnim == null)
					myAnim = GetComponent<Animator>();
				return myAnim;
			}
			set{
				myAnim = value;
			}
		}
		
		public SpriteRenderer spriteRenderer{
			get{
				if(mySprite == null)
					mySprite = GetComponent<SpriteRenderer>();
				return mySprite;
			}
			set{
				mySprite = value;
			}
		}
		
		public AnimationManager anim{
			get{
				if(myAnimMan == null)
					myAnimMan = GetComponent<AnimationManager>();
				return myAnimMan;
			}
			set{
				myAnimMan = value;
			}
		}
		
		public CollisionManager collision{
			get{
				if(myCollision == null)
					myCollision = GetComponent<CollisionManager>();
				return myCollision;
			}
			set{
				myCollision = value;
			}
		}

		public DamageManager damager{
			get{
				if(myDamager == null)
					myDamager = GetComponent<DamageManager>();
				return myDamager;
			}
			set{
				myDamager = value;
			}
		}

		public Knockback knockback{
			get{
				if(myKnockback == null)
					myKnockback = GetComponent<Knockback>();
				return myKnockback;
			}
			set{
				myKnockback = value;
			}
		}

		public MovementManager move{
			get{
				if(myMove == null)
					myMove = GetComponent<MovementManager>();
				return myMove;
			}
			set{
				myMove = value;
			}
		}

		public AudioSource audioS{
			get{
				if(myAudio == null)
					myAudio = GetComponent<AudioSource>();
				return myAudio;
			}
			set{
				myAudio = value;
			}
		}
	}
}

