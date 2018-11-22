using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLibraryIsOpen.Models.Search;
using TheLibraryIsOpen.Models.DBModels;
using TheLibraryIsOpen.Models;
using TheLibraryIsOpen.Controllers.StorageManagement;

namespace TheLibraryIsOpen.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionCatalog _tc;
        private readonly SearchTransactions _st;
        

        public TransactionController(TransactionCatalog tc, SearchTransactions st)
        {
            _tc = tc;
            _st = st;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _tc.GetLogs());
        }

       [HttpPost]
       public async Task<IActionResult> SearchTransactionHistory()
        {
            var form = HttpContext.Request.Form;
            string clientID = form["clientid"];
            string copyID = form["copyid"];
            string modelType = form["modeltype"];
            string modelID = form["modelid"];
            string date1 = form["date1"];
            string date2 = form["date2"];
            string time1 = form["time1"];
            string time2 = form["time2"];
            bool exactTime = (form["exacttime"] == "")? false:true;
            string transac = form["transc"];

            string dateTime1 = date1 + time1;
            string dateTime2 = date2 + time2;

            TempData["ClientID"] = clientID;
            TempData["CopyID"] = copyID;
            TempData["ModelType"] = modelType;
            TempData["ModelID"] = modelID;
            TempData["DateTime1"] = dateTime1;


            List<PrintedLog> logs = await _st.SearchLogsAsync(clientID, copyID, modelType, modelID, dateTime1, dateTime2, exactTime, transac);

            
            return View(logs);
        }
    }
}