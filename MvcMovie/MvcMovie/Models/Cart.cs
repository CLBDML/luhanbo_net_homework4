using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Cart
    {
        public int Id { get; set; } //Cart_id


        [Required(ErrorMessage = "购物车id必填")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "1-20个字符")]
        [Display(Name = "购物车id")]
        public string? cart_id { get; set; } //购物车id

        [Required(ErrorMessage = "商品id必填")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "1-20个字符")]
        [Display(Name = "商品id")]
        public string? product_id { get; set; } //商品id

        [Required(ErrorMessage = "商品数量必填")]
        [Range(1, 100, ErrorMessage = "1-100之间")]
        [Display(Name = "商品数量")]
        [DataType(DataType.Currency)]
        public int product_num { get; set; } //商品数量

        [Required(ErrorMessage = "商品价格必填")]
        [Range(1, 100, ErrorMessage = "1-100之间")]
        [Display(Name = "商品价格")]
        [DataType(DataType.Currency)]
        public decimal product_price { get; set; } //商品价格

        [Required(ErrorMessage = "用户id必填")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "1-20个字符")]
        [Display(Name = "用户id")]
        public string? user_id { get; set; } //用户id

        [Required(ErrorMessage = "创建时间必填")]
        [Display(Name = "创建时间")]
        [DataType(DataType.Date)]
        public DateTime createtime { get; set; } //创建时间



        
    }
}
