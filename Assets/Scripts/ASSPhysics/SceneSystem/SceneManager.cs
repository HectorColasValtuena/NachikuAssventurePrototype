using UnityEngine.SceneManagement;

namespace ASSPhysics.SceneSystem
{
	public static class ASSceneManager 
	{
	//Constant definitions
		private static class SceneNumbers
		{
			public const int LAUNCHER = 0;	//unused, included for consistency
			public const int CURTAINS = 1;
			public const int MAINMENU = 2;
		}
	//ENDOF Constant definitions

	//private fields and properties
		//private static CurtainsController curtainsController { get { return CurtainsController.instance; }}
	//ENDOF private fields and properties

	//Public methods
		public static void Initialize ()
		{
			SceneManager.LoadScene(SceneNumbers.CURTAINS, LoadSceneMode.Additive);
		}
	//ENDOF public methods
	}
}