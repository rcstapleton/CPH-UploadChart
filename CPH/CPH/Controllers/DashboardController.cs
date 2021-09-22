﻿using CPH.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IWebHostEnvironment _hostEnv;
        public DashboardController(ILogger<DashboardController> logger, IWebHostEnvironment hostEnv)
        {
            _logger = logger;
            _hostEnv = hostEnv;
        }
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Chart()
        {
            return View();
        }

        public IActionResult Account()
        {
            return View();
        }

        public IActionResult UploadCSV()
        {
            return View();
        }

        [HttpPost]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> UploadCSVAsync(UploadCSVModel form)
        {
            if (ModelState.IsValid)
            {
                var file = form.File;

                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    //get the wwwroot path and append the dir
                    var filePath = _hostEnv.WebRootPath + "\\uploads\\";

                    //create the dir
                    Directory.CreateDirectory(filePath);

                    //create the path for the uploaded file
                    var path = Path.Combine(filePath, fileName);

                    //copy the uploaded file to the dir
                    using (var stream = System.IO.File.Create(path))
                    {
                        await file.CopyToAsync(stream);
                    }

                }
            }
            return View();
        }

    }
}
