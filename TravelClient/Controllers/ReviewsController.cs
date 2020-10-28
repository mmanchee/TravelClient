using Microsoft.AspNetCore.Mvc;
using TravelClient.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace TravelClient.Controllers
{
  public class ReviewsController : Controller
  {
  private readonly TravelClientContext _db;

    public ReviewsController(TravelClientContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Review> model = _db.Reviews.ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      ViewBag.MajorId = new SelectList(_db.Majors, "MajorId", "MajorName");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Review Review)
    {
      Major majorRow = _db.Majors.FirstOrDefault(majors => majors.MajorId == Review.MajorId);
      Review.DepartmentId = majorRow.DepartmentId;
      _db.Reviews.Add(Review);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      var thisReview = _db.Reviews
        .Include(Reviews => Reviews.Courses)
        .ThenInclude(join => join.Course)
        .FirstOrDefault(Review => Review.ReviewId == id);
        return View(thisReview);
    }
    public ActionResult Edit(int id)
    {
      var thisReview = _db.Reviews.FirstOrDefault(Reviews => Reviews.ReviewId == id);
      ViewBag.MajorId = new SelectList(_db.Majors, "MajorId", "MajorName");
      return View(thisReview);
    }
    [HttpPost]
    public ActionResult Edit(Review Review)
    {
      Major majorRow = _db.Majors.FirstOrDefault(majors => majors.MajorId == Review.MajorId);
      Review.DepartmentId = majorRow.DepartmentId;
      _db.Entry(Review).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
        var thisReview = _db.Reviews.FirstOrDefault(Reviews => Reviews.ReviewId == id);
        return View(thisReview);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        var thisReview = _db.Reviews.FirstOrDefault(Reviews => Reviews.ReviewId == id);
        _db.Reviews.Remove(thisReview);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult Completed(int joinId)
    {
        var joinEntry = _db.CourseReview.FirstOrDefault(entry => entry.CourseReviewId == joinId);
        joinEntry.Completed = true;
        _db.Entry(joinEntry).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = joinEntry.ReviewId });
    }
  }
}