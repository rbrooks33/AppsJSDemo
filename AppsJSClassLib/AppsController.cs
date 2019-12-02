using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;

namespace AppsJSClassLibStandard
{
    [Route("api/AppsJS")]
    public class AppsJSController : ApiController
    {
        [Route("api/AppsJS/GetAutoComponentFolders")]
        [HttpGet]
        public Result GetAutoComponentFolders(string webRoot)
        {
            var result = new Result();

            try
            {
                var stringList = new List<string>();

                string path = System.Environment.CurrentDirectory; //HttpContext.Current.Server.MapPath("/"); // _env.WebRootPath; // + "\\" + virtualFolder; //Physical path of apps folder
                string componentPath = path + "\\Scripts\\Apps\\AutoComponents";
                var componentFolder = new DirectoryInfo(componentPath);
                foreach (var subfolder in componentFolder.GetDirectories())
                {
                    stringList.Add(subfolder.Name);
                }
                result.Data = stringList;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Data = ex;
            }

            return result;
        }

        [Route("api/AppsJS/CreateComponent")]
        [HttpGet]
        public Result CreateComponent(string appsRoot, string componentName)
        {
            var result = new Result();

            try
            {
                string path = System.Environment.CurrentDirectory; // HttpContext.Current.Server.MapPath("/" + appsRoot); //_env.WebRootPath + "\\" + appsRoot; //Physical path of apps folder
                string componentsPath = path + "\\AutoComponents";
                string templatesPath = path + "\\Templates"; // + appsRoot.Replace("/", "\\") + @"\Pages"; //Physical path to Apps templates folder

                result.Logs.Add("Determined component path to be: " + path);

                if (Directory.Exists(path))
                {

                    string componentPath = componentsPath + "\\" + componentName;

                    result.Logs.Add("Found main components path (" + componentPath + "). Checking existence of component path: " + componentPath);

                    if (!Directory.Exists(componentPath))
                    {
                        result.Logs.Add("New component path doesn't exist. Creating it.");

                        Directory.CreateDirectory(componentPath);

                        CreateComponentPage(templatesPath + "\\empty.js", componentPath + "\\" + componentName + ".js", componentName);
                        CreateComponentPage(templatesPath + "\\empty.html", componentPath + "\\" + componentName + ".html", componentName);
                        CreateComponentPage(templatesPath + "\\empty.css", componentPath + "\\" + componentName + ".css", componentName);

                        result.Success = true; //only creates once
                    }
                    else
                        result.Logs.Add("Main components path doesn't exist.");

                }
            }
            catch (Exception ex)
            {
                result.Data = ex;
            }

            return result;
        }

        private Result CreateComponentPage(string templatePath, string componentPagePath, string componentName)
        {
            var result = new Result();

            try
            {
                //COPY JS
                //string jsComponentsPath = componentPath + "\\" + componentName + ".js";

                result.Logs.Add("Checking component file: " + componentPagePath);

                if (!System.IO.File.Exists(componentPagePath))
                {
                    result.Logs.Add("Component file needs to be created.");

                    var htmlText = System.IO.File.ReadAllText(templatePath);
                    htmlText = htmlText.Replace("MyTemplate", componentName);
                    System.IO.File.WriteAllText(componentPagePath, htmlText);

                    result.Logs.Add("Component file created.");
                }
                else
                    result.Logs.Add("Component path already exists.");

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Data = ex;
            }
            return result;
        }
        [Route("api/AppsJS/CreatePage")]
        [HttpPost]
        public Result CreatePage(string webRoot, string appsRoot, string pagesRoot, string pageName, string pageControls, bool shortnamespaces)
        {
            var result = new Result();

            try
            {
                string path = System.Environment.CurrentDirectory; //HttpContext.Current.Server.MapPath("/"); //_env.WebRootPath; // + "\\" + virtualFolder; //Physical path of apps folder
                string pagesPath = path + "\\" + pagesRoot.Replace("/", "\\"); //Physical path to pages root
                string appsPageTemplatesPath = path + "\\" + appsRoot.Replace("/", "\\") + @"\Pages"; //Physical path to Apps templates folder

                if (Directory.Exists(path))
                {
                    pagesPath += "\\" + pageName;
                    
                    if (!Directory.Exists(pagesPath))
                    {
                        Directory.CreateDirectory(pagesPath);
                    }

                    //page js file
                    string pagePath = pagesPath + "\\" + pageName + ".js";

                    if (!File.Exists(pagePath))
                    {
                        File.Copy(appsPageTemplatesPath + "\\empty.js", pagePath);

                        var pageJSText = System.IO.File.ReadAllText(pagePath);
                        pageJSText = pageJSText.Replace("MyTemplate", pageName);

                        File.WriteAllText(pagePath, pageJSText);
                    }

                    //page html file
                    string pageHtmlPath = pagesPath + "\\" + pageName + ".html";

                    if (!File.Exists(pageHtmlPath))
                    {
                        File.Copy(appsPageTemplatesPath + "\\empty.html", pageHtmlPath);

                        //merge empty html file
                        var htmlText = File.ReadAllText(pageHtmlPath);
                        htmlText = htmlText.Replace("MyTemplate", pageName);
                        File.WriteAllText(pageHtmlPath, htmlText);
                    }

                    //page css file
                    string pageCssPath = pagesPath + "\\" + pageName + ".css";

                    if (!File.Exists(pageCssPath))
                    {
                        File.Copy(appsPageTemplatesPath + "\\empty.css", pageCssPath);

                        //merge empty html file
                        var cssText = File.ReadAllText(pageCssPath);
                        cssText = cssText.Replace("MyTemplate", pageName);
                        File.WriteAllText(pageCssPath, cssText);
                    }

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Data = ex;
            }

            return result;
        }
    }
}
