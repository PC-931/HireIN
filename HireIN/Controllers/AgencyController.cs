using HireIN.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace HireIN.Controllers
{
    public class AgencyController : Controller
    {
        public static int id = 1000;
        public EmployeeEntities db;
        public List<Vacancy> vacancies;

        public AgencyController()
        {
            vacancies = new List<Vacancy>();
            db = new EmployeeEntities();
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
                    TempData["err"] = "Please enter valid Username and Password!!!";
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
                if( usr.AgencyEmail!=null || usr.AgencyPass!=null )
                {
                    usr.AgencyId = id;
                    id++;
                    db.Agencies.Add(usr);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["err"] = "Please fill the form completely!!!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
            }
            return View();
        }

        public ActionResult Dashboard(Agency data)
        {
            return View();
        }

        
        public ActionResult showVacancy()
        {
            try
            {
                int agentId = (int)Session["aid"];
                vacancies = db.Vacancies.ToList();

                var q = from vac in vacancies
                        where vac.AgencyId== agentId
                        select vac;

                return View(q);
               
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                if ( v.JobTitle!=null || v.JobLocation!=null || v.JobDescription!=null || v.Salary>0  )
                {
                    v.VacancyId = id;
                    id++;
                    int agentId = (int)Session["aid"];
                    v.AgencyId = agentId;
                    db.Vacancies.Add(v);
                    db.SaveChanges();
                    return RedirectToAction("showVacancy");
                }
                else
                {
                    TempData["err"] = "All fields are required!!";
                    return View();
                }

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

                //int agentId = (int)Session["aid"];
                //vacancies = db.Vacancies.ToList();
                //var vacList = from vac in vacancies
                //        where vac.AgencyId == agentId
                //        select vac;
                //var q = appList.Join(vacList,
                //                        a => a.VacancyId,
                //                        v => v.VacancyId,
                //                        (a, v) =>
                //                            {
                //                                a.VacancyId = a.VacancyId
                //                            }
                //                     );
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
            a = db.Applicants.Find(id);
            a.Status = "Selected";
            db.SaveChanges();
            return View();
        }

        public ActionResult RejectedCandidates(int id)
        {
            Applicant a = new Applicant();
            a = db.Applicants.Find(id);
            a.Status = "Rejected";
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult EditVacancy(int id)
        {
            Vacancy vac = db.Vacancies.Find(id);
            return View(vac);
        }

        [HttpPost]
        public ActionResult EditVacancy(Vacancy v)
        {
            db.Vacancies.AddOrUpdate(v);
            db.SaveChanges();
            return RedirectToAction("showVacancy");
        }

        public ActionResult DeleteVacancy(int id)
        {
            Vacancy v = db.Vacancies.Find(id);
            db.Vacancies.Remove(v);
            return View("showVacancy");
        }

        public ActionResult ShowCandidateDetails(int id)
        {
            Candidate rec = new Candidate();
            try
            {
                rec = db.Candidates.Find(id);
                return View(rec);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
        }


        public ActionResult ShowVacancyDetails(int id)
        {
            try
            {
                Vacancy rec = db.Vacancies.Find(id);
                return View(rec);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}