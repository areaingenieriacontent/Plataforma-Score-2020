using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCORM1.Models.Survey;
using SCORM1.Models.Lms;

namespace SCORM1.Models.ViewModel
{
    public class SurveyViewModel: BaseViewModel
    {
        public SurveyModule survey { get; set; }
        public SurveyQuestionBank questionBank {get;set;}
        public List<MultiOptionSurveyQuestion> multipleOptionQuestions;
        public List<MultipleOptionsSurveyAnswer> userAnswers;
        public List<TrueFalseSurveyQuestion> trueFalseSurveyQuestions;
        public List<TrueFalseSurveyQuestion> userAnswerTrueFalse;

    }

    public class MultiOptionSurveyQuestion
    {
        public MultipleOptionsSurveyQuestion question;
        public List<MultipleOptionsSurveyAnswer> answers;
    }
}