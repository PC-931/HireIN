﻿using HireIN.Models;
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
        List<Candidate> candidates;
        List<Vacancy> vacList;

        public CandidateController()
        {
            candidates = new List<Candidate>();
            db = new MVC_AssessmentEntities1();
            vacList = new List<Vacancy>();
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
                    Session["cid"] = data.CandidateId;
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

        public ActionResult showVacancyToCandidate()
        {
            try
            {
                vacList = db.Vacancies.ToList();
                return View(vacList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult CandidateApplied(int vid)
        {
            try
            {
                Applicant a = new Applicant();
                a.ApplicantId = id;
                id++;
                a.CandidateId = (int)Session["cid"];
                a.VacancyId = vid;
                a.Status = "Applied";

                db.Applicants.Add(a);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }

        public ActionResult AppliedApplicantStatus()
        {
            List<Applicant> a = new List<Applicant>();
            a = db.Applicants.ToList();
            return View(a);
        }
    }
}