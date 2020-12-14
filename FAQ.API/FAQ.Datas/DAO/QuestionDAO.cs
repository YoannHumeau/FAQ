﻿using FAQ.Datas.DataAccess;
using FAQ.Datas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FAQ.Datas.DAO
{
    public class MySuperClass
    {
        public string Title { get; set; }
    }

    internal class QuestionDAO
    {
        private readonly FAQContext _faqContext;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="faqContext"></param>
        internal QuestionDAO(FAQContext faqContext)
        {
            _faqContext = faqContext;
        }

        /// <summary>
        /// Get questions
        /// </summary>
        /// <returns>List of <see cref="QuestionModel"/></returns>
        internal IEnumerable<QuestionModel> GetQuestions(string language)
        {
            var result = _faqContext.Questions
                .Include(q => q.QuestionTranslates.Where(qt => qt.Language == language))
                .ToList();

            return result;
        }

        /// <summary>
        /// Get a specific question by the id
        /// </summary>
        /// <param name="id">Id of the question</param>
        /// <returns><see cref="QuestionModel"/></returns>
        internal QuestionModel GetQuestion(string language, int id)
        {
            return _faqContext.Questions
                .Include(q => q.QuestionTranslates.Where(qt => qt.Language == language))
                .FirstOrDefault(q => q.Id == id)
                ;
        }

        /// <summary>
        /// Create a question
        /// </summary>
        /// <param name="question"><see cref="QuestionModel"/> Question to create</param>
        internal void CreateQuestion(QuestionModel question)
        {
            _faqContext.Questions.Add(question);

            try
            {
                _faqContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Remove a question from  the id
        /// </summary>
        /// <param name="question"><see cref="QuestionModel"/> Question to remove</param>
        internal void RemoveQuestion(QuestionModel question)
        {
            _faqContext.Questions.Remove(question);

            try
            {
                _faqContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal QuestionTranslateModel GetQuestionTranslate(string language, int questionParentId)
        {
            return _faqContext.QuestionsTranslates.Where(qt => qt.Language == language && qt.QuestionModelId == questionParentId).FirstOrDefault();
        }

        internal void RemoveQuestionTranslate(QuestionTranslateModel questionTranslate)
        {
            _faqContext.QuestionsTranslates.Remove(questionTranslate);

            try
            {
                _faqContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
