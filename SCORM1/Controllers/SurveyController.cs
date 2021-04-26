using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCORM1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SCORM1.Models.ViewModel;
using SCORM1.Models.Survey;
using SCORM1.Models.Lms;

namespace SCORM1.Controllers
{
    public class SurveyController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public SurveyController()
        {
            ApplicationDbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }
        public ApplicationUser GetActualUserId()
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            return user;
        }

        // GET: Survey
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Load a survey by its asociated module, it also instantiate the user session
        /// </summary>
        /// <param name="id">id of the survey</param>
        /// <returns>Survey view with an instantiated model</returns>
        [Authorize]
        public ActionResult Survey(int id)
        {
            string userId = GetActualUserId().Id;
            //Check if user already has a session, if the user has one, load it
            //------------------------
            SurveyModule survey = ApplicationDbContext.Surveys.Where(x => x.survey_id == id).FirstOrDefault();
            Module module = ApplicationDbContext.Modules.Find(survey.modu_id);
            Enrollment enrollment = ApplicationDbContext.Enrollments.Where(x => x.User_Id == userId && x.Modu_Id == module.Modu_Id).FirstOrDefault();
            UserSurveyResponse usr = GetUserSurvey(enrollment.Enro_Id);
            int minutes = 0;
            //if the user down't have a session, then create it
            if (usr == null)
            {
                minutes = survey.survey_time_minutes;
                usr = new UserSurveyResponse
                {
                    calification = 0,
                    enro_id = enrollment.Enro_Id,
                    survey_initial_time = DateTime.Now,
                    survey_finish_time = DateTime.Now.AddMinutes(survey.survey_time_minutes)
                };
                ApplicationDbContext.UserSurveyResponses.Add(usr);
                ApplicationDbContext.SaveChanges();
            }
            //if he has, then calculate the remaining time
            else
            {
                minutes = (int)(usr.survey_finish_time - DateTime.Now).TotalMinutes;
                if (minutes < 0)
                    minutes = 0;
            }
            //Else create a session and load whole survey
            SurveyQuestionBank sqb = ApplicationDbContext.SurveyQuestionBanks.Where(x => x.survey_id == survey.survey_id).FirstOrDefault();
            List<MultipleOptionsSurveyQuestion> mosq = ApplicationDbContext.MultipleOptionsSurveyQuestions.Where(x => x.bank_id == sqb.bank_id).ToList();
            List<MultipleOptionsSurveyAnswer> mosa = ApplicationDbContext.MultipleOptionsSurveyAnswers.ToList();//Check just load answers of questions
            List<TrueFalseSurveyQuestion> tfsq = ApplicationDbContext.TrueFalseSurveyQuestions.Where(x => x.bank_id == sqb.bank_id).ToList();

            //Populate the questions of the viewmodelto use in the view
            List<MultiOptionSurveyQuestion> listOfQuestions = new List<MultiOptionSurveyQuestion>();
            List<TrueFalseSurveyQuestion> listOfTFQuestions = new List<TrueFalseSurveyQuestion>();

            //These are for storing users answers
            List<TrueFalseSurveyAnswer> tfa = new List<TrueFalseSurveyAnswer>();

            //Populate multiple option questions and prepare user answers
            for (int cont = 0; cont < mosq.Count; cont++)
            {
                MultiOptionSurveyQuestion questionToAdd = new MultiOptionSurveyQuestion();
                questionToAdd.answers = new List<MultipleOptionsSurveyAnswer>();
                questionToAdd.question = mosq[cont];
                questionToAdd.userAnswerId = 0;
                for (int cont2 = 0; cont2 < mosa.Count; cont2++)
                {
                    if (mosa[cont2].mosq_id == mosq[cont].mosq_id)
                    {
                        questionToAdd.answers.Add(mosa[cont2]);
                    }
                }
                listOfQuestions.Add(questionToAdd);
            }

            //Shuffle multipleOptionQuestions
            listOfQuestions.Shuffle();

            //Populate True False questions and prepare user answers
            for (int cont = 0; cont < tfsq.Count; cont++)
            {
                listOfTFQuestions.Add(tfsq[cont]);
                TrueFalseSurveyAnswer a = new TrueFalseSurveyAnswer
                {
                    question = tfsq[cont],
                    userAnswerValue=2
                };
                tfa.Add(a);
            }

            SurveyViewModel model = new SurveyViewModel
            {
                survey = survey,
                questionBank = sqb,
                calification = usr.calification,
                multipleOptionQuestions = listOfQuestions,
                userAnswerTrueFalse = tfa,
                minutes = minutes
            };

            if (usr.survey_finish_time > DateTime.Now)
            {
                model.validSession = true;
            }
            else
            {
                model.validSession = false;
            }
            model.Sesion = GetActualUserId().SesionUser;
            return View(model);
        }

        public UserSurveyResponse GetUserSurvey(int enrollMentId)
        {
            if (ApplicationDbContext.UserSurveyResponses.Any(x => x.enro_id == enrollMentId))
            {
                UserSurveyResponse usr = ApplicationDbContext.UserSurveyResponses.Where(x => x.enro_id == enrollMentId).OrderBy(x => x.survey_initial_time).FirstOrDefault();
                return usr;
            }
            else
            {
                return null;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult SurveyResponse(SurveyViewModel model)
        {
            if (model.validSession)
            {
                //To-Doi validate that answers are not in database
                UserSurveyResponse usr = ApplicationDbContext.UserSurveyResponses.Where(x => x.enro_id == model.enro_id).FirstOrDefault();
                Enrollment en = ApplicationDbContext.Enrollments.Find(usr.enro_id);
                Module mod = ApplicationDbContext.Modules.Find(en.Modu_Id);
                SurveyModule sur = ApplicationDbContext.Surveys.Where(x => x.modu_id == mod.Modu_Id).FirstOrDefault();
                SurveyQuestionBank sqb = ApplicationDbContext.SurveyQuestionBanks.Where(x => x.survey_id == sur.survey_id).FirstOrDefault();

                List<MultipleOptionsSurveyAnswer> mosa = ApplicationDbContext.MultipleOptionsSurveyAnswers.ToList();//Check just load answers of questions
                List<TrueFalseSurveyQuestion> tfsq = ApplicationDbContext.TrueFalseSurveyQuestions.Where(x => x.bank_id == sqb.bank_id).ToList();

                if (model.multipleOptionQuestions != null && model.multipleOptionQuestions.Count > 0)
                {
                    for (int cont = 0; cont < model.multipleOptionQuestions.Count; cont++)
                    {
                        if (model.multipleOptionQuestions[cont].userAnswerId != 0)
                        {
                            MultipleOptionsSurveyUser answer = new MultipleOptionsSurveyUser
                            {
                                usr_id = usr.us_id,
                                mosa_id = model.multipleOptionQuestions[cont].userAnswerId
                            };
                            ApplicationDbContext.MultipleOptionsSurveyUsers.Add(answer);
                        }
                    }
                }
                if (model.userAnswerTrueFalse != null && model.userAnswerTrueFalse.Count > 0)
                {
                    for (int cont = 0; cont < model.userAnswerTrueFalse.Count; cont++)
                    {
                        if (model.userAnswerTrueFalse[cont].userAnswerValue <= 1)
                        {
                            TrueFalseSurveyUser answer = new TrueFalseSurveyUser
                            {
                                usr_id = usr.us_id,
                                tfsq_id = model.userAnswerTrueFalse[cont].question.tfsq_id,
                                user_answer = model.userAnswerTrueFalse[cont].userAnswerValue
                            };
                            ApplicationDbContext.TrueFalseSurveyUsers.Add(answer);
                        }
                    }
                }
                float qualification = 0;

                if (model.userAnswerTrueFalse != null && model.userAnswerTrueFalse.Count > 0)
                {
                    // count correct true false answers
                    for (int cont = 0; cont < model.userAnswerTrueFalse.Count; cont++)
                    {
                        for (int cont2 = 0; cont2 < tfsq.Count; cont2++)
                        {
                            if (model.userAnswerTrueFalse[cont].question.tfsq_id == tfsq[cont2].tfsq_id)
                            {
                                if (model.userAnswerTrueFalse[cont].userAnswerValue == tfsq[cont2].correct)
                                {
                                    qualification += 1;
                                }
                                cont2 = tfsq.Count;
                            }
                        }
                    }
                }

                if (model.multipleOptionQuestions != null && model.multipleOptionQuestions.Count > 0)
                {
                    // count correct multiple option answers
                    for (int cont = 0; cont < model.multipleOptionQuestions.Count; cont++)
                    {
                        for (int cont2 = 0; cont2 < mosa.Count; cont2++)
                        {
                            if (model.multipleOptionQuestions[cont].userAnswerId == mosa[cont2].mosa_id)
                            {
                                if (mosa[cont2].correct_answer == 1)
                                {
                                    qualification += 1;
                                }
                                cont2 = mosa.Count;
                            }
                        }
                    }
                }

                usr.calification = (qualification / sqb.questionsToEvaluate) * 100f;
                usr.presented = true;
                model.validSession = false;
                model.calification = usr.calification;
                ApplicationDbContext.SaveChanges();
            }
            model.Sesion = GetActualUserId().SesionUser;
            return View("Survey", model);
        }
    }
}