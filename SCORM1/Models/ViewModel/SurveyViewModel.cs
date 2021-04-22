using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCORM1.Models.Survey;
using SCORM1.Models.Lms;

namespace SCORM1.Models.ViewModel
{
    public class SurveyViewModel : BaseViewModel
    {
        public SurveyModule survey { get; set; }
        public int enro_id { get; set; }
        public float calification { get; set; }
        public SurveyQuestionBank questionBank { get; set; }
        public List<MultiOptionSurveyQuestion> multipleOptionQuestions { get; set; }
        public List<TrueFalseSurveyQuestion> trueFalseSurveyQuestions { get; set; }
        public List<MultipleOptionAnswer> userAnswers { get; set; }
        public List<TrueFalseSurveyAnswer> userAnswerTrueFalse { get; set; }
        public bool validSession { get; set; }
    }

    public class MultiOptionSurveyQuestion
    {
        public MultipleOptionsSurveyQuestion question { get; set; }
        public List<MultipleOptionsSurveyAnswer> answers { get; set; }
    }

    public class TrueFalseSurveyAnswer
    {
        public int questionId { get; set; }
        public int value { get; set; }
    }

    public class MultipleOptionAnswer
    {
        public int questionId { get; set; }
        public int answerId { get; set; }
    }
}