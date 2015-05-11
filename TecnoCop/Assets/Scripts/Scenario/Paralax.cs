using UnityEngine;
using System.Collections;

namespace TecnoCop{
	namespace Collisions{
		public class Paralax : MonoBehaviour {
			public Vector2 intesisty;
			private Renderer myRenderer;


			void Start(){
				myRenderer = GetComponent<Renderer>();
			}
			// Update is called once per frame
			void Update () {
				Vector3 cp = Camera.main.transform.position;
				myRenderer.material.SetTextureOffset("_MainTex",new Vector2(cp.x*intesisty.x + transform.position.x *0.0625f,(transform.position.y-cp.y)*0.0625f +0.45f));
			}
		}
	}
}
