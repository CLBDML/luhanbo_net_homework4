using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Movie != null ? 
        //                  View(await _context.Movie.ToListAsync()) :
        //                  Problem("Entity set 'MvcMovieContext.Movie'  is null.");
        //}

        //public async Task<IActionResult> Index(string searchstring)
        //{
        //    if (String.IsNullOrEmpty(searchstring))
        //    {
        //        searchstring = "";
        //    }
        //    var movies = from m in _context.Movie
        //                 where m.Genre.Contains(searchstring)
        //                 select m;
        //    return View(await movies.ToListAsync());
        //}

        public async Task<IActionResult> Index(string searchstring, double? low, double? high)
        {
            if (String.IsNullOrEmpty(searchstring)) { searchstring = ""; }
            if (low == null) { low = 0.0; }
            if (high == null) { high = 999.0; }
            var movies = from m in _context.Movie
                         where m.Title.Contains(searchstring) && Convert.ToDecimal(low) <= m.Price
                         && m.Price <= Convert.ToDecimal(high)
                         select m;
            return View(await movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Test(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            // 创建一个模型对象
            Movie? movie = _context.Movie.Find(id); // 含义：从Movie表中查找id=1的对象

            if(movie == null) {
                return NotFound("查询的电影不存在");
            }

            return View(movie);
        }

        public IActionResult Test2()
        {
            List<Movie> movies = _context.Movie.ToList(); // 含义：查询Movie表所有对象
            return View(movies);
        }

        public string Test3()
        {
            int[] scores = new int[] { 97, 85, 92, 81, 60 }; //定义int数组
            IEnumerable<int> query =
                from s in scores //必须
                where s > 80 //条件，可选
                where s < 90
                orderby s descending //排序可选，descending降序，ascending升序(默认)
                select s; //必须
            string tmp = "";
            foreach (int i in query) //遍历查询的结果集
            {
                tmp += i + " ";
            }
            return tmp;
        }

        public string Test4()
        {
            string[] instructors = { "Aaron", "Jane", "Fritz", "Keith", "Scott", "Tom" }; //定义string数组
            IEnumerable<string> query =
            from s in instructors //必须
            where s.Length == 5 //条件，可选
            orderby s descending //排序可选，descending降序，ascending升序(默认)
            select s; //必须
            string tmp = "";
            foreach (string i in query) //遍历查询的结果集
            {
                tmp += i + " ";
            }
            return tmp;
        }

        public IActionResult Test5()
        {
           string searchstring = "科幻"; //电影类型
           var query =
           from s in _context.Movie //查询Movie表
           where s.Genre.Contains(searchstring) //电影类型(Genre)包含
           orderby s.Id //按电影id降序排序
           select s;
            return View("Test2", query.ToList());
        }


    }
}
