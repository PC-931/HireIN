using HireIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HireIN.Controllers
{
    public class AgencyController : Controller
    {
        public static int id = 1000;
        public MVC_AssessmentEntities1 db;
        public List<Vacancy> vacancies;

        public AgencyController()
        {
            vacancies = new List<Vacancy>();
            db = new MVC_AssessmentEntities1();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Agency usr)
        {
            try
            {
                var res = db.Agencies.Where(m => (m.AgencyEmail == usr.AgencyEmail) && (m.AgencyPass == usr.AgencyPass)).FirstOrDefault();
                Agency data = new Agency();
                data = res;
                if( data!= null)
                {
                    Session["aid"] = data.AgencyId;
                    return RedirectToAction("Dashboard", data);
                }
                else
                {
                    TempData["err"] = "UserNotFound";
                }
            }
            catch(Exception ex)
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
        public ActionResult Register(Agency usr)
        {
            try
            {
                usr.AgencyId = id;
                id++;
                db.Agencies.Add(usr);
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

        
        public ActionResult showVacancy()
        {
            try
            {                
                vacancies = db.Vacancies.ToList();
                if(vacancies.Count > 0)
                {
                    return View(vacancies);
                }
                else
                {
                    TempData["err"] = "No vacancies created!!!";
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View(vacancies);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult CreateVacancy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateVacancy(Vacancy v)
        {
            try
            {
                v.VacancyId = id;
                id++;
                //v.AgencyId = Convert.ToInt32(Session["aid"]);
                db.Vacancies.Add(v);
                db.SaveChanges();
                return RedirectToAction("showVacancy");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult ApplicantsApplied()
        {
            try
            {
                List<Applicant> appList = new List<Applicant>();
                appList = db.Applicants.ToList();
                return View(appList);
            }
            catch(Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }

        public ActionResult AcceptedCandidates(int id)
        {
            Applicant a = new Applicant();
            a = db.Applicants.FirstOrDefault();
            a.Status = "Selected";
            db.SaveChanges();
            return View();
        }

        public ActionResult RejectedCandidates(int id)
        {
            Applicant a = new Applicant();
            a = db.Applicants.FirstOrDefault();
            a.Status = "Rejected";
            db.SaveChanges();
            return View();
        }
    }
}