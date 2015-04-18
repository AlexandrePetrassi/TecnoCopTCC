using UnityEngine;
using System.Collections;

namespace TecnoCop{
	namespace Collisions{
		public class Paralax : MonoBehaviour {
			public Vector2 intesisty;
			
			// Update is called once per frame
			void Update () {
				Vector3 cp = Camera.main.transform.position;
				GetComponent<Renderer>().material.SetTextureOffset("_MainTex",new Vector2(cp.x*intesisty.x - transform.position.x*0.03125f,cp.y*intesisty.y - transform.position.y*0.03125f));
			}
		}
	}
}
