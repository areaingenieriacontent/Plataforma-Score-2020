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
using SCORM1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SCORM1.Models.ViewModel;
using SCORM1.Models.VSDR;
using System.Collections.Generic;
using System;

namespace SCORM1.Controllers
{
    public class VSDRController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public VSDRController()
        {
            ApplicationDbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        // GET: VSDR
        public ActionResult Index()
        {
            return View();
        }
        public ApplicationUser GetActualUserId()
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            return user;
        }

        #region VSDR Users
        #endregion

        #region VSDR Teachers
        //Returns all available VSDR sessions available
        [Authorize]
        public ActionResult VsdrUserList()
        {
            VsdrUserVM vsdrModel = new VsdrUserVM();
            //Get list from data base;
            List<VsdrSession> vsdrList= new List<VsdrSession>();
            List<VsdrSession> tempList= new List<VsdrSession>();
            vsdrList = ApplicationDbContext.VsdrSessions.ToList();

            //Removes not available and date expired vsdr sessions
            if (vsdrList.Count > 0)
            {
                foreach (VsdrSession debateRoom in vsdrList)
                {
                    if (!debateRoom.available)
                    {
                        tempList.Add(debateRoom);
                    }
                    else if (debateRoom.end_date < DateTime.Now)
                    {
                        tempList.Add(debateRoom);
                    }
                }
                foreach (VsdrSession debateRoom in tempList)
                {
                    vsdrList.Remove(debateRoom);
                }
            }
            //Fillter list by date and availability;
            vsdrModel.listOfVsdr = vsdrList;
            vsdrModel.Sesion = GetActualUserId().SesionUser;
            return View(vsdrModel);
        }

        [Authorize]
        public ActionResult VsdrContent(int id)
        {
            VsdrUserVM vsdrModel = new VsdrUserVM();
            VsdrSession vsdrToReturn = ApplicationDbContext.VsdrSessions.Find(id);
            VsdrUserFile vsdrUserIssuedFiles;
            VsdrTeacherComment vsdrTeacherComments;
            if (vsdrToReturn.end_date.Subtract(DateTime.Now).TotalMinutes < 15)
            {
                vsdrModel.meetingAvailable = true;
            }
            //add loaded data to view model
            vsdrModel.actualVsdr = vsdrToReturn;
            vsdrModel.Sesion = GetActualUserId().SesionUser;
            return View(vsdrModel);
        }

        public ActionResult RedirectToUrl(int id)
        {
            VsdrSession vsdrToReturn = ApplicationDbContext.VsdrSessions.Find(id);
            return Redirect(vsdrToReturn.resource_url);
        }
        
        #endregion

        #region VSDR Admin
        #endregion

    }
}