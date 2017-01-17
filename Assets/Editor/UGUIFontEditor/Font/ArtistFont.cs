using UnityEngine;
using System.Collections;
using UnityEditor;

public class ArtistFont : MonoBehaviour
{
    /*public static void BatchCreateArtistFont()
	{
		string dirName = "";
		string fntname = EditorUtils.SelectObjectPathInfo(ref dirName).Split('.')[0];
		Debug.Log(fntname);
		Debug.Log(dirName);

		string fntFileName = dirName + fntname + ".fnt";
		
		Font CustomFont = new Font();
		{
			AssetDatabase.CreateAsset(CustomFont, dirName + fntname + ".fontsettings");
			AssetDatabase.SaveAssets();
		}

		TextAsset BMFontText = null;
		{
			BMFontText = AssetDatabase.LoadAssetAtPath(fntFileName, typeof(TextAsset)) as TextAsset;
		}

		BMFont mbFont = new BMFont();
		BMFontReader.Load(mbFont, BMFontText.name, BMFontText.bytes);  // 借用NGUI封装的读取类
		CharacterInfo[] characterInfo = new CharacterInfo[mbFont.glyphs.Count];
		for (int i = 0; i < mbFont.glyphs.Count; i++)
		{
			BMGlyph bmInfo = mbFont.glyphs[i];
			CharacterInfo info = new CharacterInfo();
			info.index = bmInfo.index;
			info.uv.x = (float)bmInfo.x / (float)mbFont.texWidth;
			info.uv.y = 1 - (float)bmInfo.y / (float)mbFont.texHeight;
			info.uv.width = (float)bmInfo.width / (float)mbFont.texWidth;
			info.uv.height = -1f * (float)bmInfo.height / (float)mbFont.texHeight;
			info.vert.x = (float)bmInfo.offsetX;
			info.vert.y = (float)bmInfo.offsetY;
			info.vert.width = (float)bmInfo.width;
			info.vert.height = (float)bmInfo.height;
			info.width = (float)bmInfo.advance;
			characterInfo[i] = info;
		}
		CustomFont.characterInfo = characterInfo;


		string textureFilename = dirName + mbFont.spriteName + ".png";
		Material mat = null;
		{
			Shader shader = Shader.Find("Transparent/Diffuse");
			mat = new Material(shader);
			Texture tex = AssetDatabase.LoadAssetAtPath(textureFilename, typeof(Texture)) as Texture;
			mat.SetTexture("_MainTex", tex);
			AssetDatabase.CreateAsset(mat, dirName + fntname + ".mat");
			AssetDatabase.SaveAssets();
		}
		CustomFont.material = mat;
	}
    */
    public static void CreateCocos2dxStyleBmpFont()
    {
        string dirName = "";
        string pngname = EditorUtils.SelectObjectPathInfo(ref dirName).Split('.')[0];
        Debug.Log(pngname);
        Debug.Log(dirName);

        string pngFileName = dirName + pngname + ".png";

        Font CustomFont = new Font();
        {
            AssetDatabase.CreateAsset(CustomFont, dirName + pngname + ".fontsettings");
            AssetDatabase.SaveAssets();
        }

        Texture2D BMFontTexture = null;
        {
            BMFontTexture = AssetDatabase.LoadAssetAtPath(pngFileName, typeof(Texture2D)) as Texture2D;
        }

        CharacterInfo[] characterInfo = new CharacterInfo[12];
        float singleCharacterWidth = BMFontTexture.width / 12f;
        float singleCharacterHieght = BMFontTexture.height;
        for (int i = 0; i < 12; i++)
        {
            CharacterInfo info = new CharacterInfo();
            info.index = 46 + i;

            info.uvBottomLeft = new Vector2((float)1f / 12f * i, 0f);
            info.uvBottomRight = new Vector2((float)1f / 12f * (i+1), 0f);
            info.uvTopLeft = new Vector2((float)1f / 12f * i, 1f);
            info.uvTopRight = new Vector2((float)1f / 12f * (i+1), 1f);

            info.minX = 0;
            info.minY = 0;
            info.maxX = (int)singleCharacterWidth;
            info.maxY = (int)singleCharacterHieght;
            info.advance = (int)singleCharacterWidth;
            characterInfo[i] = info;
        }
        CustomFont.characterInfo = characterInfo;


        string textureFilename = pngFileName;
        Material mat = null;
        {
            Shader shader = Shader.Find("Transparent/Diffuse");
            mat = new Material(shader);
            Texture tex = AssetDatabase.LoadAssetAtPath(textureFilename, typeof(Texture)) as Texture;
            mat.SetTexture("_MainTex", tex);
            AssetDatabase.CreateAsset(mat, dirName + pngname + ".mat");
            AssetDatabase.SaveAssets();
        }
        CustomFont.material = mat;

        AssetDatabase.SaveAssets();
    }
}
