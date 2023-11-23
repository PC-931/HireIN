using HireIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HireIN.Controllers
{
    public class CandidateController : Controller
    {
        public static int id = 1500;
        public MVC_AssessmentEntities1 db;

        public CandidateController()
        {
            db = new MVC_AssessmentEntities1();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Candidate usr)
        {
            try
            {
                var res = db.Candidates.Where(m => (m.CandidateEmail == usr.CandidateEmail) && (m.CandidatePass == usr.CandidatePass)).FirstOrDefault();
                Candidate data = new Candidate();
                data = res;
                if (data != null)
                {
                    Session["aid"] = data.CandidateId;
                    return RedirectToAction("Dashboard", data);
                }
                else
                {
                    TempData["err"] = "UserNotFound";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Candidate usr)
        {
            try
            {
                usr.CandidateId = id;
                id++;
                db.Candidates.Add(usr);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Dashboard(Agency data)
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}