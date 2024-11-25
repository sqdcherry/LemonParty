using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;
    [SerializeField] private TMP_Text[] _entryTextObjects;
    [SerializeField] private string _userName;

    private int Score;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        LoadEntries();
    }

    public void LoadEntries()
    {
        Leaderboards.LeaderBoard.GetEntries(entries =>
        {
            foreach (var t in _entryTextObjects)
                t.text = "";

            var lengt = Mathf.Min(_entryTextObjects.Length, entries.Length);
            for (int i = 0; i < lengt; i++)
                _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";
        });
    }

    public void UploadEntries(string userName, int score)
    {
        Leaderboards.LeaderBoard.UploadNewEntry(userName, score, isSuccessful =>
        {
            if (isSuccessful)
                LoadEntries();
        });
    }
}
