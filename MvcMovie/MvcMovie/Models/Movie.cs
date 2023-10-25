using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int Id { get; set; } //电影id


        [Required(ErrorMessage = "电影名称必填")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "3-20个字符")]
        [Display(Name = "电影名称")]
        public string? Title { get; set; } //电影名称


        [Required(ErrorMessage = "上映日期必填")]
        [Display(Name = "上映日期")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } //上映日期


        [Required(ErrorMessage = "电影类型必填")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "2-10个字符以内")]
        [Display(Name = "电影类型")]
        public string? Genre { get; set; } //电影类型


        [Required(ErrorMessage = "电影票价必填")]
        [Range(1, 100, ErrorMessage = "1-100之间")]
        [Display(Name = "票价")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; } //电影票价
    }
}
