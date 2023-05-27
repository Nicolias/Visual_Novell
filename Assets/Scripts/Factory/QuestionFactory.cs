using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Factory.Quiz
{
    public class QuestionFactory
    {
        private Dictionary<CharacterType, List<QuizElement>> _characterQuizQustions;

        public QuizElement GetQuestion(CharacterType characterType)
        {
            List<QuizElement> quizElements = _characterQuizQustions[characterType];

            return quizElements[Random.Range(0, quizElements.Count)];
        }

        public void AddQuestionElement(CharacterType characterType, QuizElement quizElement)
        {
            if (_characterQuizQustions.ContainsKey(characterType))
                _characterQuizQustions[characterType].Add(quizElement);
            else
                _characterQuizQustions.Add(characterType, new() { quizElement});
        }
    }
}