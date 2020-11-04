using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SCORM1.Enum;
using SCORM1.Models;
using SCORM1.Models.Lms;
using SCORM1.Models.ViewModel;
using SCORM1.Models.Engagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using SCORM1.Models.PageCustomization;
using PagedList;
using SCORM1.Models.Logs;
using SCORM1.Models.ratings;
using System.Security.Cryptography.X509Certificates;
using SCORM1.Models.RigidCourse;
using iTextSharp.text.pdf.events;

namespace SCORM1.Controllers
{
    public class ProtectedFailureController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        protected AdminTrainingController adminTrainingController { get; set; }
        // GET: ProtectedFailure
        public ActionResult Index()
        {
            return View();
        }

        #region Protected failure functions
        //Add the created protected failure and its categories question bank
        [HttpPost]
        [Authorize]
        public ActionResult CreateProtectedFailureTest(AdminProtectedFailure model)
        {
            AdminProtectedFailure pft = null;
            int bankQuestionsAdded = 0;
            //if the model passed throug parameter is valid
            if (ModelState.IsValid)
            {
                pft = model;
                ProtectedFailureTest testToAdd = pft.protectedFailureTest;
                try
                {
                    //add the protected failure received to the database
                    ApplicationDbContext.ProtectedFailureTests.Add(testToAdd);
                    ApplicationDbContext.SaveChanges();
                }
                catch
                {
                    //if it can't be added, return to the view
                    TempData["Info"] = "No se pudo crear la evaluación de falla protegida";
                    return (adminTrainingController.Grades(model.protectedFailureTest.Modu_Id));
                }

                //cicles throug the categories to add their respective question banks
                for (int cont = 0; cont < pft.categoryList.Count; cont++)
                {
                    if (pft.bankToCreateList[cont])
                    {
                        CategoryQuestionBank cqb = new CategoryQuestionBank
                        {
                            Cate_Id = pft.categoryList[cont].Cate_Id,
                            Modu_Id = pft.protectedFailureTest.Modu_Id,
                            EvaluatedQuestionQuantity = pft.questionQuantityList[cont],
                            AprovedCategoryPercentage = pft.approvedPercentageList[cont]
                        };
                        ApplicationDbContext.CategoryQuestionsBanks.Add(cqb);
                    }
                }
                ApplicationDbContext.SaveChanges();
            }
            if (pft == null)
            {
                TempData["Info"] = "No se pudo crear la evaluación de falla protegida";
            }
            else
            {
                TempData["Info"] = "Evaluación de falla protegida creada correctamente, se agregaron " + bankQuestionsAdded + " categorias";
            }
            return (adminTrainingController.Grades(model.protectedFailureTest.Modu_Id));
        }

        [Authorize]
        public ActionResult CreateProtectedFailureTest(int id)
        {
            AdminProtectedFailure apf = new AdminProtectedFailure
            {
                modu_id = id
            };
            return View();
        }

        //Creates the Protected failure test with the view given attributes
        [Authorize]
        public ActionResult AddQuestionsToProtectedFailureTest(int modu_Id)
        {
            AdminProtectedFailure apf = null;
            if (ApplicationDbContext.ProtectedFailureTests.Find(modu_Id) != null)
            {
                apf = new AdminProtectedFailure
                {
                    protectedFailureTest = ApplicationDbContext.ProtectedFailureTests.Find(modu_Id),
                    categoryList = ApplicationDbContext.Categories.Where(x => x.ToCo_Id == x.TopicCourse.ToCo_Id && x.TopicCourse.Modu_Id == modu_Id).ToList()
                };
                apf.bankToCreateList = new List<bool>(apf.categoryList.Count);
                apf.questionQuantityList = new List<int>(apf.categoryList.Count);
                apf.approvedPercentageList = new List<float>(apf.categoryList.Count);
            }
            //To-Do customized view
            return View(apf);
        }
        #endregion

    }
}