using System.Collections.Generic;
using System.Linq;
using System;

namespace Factory.Quiz
{
    public class QuestionFactory
    {
        Random _random = new Random();
        private Dictionary<CharacterType, Queue<QuizQuestion>> _charactersQuizQustions;

        public QuestionFactory()
        {
            _charactersQuizQustions = new Dictionary<CharacterType, Queue<QuizQuestion>>()
            {
                {CharacterType.Dey, new Queue<QuizQuestion>() },
                {CharacterType.Liza, new Queue<QuizQuestion>() }
            };
        }

        public void AddQuestionElement(QuizQuestion quizElement)
        {
            if (_charactersQuizQustions.ContainsKey(quizElement.CharacterType))
                _charactersQuizQustions[quizElement.CharacterType].Enqueue(quizElement);
            else
                foreach (CharacterType character in _charactersQuizQustions.Keys)
                    _charactersQuizQustions[character].Enqueue(quizElement);
        }

        public QuizQuestion GetQuestion(CharacterType characterType)
        {
            Shuffle(characterType);

            var result = _charactersQuizQustions[characterType].Dequeue();
            _charactersQuizQustions[characterType].Enqueue(result);

            return result;
        }

        private void Shuffle(CharacterType characterType)
        {
            List<QuizQuestion> questionList = _charactersQuizQustions[characterType].ToList();

            _charactersQuizQustions[characterType] = new(questionList.OrderBy(c => _random.NextDouble()).ToList());
        }
    }
}
