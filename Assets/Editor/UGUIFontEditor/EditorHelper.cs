using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class EditorHelper : MonoBehaviour {

	//[MenuItem("Assets/BatchCreateArtistFont")]
    
    //static public void BatchCreateArtistFont()
	//{
	//	ArtistFont.BatchCreateArtistFont();
	//}

    [MenuItem("Assets/CreateCocos2dxStyleBmpFont")]
    static public void CreateCocos2dxStyleBmpFont()
    {
        ArtistFont.CreateCocos2dxStyleBmpFont();
    }
}
