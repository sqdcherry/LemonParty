//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using Dan.Main;

//public class Leaderboard : MonoBehaviour
//{
//    [SerializeField] private TMP_Text[] _entryTextObjects;
//    [SerializeField] private TMP_InputField _userName;

//    //[SerializeField] private ExampleGame _exampleGame;
//    //private int Score => _exampleGame.Score;

//    private void Start()
//    {
//        LoadEntries();
//    }

//    private void LoadEntries()
//    {
//        Leaderboards.LeaderBoard.GetEntries(entries =>
//        {
//            foreach (var t in _entryTextObjects)
//                t.text = "";

//            var lengt = Mathf.Min(_entryTextObjects.Length, entries.Length);
//            for (int i = 0; i < lengt; i++)
//                _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";
//        });
//    }

//    private void UploadEntries()
//    {
//        Leaderboards.LeaderBoard.UploadNewEntry(_userName.text, Score, isSuccessful =>
//        {
//            if (isSuccessful)
//                LoadEntries();
//        });
//    }
//}
