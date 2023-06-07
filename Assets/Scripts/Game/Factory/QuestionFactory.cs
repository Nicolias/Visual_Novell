using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Factory.Quiz
{
    public class QuestionFactory
    {
        private Dictionary<CharacterType, Queue<QuizElement>> _characterQuizQustions = new();

        public QuizElement GetQuestion(CharacterType characterType)
        {
            var result = _characterQuizQustions[characterType].Dequeue();
            _characterQuizQustions[characterType].Enqueue(result);

            if (Random.Range(0, _characterQuizQustions[characterType].Count) == 0)
                Shuffle(characterType);

            return result;
        }

        public void AddQuestionElement(QuizElement quizElement)
        {
            if (_characterQuizQustions.ContainsKey(quizElement.CharacterType))
            {
                _characterQuizQustions[quizElement.CharacterType].Enqueue(quizElement);
            }
            else
            {
                if (quizElement.CharacterType != CharacterType.All)
                {
                    Queue<QuizElement> quizeQueue = new();
                    quizeQueue.Enqueue(quizElement);
                    _characterQuizQustions.Add(quizElement.CharacterType, quizeQueue);
                }
                else
                {
                    foreach (var key in _characterQuizQustions.Keys)
                        _characterQuizQustions[key].Enqueue(quizElement);
                }
            }
        }

        private void Shuffle(CharacterType characterType)
        {
            List<QuizElement> questionList = _characterQuizQustions[characterType].ToList();

            var rnd = new System.Random();
            _characterQuizQustions[characterType] = new(questionList.OrderBy(c => rnd.NextDouble()).ToList());
        }
    }
}
