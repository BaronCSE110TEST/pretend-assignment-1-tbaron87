using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class gui_load : MonoBehaviour {

	public GUISkin style_skin;
	public Texture lego_brick, corner_brick;
	public Texture2D play, settings, exit;
    public GameObject info;

    private bool infoShowing = false;

	void Start(){
		log_to_file (Application.dataPath + "/resolutions_logfile.txt");

		play = Resources.Load ("gui4/png/button/512/100", typeof(Texture2D)) as Texture2D;
		exit = Resources.Load ("gui4/png/button/512/83", typeof(Texture2D)) as Texture2D;
		settings = Resources.Load ("gui4/png/button/512/84", typeof(Texture2D)) as Texture2D;
	}

	void OnGUI( ){
		Rect gui_mod = gui_modified_screen_rect ();

		GUI.skin = style_skin;
		//GUI.skin.button.padding.left = (int)gui_mod.width / 6;
		if (AspectUtility.screenHeight > 1200)
			GUI.skin.button.fontSize = 48;

		GUI.BeginGroup (AspectUtility.screenRect);

		GUI.Box (gui_mod , GameConstants.version);

		gui_mod.x += gui_mod.width / 8.0f;
		gui_mod.y += gui_mod.height / 8.0f;
		gui_mod.height = gui_mod.height / 8.0f;
		gui_mod.width = 3.0f * gui_mod.width / 4.0f;

		GUI.Label (gui_mod, "Vector Unknown");

		gui_mod.y += gui_mod.height / 4.0f + gui_mod.height;

		if( GUI.Button( gui_mod,"Play")){
			SceneManager.LoadScene ( "level_load_scene");
		}

		draw_bricks (gui_mod);

		gui_mod.y += gui_mod.height / 4.0f + gui_mod.height;

        if (GUI.Button(gui_mod, "Instructions"))
        {
            if(infoShowing)
            {
                info.SetActive(false);
                infoShowing = false;
            }
            else
            {
                info.SetActive(true);
                infoShowing = true;
            }
        }

        draw_bricks(gui_mod);

        gui_mod.y += gui_mod.height / 4.0f + gui_mod.height;

        if ( GUI.Button( gui_mod, "Settings")){
			SceneManager.LoadScene ("settings_scene");
		}

		draw_bricks (gui_mod);

		gui_mod.y += gui_mod.height / 4.0f + gui_mod.height;

		if( GUI.Button( gui_mod, "EXIT")){
			Application.Quit();	
		}

		draw_bricks (gui_mod);

		GUI.EndGroup ();
	}

	void draw_bricks( Rect guide){
		Rect brick_location = new Rect();

		brick_location.height = guide.height * 1.2f;
		brick_location.width = guide.width / 8.0f;
		brick_location.x = guide.x - brick_location.width / 12f;
		brick_location.y = guide.y  - brick_location.height / 12f;

		GUI.DrawTexture (brick_location, lego_brick);

		brick_location.x += guide.width - (brick_location.width / 2);

		GUI.DrawTexture (brick_location, lego_brick);
	}

	private Rect gui_modified_screen_rect(){
		Rect base_rect = AspectUtility.screenRect;

		float x_rect = base_rect.x + AspectUtility.screenWidth * ( 2f / 3f)  - AspectUtility.screenWidth * ( 1f / 10f);
		float y_rect = base_rect.y + AspectUtility.screenHeight * (1f / 10f);
		float g_width = AspectUtility.screenWidth * (1f / 3f);
		float h_width = AspectUtility.screenHeight * (8f / 10f);

		return new Rect ( x_rect, y_rect, g_width, h_width);

	}

	private void log_to_file(string path){
		StreamWriter clear_path = new StreamWriter (path);
		clear_path.Write ("");
		clear_path.Close ();

		StreamWriter writer = new StreamWriter (path, true);

		writer.WriteLine ("data path: " + Application.dataPath);
		writer.WriteLine ("Fullscreen: " + Screen.fullScreen);
		writer.WriteLine ("width: " + AspectUtility.screenWidth + " pixels");
		writer.WriteLine ("height: " + AspectUtility.screenHeight + " pixels");
		writer.WriteLine ("x offset: " + AspectUtility.xOffset + " pixels");
		writer.WriteLine ("y offset: " + AspectUtility.yOffset + " pixels");
		writer.WriteLine ("Screen Rectangle: " + AspectUtility.screenRect);
		writer.WriteLine ("resolutions available: ");
		Resolution[] res = Screen.resolutions;
		foreach (Resolution re in res)
			writer.WriteLine ("\t"+re.ToString());
		writer.WriteLine ("current resolution: " + Screen.currentResolution.ToString());

		writer.Close ();
	}
}
