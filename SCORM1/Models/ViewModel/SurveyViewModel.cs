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
        public int minutes { get; set; }
        public int enro_id { get; set; }
        public float calification { get; set; }
        public SurveyQuestionBank questionBank { get; set; }
        public List<MultiOptionSurveyQuestion> multipleOptionQuestions { get; set; }
        //public List<TrueFalseSurveyQuestion> trueFalseSurveyQuestions { get; set; }
        public List<TrueFalseSurveyAnswer> userAnswerTrueFalse { get; set; }
        public bool validSession { get; set; }
    }

    public class MultiOptionSurveyQuestion
    {
        public MultipleOptionsSurveyQuestion question { get; set; }
        public List<MultipleOptionsSurveyAnswer> answers { get; set; }
        public int userAnswerId { get; set; }
    }

    public class TrueFalseSurveyAnswer
    {
        public TrueFalseSurveyQuestion question { get; set; }
        public int userAnswerValue { get; set; }
    }
}