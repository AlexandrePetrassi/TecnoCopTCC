using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generator scenario. Gera um cenario a partir de uma imagem bmp. cada pixel eh equivalente a um tile.
/// Tambem gera colliders para cada tile. Colliders adjacentes sao mesclados em colliders maiores.
/// </summary>
namespace TecnoCop{
	namespace Collisions{
		public class ScenarioManager : MonoBehaviour {
			
			public Texture2D                     map;                                            // Textura que eh interpretada como tilemap
			public List<Color>                   colors;                                         // Array de cores que sao traduzidas para tiles
			public List<GameObject>              tiles;                                          // Tile correspondente a cor passada pelo tilemap
			private Dictionary<Color,GameObject> colorTile = new Dictionary<Color,GameObject>(); // Dicionario que relaciona Cor->Tile
			private List<Precollider>			 precolliders   = new List<Precollider>();       // Lista de colliders em formaçao

			/// <summary>
			/// Inicializa o cenario
			/// </summary>
			void Awake () {
				buildDictionaries();
				buildScenarioAndColliderMap();
				joinPrecollidersVertically();
				buildColliders();
			}
			
			/// <summary>
			/// Inicializa e constroi o dicionario utilizando os arrays colors, tiles e colliders como referencia
			/// </summary>
			void buildDictionaries(){
				for(int i = 0; i < colors.Count; ++i){
					colorTile.Add(colors[i],tiles[i]);
				}
			}
			
			/// <summary>
			/// Constroi o cenario em si, instanciando os prefabs que o compoe
			/// </summary>
			void buildScenarioAndColliderMap(){
				GameObject scenario = new GameObject();
				scenario.name = map.name;
				for(int y = 0; y < map.height; ++y){
					for(int x = 0; x < map.width; ++x ){ // lendo textura
						GameObject tilePrefab = getTile(map.GetPixel(x,y));
						GameObject tile = Instantiate(tilePrefab,new Vector3(x,y,0),Quaternion.identity) as GameObject;
						if(isBlocked(tile)) addPrecollider(x,y);
						tile.transform.parent = scenario.transform;
					}
				}
			}
			
			/// <summary>
			/// adciona o precollider a lista de precolliders, mesclando horizontalmente precolliders adjacentes caso seja possivel
			/// </summary>
			/// <param name="x">The x coordinate.</param>
			/// <param name="y">The y coordinate.</param>
			void addPrecollider(int x,int y){
				foreach(Precollider precollider in precolliders){
					if(precollider.bounds.xMax == x && precollider.bounds.yMin == y){
						precollider.bounds.Set(
							precollider.bounds.xMin,
							precollider.bounds.yMin,
							precollider.bounds.width + 1,
							1
							);
						return;
					}
				}
				precolliders.Add(new Precollider(x,y,1,1));
			}
			
			/// <summary>
			/// Apos a lista de precolliders estar pronta, este metodo mescla precolliders verticalmente
			/// </summary>
			void joinPrecollidersVertically(){
				for(int i = 0; i < precolliders.Count; ++i){
					Precollider precollider = precolliders[i];
					for(int j = i+1; j < precolliders.Count; ++j){
						Precollider nextPc = precolliders[j];
						if(precollider.isVerticallyJoinableWith(nextPc)){
							nextPc.bounds.yMin = precollider.bounds.yMin;
							precollider.joined = true;
							break;
						}
					}
				}
			}
			
			/// <summary>
			/// Retorna o tile correspondente a uma cor
			/// </summary>
			/// <returns>tile.</returns>
			/// <param name="color">Color.</param>
			GameObject getTile(Color color){ 
				foreach(var pair in colorTile)
					if(pair.Key == color) return pair.Value;
				return tiles[0];
			}
			
			/// <summary>
			/// Checa se o tile esta bloqueado, ou seja, se ele contem um collider
			/// </summary>
			/// <returns><c>true</c>, if tile is blocked, <c>false</c> otherwise.</returns>
			/// <param name="tile">Tile.</param>
			bool isBlocked(GameObject tile){
				Tile col = tile.GetComponent<Tile>();
				if(col == null) return false;
				return col.collideable;
			}
			
			/// <summary>
			/// Instancia os colliders em si, usando a lista de precolliders mesclados como referencia
			/// </summary>
			void buildColliders(){
				GameObject ColliderGroupment = new GameObject(); // Gameobject usado para agrupar todos os Colliders solidos
				ColliderGroupment.name = "Colliders";
				foreach(Precollider precollider in precolliders){
					if(precollider == null || precollider.joined) continue;
					precollider.build().transform.parent = ColliderGroupment.transform;
				}
			}
			
			/// Classe interna utilizada para calcular a posiçao e mesclar colliders antes que sejam instanciados
			private class Precollider{
				
				public Rect bounds; // Dimençoes do collider
				public bool joined; // Marca se o collider ja foi mesclado a outro
				
				/// <summary>
				/// Incializa o precollider
				/// </summary>
				public Precollider(int left, int up, int right, int down){
					bounds = new Rect(left,up,right,down);
					if(bounds.x == 0) return;
				}
				
				/// <summary>
				/// Retorna a posiçao central do collider
				/// </summary>
				/// <returns>The position.</returns>
				public Vector3 getPosition(){
					return new Vector3(
						bounds.xMin + bounds.width/2 - 0.5f,
						bounds.yMin + bounds.height/2 - 0.5f,
						0
						);
				}
				
				/// <summary>
				/// Checa se este precollider pode ser mesclado verticalmente com outro collider.
				/// </summary>
				public bool isVerticallyJoinableWith(Precollider precollider){
					if(bounds.yMin + bounds.height == precollider.bounds.yMin && bounds.xMin == precollider.bounds.xMin && bounds.xMax == precollider.bounds.xMax)
						return true;
					return false;
				}
				
				/// <summary>
				/// Constroi o BoxCollider2D
				/// </summary>
				public GameObject build(){
					GameObject newCollider = new GameObject();
					newCollider.transform.position = getPosition();
					BoxCollider2D component = newCollider.AddComponent<BoxCollider2D>();
					component.size = new Vector2(bounds.width,bounds.height);
					newCollider.name = "Collider (" + newCollider.transform.position.x + "," + newCollider.transform.position.y + ")";
					return newCollider;
				}
			}
		}
	}
}
