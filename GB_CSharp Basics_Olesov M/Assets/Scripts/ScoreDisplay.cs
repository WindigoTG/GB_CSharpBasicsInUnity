using UnityEngine.UI;
using UnityEngine;

namespace BallGame
{
    public class ScoreDisplay
    {
        private Text _text;
        public ScoreDisplay()
        {
            _text = Object.FindObjectOfType<Text>();
        }

        public void Display(int value)
        {
            _text.text = $"Вы набрали {value}";
        }

    }
}