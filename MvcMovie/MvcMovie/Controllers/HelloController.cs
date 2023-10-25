using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloController : Controller  //注意：控制器名称是"Hello" (不含Controller后缀
    {
        public IActionResult Index()
        {
            ViewData["Name"] = "小明";

            ViewData["Message"] = "important information";
            ViewData["Count"] = 10;
            return View();
        }

        //public String Index()  // https://localhost:端口号/Hello/Index,Index是默认的首页Action，可省略不写
        //{
        //    return "这是Index首页方法…";
        //}

        //public IActionResult Welcome() // https://localhost:端口号/Hello/Welcome
        //{
        //    return Content("<h3>这是Welcome方法…</h3>", "text/html; charset=utf-8");
        //}

        public IActionResult Welcome(string name, int n = 1)
        {
            ViewData["msg"] = "name=" + name + ", n=" + n;
            return View();
        }


        public string Details(int id)
        {
            string tmp = "id=" + id;
            return tmp;
        }

        public IActionResult Test()
        {
            return View(); // 返回登录界面 (视图代码见下页)
        }




    }
}
