using iTextSharp.text;
using iTextSharp.text.pdf;
using MyTaskManagement.Models;
using MyTaskManagement.Persistence;
  
using iTextSharp.text.html.simpleparser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using iTextSharp.tool.xml;
using MyTaskManagement.Core.ViewModel;
using MyTaskManagement.Core.Domain;

namespace MyTaskManagement.Controllers
{
    public class FinancialStatusController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());


        // GET: FinancialStatus  return list of All  Employee Financial 
        public ActionResult Index()
        {
            //so will pass list of users with thier financials 

            var listEmployeeFinanical = _unitOfWork.UserRepositry.GetAllUsersWithProjectsAndTasksAndRolesAndFinanicalWithFilesWithPayments();
            return View(listEmployeeFinanical);
        }


         

        // GET: FinancialStatus  return list of  Financial  for one employee
        public ActionResult ListFinancailForOneEmployee(string id, int month = 0, int year = 0)
        {
            //so will pass user  with thier financials 

            var UserWithlistFinanical = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFilesWithPayments(id);
            //return View(UserWithlistFinanical);

             var listOwnFinancial = UserWithlistFinanical.FinancialstatusList;

            if (month == 0 && year != 0)
            { //No sorting  for month
                listOwnFinancial = listOwnFinancial.Where( financialstatus => financialstatus.Date.Year == year).ToList();

            }
            else if (month != 0 && year == 0)
            {//No sorting  for year
                listOwnFinancial = listOwnFinancial.Where(financialstatus => financialstatus.Date.Month == month).ToList();

            }
            else if (month != 0 && year != 0)
            {//No sorting  for year nor month
                listOwnFinancial = listOwnFinancial.Where(financialstatus => financialstatus.Date.Month == month && financialstatus.Date.Year == year).ToList();

            }

            var vm = new FinanicalstatusViewModel()
            {
                 User = UserWithlistFinanical,
                Financialstatus = listOwnFinancial
            };
           
            ViewBag.m = month;
            ViewBag.y = year;

            return View(vm);


        }

        // GET: FinancialStatus  return list of   AllPayments  for all employee
        public ActionResult ShowAllUsersWithPayments()
        {
            var listusers = _unitOfWork.UserRepositry.GetAllUsersWithProjectsAndTasksAndRolesAndFinanicalWithFilesWithPayments();
          
            return View(listusers);
        }


        //Get
        public ActionResult ShowPaymentsEmployee(string id, int month = 0, int year = 0)
        {
            var employee = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFilesWithPayments(id);
            var listOwnPayments = employee. Payments;

            if (month == 0 && year != 0)
            { //No sorting  for month
                listOwnPayments = listOwnPayments.Where(l => l.DateTime.Year == year).ToList();

            }
            else if (month != 0 && year == 0)
            {//No sorting  for year
                listOwnPayments = listOwnPayments.Where(l => l.DateTime.Date.Month == month).ToList();

            }
            else if (month != 0 && year != 0)
            {//No sorting  for year nor month
                listOwnPayments = listOwnPayments.Where(l => l.DateTime.Month == month && l.DateTime.Year == year).ToList();

            }

            var vm = new PaymentViewModel()
            {
                User = employee,
                 Payments = listOwnPayments
            };

            ViewBag.m = month;
            ViewBag.y = year;
            return View(vm);
        }
        public ActionResult DeletePaymentsEmployee(int id)
        {
            var pay = _unitOfWork.paymentRepositry.Get(id);
            var idd = pay.ApplicationUserId;
              _unitOfWork.paymentRepositry.Remove(pay);
              _unitOfWork.Complete();


            return RedirectToAction("ShowPaymentsEmployee", new {id=  idd });
        }

        public ActionResult CreatePayment(string id)
        {
            var newPayment = new Payment()
            {ApplicationUserId = id,
                DateTime = DateTime.Now //set default date time...
            };
            ViewBag.allusers = _unitOfWork.UserRepositry.GetAll();
            return View(newPayment);
        }

        [HttpPost]
        public ActionResult CreatePayment(Payment payment )
        {
            var ui = Request.Form["__UserId__"];
            var user = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFilesWithPayments(ui);


            var newPayment = new Payment()
            {
                ApplicaionUser= new ApplicationUser(),
                DateTime = payment.DateTime,
                Note = payment.Note,
                AmountOfMoney=payment.AmountOfMoney
            };

            newPayment.ApplicaionUser = user;

            _unitOfWork.paymentRepositry.Add(newPayment);
            _unitOfWork.Complete();

            return RedirectToAction("ShowPaymentsEmployee" , new {id = ui });
        }
        // GET: FinancialStatus/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FinancialStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinancialStatus/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialStatus/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FinancialStatus/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialStatus/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FinancialStatus/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Export(string GridHtml)
        {
         
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }
    }
}
