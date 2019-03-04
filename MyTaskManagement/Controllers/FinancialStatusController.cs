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

namespace MyTaskManagement.Controllers
{
    public class FinancialStatusController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());


        // GET: FinancialStatus  return list of All  Employee Financial 
        public ActionResult Index()
        {
            //so will pass list of users with thier financials 

            var listEmployeeFinanical = _unitOfWork.UserRepositry.GetAllUsersWithProjectsAndTasksAndRolesAndFinanicalWithFiles();
            return View(listEmployeeFinanical);
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
