using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoMaster.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using DemoMaster.Repository;

namespace DemoMaster.Controllers
{
    public class CustomerMastersController : Controller
    {
        private FMSARABICEntities2 db = new FMSARABICEntities2();

        // GET: CustomerMasters
        public ActionResult Index()
        {
            return View(db.CustomerMaster.ToList());
        }

        // GET: CustomerMasters/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerMaster customerMaster = db.CustomerMaster.Find(id);
            if (customerMaster == null)
            {
                return HttpNotFound();
            }
            return View(customerMaster);
        }

        // GET: CustomerMasters/Create
        public ActionResult Create()
        {
            CustomerMaster GetValue = new CustomerMaster();
            GetValue.GetCountryCode = GetCountryCode();
            //CustomerMaster GetValue = new CustomerMaster();
            GetValue.GetStateCode = GetStateCode(GetValue);
            GetValue.GetCompany = GetCompany();
            return View(GetValue);
        }

        // POST: CustomerMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CustomerCode,CompanyCode,CompanyName,TitleName,FirstName,MiddleName,LastName,AccountCode,CustomerTypeCode,PaymentTermsCode,Address1,Address2,City,StateCode,CountryCode,ZipCode,Phone,Fax,Email,WebSite,CreditCardNo,ExpiryDate,Taxable,ShipToAddress1,ShipToAddress2,ShipToCity,ShipToStateCode,ShipToCountryCode,ShipToZipCode,Remarks,BalanceAmount,InActive,CreatedUser,CreatedDate,ModifiedUser,ModifiedDate,OPENBAL,EXCHANGERATE,FAMILYCODE,CreatedInFinacs,Comments,SSN,EmpTypeId,EmpGroupId,DepartmentId,BankAccountNo,BankName,PayCycle,BirthDay,NoOfDependents,MaritalStatus,DateOfJoin,Released,ReleasedOn,ReleasedRemarks,APAccount,CustomField1,CustomField2,CustomField3,CustomField4,CustomField5,CustomField6,CustomField7,CustomField8,CustomField9,CustomField10,CustomField11,CustomField12,CustomField13,CustomField14,CustomField15,ContactPersonName,ContactGUID,APFamilyCode,StatusID")] CustomerMaster customerMaster)
        public ActionResult Create([Bind(Include = "CustomerCode,CompanyCode,CompanyName,Address1,Address2,City,StateCode,CountryCode,ZipCode,Phone,Fax,Email,InActive,CreatedDate")] CustomerMaster customerMaster)
        {
            if (ModelState.IsValid)
            {
                db.CustomerMaster.Add(customerMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerMaster);
        }

        // GET: CustomerMasters/Edit/5
        public ActionResult Edit(string customercode,string companycode)
        {
            if (customercode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerMaster customerMaster = db.CustomerMaster.Find(customercode,companycode);
            if (customerMaster == null)
            {
                return HttpNotFound();
            }
            return View(customerMaster);
        }

        // POST: CustomerMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerCode,CompanyCode,CompanyName,Address1,Address2,City,StateCode,CountryCode,ZipCode,Phone,Fax,Email,InActive,CreatedDate")] CustomerMaster customerMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerMaster);
        }

        // GET: CustomerMaster/Delete/5
        public ActionResult Delete(string customercode, string companycode)
        {
            if (customercode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerMaster customerMaster = db.CustomerMaster.Find(customercode, companycode);
            if (customerMaster == null)
            {
                return HttpNotFound();
            }
            return View(customerMaster);
        }

        // POST: CustomerMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string customercode, string companycode)
        {
            try
            {
                CustomerMaster customerMaster = db.CustomerMaster.Find(customercode, companycode);
                db.CustomerMaster.Remove(customerMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "CustomerMastersController", "Index"));

            }
        }
        public ActionResult List()
        {
            return View(db.CustomerMaster.ToList());
        }
        private static List<SelectListItem> GetCountryCode()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["FMSARABICEntities"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = " SELECT distinct CountryName, CountryCode FROM CountryMaster";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["CountryName"].ToString(),
                                Value = sdr["CountryCode"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }
        private static List<SelectListItem> GetStateCode(CustomerMaster cm)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["FMSARABICEntities"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.CommandText = "stpMas_GetAllStateName";

            SqlCommand cmd = new SqlCommand("stpMas_GetAllStates", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //ObjectParameter op3 = new ObjectParameter("RECORDCOUNT", typeof(int));
            //cmd.Parameters.AddWithValue("@RecordCount", op3);

            SqlParameter recordcount = new SqlParameter();
            recordcount.ParameterName = "@RECORDCOUNT";// Defining Name
            recordcount.SqlDbType = SqlDbType.Int; // Defining DataType
            recordcount.Direction = ParameterDirection.Output;// Setting the direction 
            cmd.Parameters.Add(recordcount);


            //add any parameters the stored procedure might require
            //cmd.Parameters.Add(new SqlParameter("@parameter1", token));
            cmd.Connection = con;
            con.Open();


            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    items.Add(new SelectListItem
                    {
                        Text = sdr["StateName"].ToString(),
                        Value = sdr["StateCode"].ToString()
                    });
                }
            }
            con.Close();

            return items;
        }
        private static List<SelectListItem> GetCompany()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["FMSARABICEntities"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = " SELECT distinct CompanyName, CompanyCode FROM CompanySetup";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["CompanyName"].ToString(),
                                Value = sdr["CompanyCode"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }

        //public ActionResult AddCustomer(CustomerMaster cm)
        public ActionResult JQAjaxAddCustomerDB(CustomerMaster cm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CustomerRepository cmRepo = new CustomerRepository();
                    if (cmRepo.AddCustomer(cm))
                    {
                        return Json(new
                        {
                            msg = "Successfully added " + cm.CompanyName
                        });
                        //ViewBag.Message = "Customer details added successfully";
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult JQAjax(CustomerMaster cm)
        {
            try
            {
                return Json(new { Customer = cm, JsonRequestBehavior.AllowGet });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ActionResult AjaxGetList()
        {
            try
            {
                return Json(db.CustomerMaster.ToList(), JsonRequestBehavior.AllowGet);
                //return Json(db.CustomerMaster.ToList().Where(user => user.CustomerCode == "Abb0001"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("false");
            }
        }
        public ActionResult AjaxAddList()
        {
            try
            {
                return View("AjaxAddList");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //public ActionResult JQAjax(CustomerMaster cm)
        //{
        //    try
        //    {
        //        return Json(new
        //        {
        //            msg = "Successfully added " + cm.CompanyName
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
